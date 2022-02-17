// SPDX-License-Identifier: AGPL-3.0-only
pragma solidity ^0.8.0;

import { VolatileToken } from "./VolatileToken.sol";

import { ERC20 } from "@rari-capital/solmate/src/tokens/ERC20.sol";
import { SafeTransferLib } from "@rari-capital/solmate/src/utils/SafeTransferLib.sol";
import { IERC20 } from "./interfaces/IERC20.sol";
import { IERC4626 } from "./interfaces/IERC4626.sol";
import { FixedPointMathLib } from "./utils/FixedPointMath.sol";

import "@chainlink/contracts/src/v0.8/interfaces/AggregatorV3Interface.sol";
import "./interfaces/IWETH9.sol";

import "hardhat/console.sol";

/// Deposit, Withdraw, Mint, Redeem exists for Stable
/// Fund & Defund exists for Volatility
/// All reserve asset accounting (WETH) is done inside of Stable Vault.
/// Preserve 4626 expected interface (eg vault mint/burn operatin on one underlying)

error NoEthSupplied();

contract StableVault is ERC20, IERC4626 {
    using SafeTransferLib for ERC20;
    using FixedPointMathLib for uint256;

    uint256 public totalFloat;
    uint256 public constant MAX_DEBT_RATIO = (1e18 * 8) / 10; // 80%

    VolatileToken public immutable volatile;
    IWETH9 public immutable weth;
    AggregatorV3Interface internal immutable priceFeed;

    constructor() ERC20("SRAI", "SRU", 18) {
        volatile = new VolatileToken("VOLAI", "volSRU", 18);
        weth = IWETH9(0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2);
        priceFeed = AggregatorV3Interface(0x5f4eC3Df9cbd43714FE2740f5E3616155c5b8419);
    }

    receive() external payable {}

    fallback() external payable {}

    /*///////////////////////////////////////////////////////////////
                        DEPOSIT/WITHDRAWAL LOGIC
    //////////////////////////////////////////////////////////////*/

    /// @notice Stablecoin
    function deposit(uint256 amount, address to) public override returns (uint256 stableCoinAmount) {
        require((stableCoinAmount = previewDeposit(amount)) != 0, "ZERO_SHARES");
        weth.transferFrom(msg.sender, address(this), amount);
        totalFloat += amount;

        _mint(to, stableCoinAmount);
        emit Deposit(msg.sender, to, amount, stableCoinAmount);
    }

    /// @notice Stablecoin
    function withdraw(
        uint256 amountStable,
        address to,
        address from
    ) public override returns (uint256 wethOut) {
        uint256 allowed = allowance[from][msg.sender];
        if (msg.sender != from && allowed != type(uint256).max) allowance[from][msg.sender] = allowed - amountStable;

        _burn(from, amountStable);
        wethOut = previewWithdraw(amountStable); // Calc wethOut for given amountStable
        totalFloat -= wethOut;

        emit Withdraw(from, to, amountStable, wethOut);

        weth.transferFrom(address(this), msg.sender, wethOut); // 0 approve earlier

        return wethOut;
    }

    /// @notice Stablecoin
    function previewDeposit(uint256 amount) public view override returns (uint256 stableCoinAmount) {
        uint256 latest = uint256(getLatestPrice());
        return (latest / 1e8) * amount; // (ETH/USD) * AMOUNT
    }

    /// @notice Stablecoin
    function previewWithdraw(uint256 amount) public view override returns (uint256 shares) {
        uint256 latest = uint256(getLatestPrice());
        return totalSupply == 0 ? amount : amount / (latest / 1e8); // AMOUNT / (ETH/USD)
    }

    /// @notice Volatility token
    function mint(uint256 amount, address to) public override returns (uint256 volCoinAmount) {
        weth.transfer(address(this), amount);
        volatile.mint(to, volCoinAmount = previewMint(amount));
        totalFloat += amount;
        emit Deposit(msg.sender, to, volCoinAmount, amount);
    }

    /// @notice Volatility token
    function redeem(
        uint256 amountVolCoin,
        address to,
        address from
    ) public override returns (uint256 wethOut) {
        uint256 allowed = volatile.allowance(from, msg.sender);

        /// You cant set allowance from this contract on external contract
        /// That's why Vault should only operate on underlying (add fund/defund funcs for volatile)
        /// External contract changing allowance
        if (msg.sender != from && allowed != type(uint256).max) {
            volatile.setAllowance(from, msg.sender, (allowed - amountVolCoin));
        }
        require((wethOut = previewRedeem(amountVolCoin)) != 0, "ZERO_ASSETS");

        wethOut = previewRedeem(amountVolCoin);
        _burn(from, amountVolCoin);
        totalFloat -= wethOut;

        emit Withdraw(from, to, wethOut, amountVolCoin);

        weth.transferFrom(address(this), msg.sender, wethOut);
    }

    /// @notice Volatility token
    function previewMint(uint256 amount) public view override returns (uint256 volCoinAmount) {
        uint256 latest = uint256(getLatestPrice());
        return (latest / 1e8) * amount; // (ETH/USD) * AMOUNT, SAME AS DEPOSIT
    }

    /// @notice Volatility token
    // Funding tranches, lock from redeem
    // Allow to withdraw from all pool share
    // Accrue interests
    function previewRedeem(uint256 amount) public view override returns (uint256 amounts) {
        return totalSupply == 0 ? amount : amount.mulDivDown(totalAssets(), totalSupply);
    }

    /*///////////////////////////////////////////////////////////////
                         INTERNAL HOOKS LOGIC
    //////////////////////////////////////////////////////////////*/

    function beforeWithdraw(uint256 amount) internal {}

    function afterDeposit(uint256 amount) internal {}

    /*///////////////////////////////////////////////////////////////
                        ACCOUNTING LOGIC
    //////////////////////////////////////////////////////////////*/

    function totalAssets() public view override returns (uint256) {
        return weth.balanceOf(address(this));
    }

    function assetsOf(address user) public view override returns (uint256) {
        return balanceOf[user];
    }

    function stableAssetsOf(address user) public view returns (uint256) {
        return balanceOf[user];
    }

    function volatileAssetsOf(address user) public view returns (uint256) {
        return volatile.balanceOf(user);
    }

    function assetsPerShare() public view override returns (uint256) {
        return previewRedeem(10**decimals);
    }

    function maxDeposit(address) public pure override returns (uint256) {
        return type(uint256).max;
    }

    function maxMint(address) public pure override returns (uint256) {
        return type(uint256).max;
    }

    function maxWithdraw(address user) public view override returns (uint256) {
        return assetsOf(user);
    }

    function maxRedeem(address user) public view override returns (uint256) {
        return balanceOf[user];
    }

    function getLatestPrice() public view returns (int256) {
        (uint80 roundID, int256 price, uint256 startedAt, uint256 timeStamp, uint80 answeredInRound) = priceFeed
            .latestRoundData();
        return price;
    }

    /*///////////////////////////////////////////////////////////////
            ETH WRAPPER LOGIC (split into other proxy contract)
            https://github.com/ethers-io/ethers.js/issues/1160
    //////////////////////////////////////////////////////////////*/
    // function deposit() external payable returns (uint256 stableCoinAmount) {
    // uint256 ethIn = msg.value;
    // if (ethIn > 0) revert NoEthSupplied();
    // weth.deposit{ value: msg.value }();
    // stableCoinAmount = deposit(ethIn, msg.sender);
    // }
    // function mint() external payable returns (uint256 volCoinAmount) {
    // uint256 ethIn = msg.value;
    // if (ethIn > 0) revert NoEthSupplied();
    // weth.deposit{ value: msg.value }();
    // volCoinAmount = mint(ethIn, msg.sender);
    // }
}
