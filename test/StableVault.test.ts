import { ethers } from "hardhat";
import chai from "chai";
import { solidity } from "ethereum-waffle";
import { StableVault__factory } from "../typechain";
import { BigNumber, Contract } from "ethers";
import { SignerWithAddress } from "@nomiclabs/hardhat-ethers/signers";

chai.use(solidity);
const { expect } = chai;

describe("StableCoin", async () => {
  let VaultContract: Contract;
  let signers: SignerWithAddress[];

  const [deployer, user1, user2] = await ethers.getSigners();
  signers = [deployer, user1, user2];

  before("Deploy Vault contract", async () => {
    const vaultFactory = new StableVault__factory(signers[0]);
    VaultContract = await vaultFactory.deploy();
  });

  describe("Mint", async () => {

    it("Deposit 500 ETH as intial liquidity for Stablecoin", async () => {
      
    });
  
    it("Mint 50 ETH of VolCoin", async () => {
      
    });
  
  });
});
