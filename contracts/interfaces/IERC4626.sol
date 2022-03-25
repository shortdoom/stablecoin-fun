// SPDX-License-Identifier: AGPL-3.0-only
pragma solidity ^0.8.0;

interface IERC4626 {
    /*///////////////////////////////////////////////////////////////
                                Events
    //////////////////////////////////////////////////////////////*/

    event Deposit(address indexed from, address indexed to, uint256 amount, uint256 shares);
    event Withdraw(address indexed from, address indexed to, uint256 amount, uint256 shares);


    /*///////////////////////////////////////////////////////////////
                            Mutable Functions
    //////////////////////////////////////////////////////////////*/

    function deposit(uint256 amount, address to) external virtual returns (uint256 shares);

    function mint(uint256 shares, address to) external virtual returns (uint256 underlyingAmount);

    function withdraw(
        uint256 amount,
        address to,
        address from
    ) external virtual returns (uint256 shares);

    function redeem(
        uint256 shares,
        address to,
        address from
    ) external virtual returns (uint256 amount);

    /*///////////////////////////////////////////////////////////////
                            View Functions
    //////////////////////////////////////////////////////////////*/

    function totalAssets() external view virtual returns (uint256);

    function assetsOf(address user) external view virtual returns (uint256);

    function assetsPerShare() external view virtual returns (uint256);

    function maxDeposit(address) external virtual returns (uint256);

    function maxMint(address) external virtual returns (uint256);

    function maxRedeem(address user) external view virtual returns (uint256);

    function maxWithdraw(address user) external view virtual returns (uint256);

    /**
      @notice Returns the amount of vault tokens that would be obtained if depositing a given amount of underlying tokens in a `deposit` call.
      @param underlyingAmount the input amount of underlying tokens
      @return shareAmount the corresponding amount of shares out from a deposit call with `underlyingAmount` in
     */
    function previewDeposit(uint256 underlyingAmount) external view virtual returns (uint256 shareAmount);

    /**
      @notice Returns the amount of underlying tokens that would be deposited if minting a given amount of shares in a `mint` call.
      @param shareAmount the amount of shares from a mint call.
      @return underlyingAmount the amount of underlying tokens corresponding to the mint call
     */
    function previewMint(uint256 shareAmount) external view virtual returns (uint256 underlyingAmount);

    /**
      @notice Returns the amount of vault tokens that would be burned if withdrawing a given amount of underlying tokens in a `withdraw` call.
      @param underlyingAmount the input amount of underlying tokens
      @return shareAmount the corresponding amount of shares out from a withdraw call with `underlyingAmount` in
     */
    function previewWithdraw(uint256 underlyingAmount) external view virtual returns (uint256 shareAmount);

    /**
      @notice Returns the amount of underlying tokens that would be obtained if redeeming a given amount of shares in a `redeem` call.
      @param shareAmount the amount of shares from a redeem call.
      @return underlyingAmount the amount of underlying tokens corresponding to the redeem call
     */
    function previewRedeem(uint256 shareAmount) external view virtual returns (uint256 underlyingAmount);
}
