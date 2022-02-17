# Stablecoin Notes

Architecture notes:

There are three routes to build stablecoin using 4626.

1. Break interface and build two-token vault with shares used as volatility/funding token. Suprisingly, interface MUST enforce singular underlying for uniformity.
2. Keep single underlying token (reserve) assumption and extend 4626 with fund/defund functions, operating on volatility token separately (Eg. USM stablecoin)
3. Build two Vaults, for stable and volatility token each. Build router between two Vaults, connecting through previewFunctions.

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