# Primitive Stablecoin

Based HEAVILY on job done by https://github.com/usmfum/USM team. This is vulnerable and not usable in production version of USM stablecoin. Utilizes same mint<>fund logic with two tokens, where minters sell risk and funders buy risk. Bascially, you should read through series of brillant medium articles written by @jacob-eliosoff explaining how minimalistic stablecoin is build.

Proposed implementation strips all of the useful and usable mechanics of minimal stablecoin to present most primitive blueprint of it (If you still had problems, like me, to wrap your head around USM, it may help to see it's core functions through EIP-4626). It translates core functionality of USM to ERC-4626 previewFunctions and demonstrates how one can use newly proposed standard to build their own stable currency.

# Stablecoin-4626 Notes

There are three easy/obvious routes to build stablecoin using 4626.

1. Break interface and build two-token vault with shares used as volatility/funding token. Suprisingly, interface MUST enforce singular underlying for uniformity.
2. Keep single underlying token (reserve) assumption and extend 4626 with fund/defund functions, operating on volatility token separately (Eg. USM stablecoin)
3. Build two Vaults, for stable and volatility token each. Build router between two Vaults, connecting through previewFunctions.

This project chooses to keep ERC-4626 expected behavior (Route no. 2).

## Usage

Set `.env` and run `npx hardhat node` then `npx hardhat run scripts/StableVault.ts --network localhost`

### Pre Requisites

Before running any command, make sure to install dependencies:

```sh
$ yarn install
```

### Compile

Compile the smart contracts with Hardhat:

```sh
$ yarn compile
```

### Test

Run the Mocha tests:

```sh
$ yarn test
```

### Deploy contract to netowrk (requires Mnemonic and infura API key)

```
npx hardhat run --network rinkeby ./scripts/deploy.ts
```

### Validate a contract with etherscan (requires API ke)

```
npx hardhat verify --network <network> <DEPLOYED_CONTRACT_ADDRESS> "Constructor argument 1"
```

### Added plugins

- Gas reporter [hardhat-gas-reporter](https://hardhat.org/plugins/hardhat-gas-reporter.html)
- Etherscan [hardhat-etherscan](https://hardhat.org/plugins/nomiclabs-hardhat-etherscan.html)