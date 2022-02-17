import { ethers } from "hardhat";
import { BigNumber, Contract } from "ethers";
import { StableVault__factory } from "../typechain";
import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";

const WETH_ADDRESS = "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2";
const WETH_DEPOSIT = ["function deposit() payable", "function approve(address guy, uint wad) public returns (bool)"]
const WAD_AMOUNT = ethers.utils.parseEther("0.1");
const BIG_AMOUNT = ethers.utils.parseEther("500");

async function main(): Promise<void> {
  let signers: SignerWithAddress[];
  let vaultContract: Contract;

  const [deployer] = await ethers.getSigners();
  signers = await ethers.getSigners();

  const weth = new Contract(WETH_ADDRESS, WETH_DEPOSIT, deployer);
  await weth.deposit({value:BIG_AMOUNT});
  
  await deployVault();
  await initialLiquidity();
  await showBalances();

  async function showBalances() {
      const totalAssets = await vaultContract.totalAssets();
      const assetsOf = await vaultContract.assetsOf(deployer.address);
      console.log(ethers.utils.formatUnits(totalAssets, "ether"))
      console.log(ethers.utils.formatUnits(assetsOf, "ether"))
  }

  async function deployVault() {
    const vaultFactory = new StableVault__factory(deployer);
    vaultContract = await vaultFactory.deploy();
  }

  async function initialLiquidity() {
    await weth.approve(vaultContract.address, WAD_AMOUNT);
    await vaultContract.deposit(WAD_AMOUNT, deployer.address);
  }

}

main()
  .then(() => process.exit(0))
  .catch((error: Error) => {
    console.error(error);
    process.exit(1);
  });
