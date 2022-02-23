import { ethers } from "hardhat";
import { BigNumber, Contract, Signer } from "ethers";
import { StableVault__factory } from "../typechain";
import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";

const WETH_ADDRESS = "0xC02aaA39b223FE8D0A0e5C4F27eAD9083C756Cc2";
const WETH_DEPOSIT = ["function deposit() payable", "function approve(address guy, uint wad) public returns (bool)"]
const WAD_AMOUNT = ethers.utils.parseEther("1");
const BIG_AMOUNT = ethers.utils.parseEther("10");

async function main(): Promise<void> {
  let signers: SignerWithAddress[];
  let vaultContract: Contract;

  const [deployer] = await ethers.getSigners();
  signers = await ethers.getSigners();

  const weth = new Contract(WETH_ADDRESS, WETH_DEPOSIT, deployer);
  
  for (let i = 0; i < signers.length; i++) {
    weth.connect(signers[i]);
    await weth.deposit({value:BIG_AMOUNT});
  }

  async function mineBlocks() {
    for (let index = 0; index < 500; index++) {
      // console.log("mining block", index);
      await ethers.provider.send("evm_mine", []);
    }
  }
  
  await deployVault();
  await mintFirstStable();
  await mintMultipleStable();
  await mintFunding();
  await defunding();

  async function showBalances(user: SignerWithAddress) {
      const totalAssets = await vaultContract.totalAssets();
      const assetsOf = await vaultContract.assetsOf(user.address);
      console.log("Reserve WETH (in Vault)", ethers.utils.formatUnits(totalAssets, "ether"))
      console.log("User minted STABLE", ethers.utils.formatUnits(assetsOf, "ether"))
      console.log("\n");
  }

  async function deployVault() {
    const vaultFactory = new StableVault__factory(deployer);
    vaultContract = await vaultFactory.deploy();
  }

  async function mintFirstStable() {
    await weth.approve(vaultContract.address, WAD_AMOUNT);
    await vaultContract.deposit(WAD_AMOUNT, deployer.address);
    await showBalances(deployer);
  }

  async function mintMultipleStable() {
    for (let i = 0; i < signers.length; i++) {
      weth.connect(signers[i]);
      await weth.approve(vaultContract.address, WAD_AMOUNT);
      await vaultContract.deposit(WAD_AMOUNT, signers[i].address);
      // await showBalances(signers[i]);
    }
    // await mineBlocks();
  }

  async function mintFunding() {
    const volCoinAmount = ethers.utils.parseEther("3000");
    for (let i = 2; i < signers.length; i++) {
      const wethIn = await vaultContract.previewFund(volCoinAmount);
      weth.connect(signers[i]);
      await weth.approve(vaultContract.address, wethIn);
      await vaultContract.fund(volCoinAmount, signers[i].address);
      await showBalances(signers[i]);
    }
  }

  async function defunding() {
    const volCoinAmount = ethers.utils.parseEther("3000");
    for (let i = 2; i < signers.length; i++) {
      const wethOut = await vaultContract.previewDefund(volCoinAmount);
      console.log("Defund amount", ethers.utils.formatUnits(wethOut, "ether"));
    }
  }

}

main()
  .then(() => process.exit(0))
  .catch((error: Error) => {
    console.error(error);
    process.exit(1);
  });
