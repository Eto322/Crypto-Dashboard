# Crypto Dashboard Project

## Overview

The **Crypto Dashboard** project is designed to provide users with comprehensive information about various cryptocurrencies. It integrates with multiple APIs to fetch data, deserialize it, and present it in a user-friendly manner.

## Table of Contents

- [Implemented Functionality](#implemented-functionality)
  - [Data Access Layer (DAL)](#data-access-layer-dal)
  - [Business Logic Layer (BLL)](#business-logic-layer-bll)
  - [Models](#models)
  - [ViewModel](#viewmodel)
- [API Key Configuration](#api-key-configuration)


## Implemented Functionality

### Data Access Layer (DAL)

#### ApiClients

1. **CoinCapApi**
   - **Constructor**: Initializes the API client with an API key and sets up the HTTP client with the necessary headers.
   - **SearchCoinCap**: Searches for cryptocurrencies on CoinCap based on a query.
   - **GetTopCryptos**: Retrieves the top cryptocurrencies from CoinCap.
   - **GetResponse**: Handles HTTP requests and responses.

2. **CoinGeckoApi**
   - **Constructor**: Initializes the API client with an API key and sets up the HTTP client with the necessary headers.
   - **GetCandelsById**: Fetches candlestick data for a specific cryptocurrency.
   - **GetMarketInfoOnCoin**: Retrieves market information for a specific coin.
   - **SearchCoins**: Searches for cryptocurrencies on CoinGecko based on a query.
   - **GetTopCryptos**: Retrieves the top cryptocurrencies from CoinGecko.
   - **GetCryptoFullInfoById**: Fetches full information for a specific cryptocurrency.
   - **GetCryptoDetailsById**: Retrieves detailed information for a list of cryptocurrencies.

#### Credentials

1. **CredentialManager**
   - **GetGeckoApiKey**: Retrieves the API key for CoinGecko.
   - **GetCoinCapApiKey**: Retrieves the API key for CoinCap.

### Business Logic Layer (BLL)

#### Manager

1. **CryptoInfoManager**
   - **Constructor**: Initializes various API clients and deserializers using credentials.
   - **GetTopNCryptos**: Fetches and merges the top N cryptocurrencies from both CoinCap and CoinGecko.
   - **GetSearchCryptocurrencies**: Searches and merges cryptocurrency data from both CoinCap and CoinGecko based on a query.
   - **GetAdditionalInfo**: Retrieves additional information about a specific cryptocurrency.
   - **GetCandelsById**: Fetches candlestick data for a specific cryptocurrency.
   - **GetExchangeModels**: Retrieves exchange information for a specific cryptocurrency.

#### Deserializers

1. **CryptocurrencyDeserializer**
   - **Deserialize**: Deserializes cryptocurrency data from JSON.

2. **AdditionalCoinInfoDeserializer**
   - **Deserialize**: Deserializes additional coin information from JSON.

3. **CandlesDeserializer**
   - **Deserialize**: Deserializes candlestick data from JSON.

4. **ExchangeDeserializer**
   - **Deserialize**: Deserializes exchange data from JSON.

### Models

1. **CryptocurrencyModel**: Represents a cryptocurrency.
2. **AdditionalCoinInfoModel**: Represents additional information about a cryptocurrency.
3. **CandlestickModel**: Represents candlestick data for a cryptocurrency.
4. **ExchangeModel**: Represents exchange information for a cryptocurrency.

### View Models (VM)

#### ViewModel

1. **CryptoDashboardViewModel**
   - **Constructor**: Initializes the view model with necessary services and commands.
   - **Cryptocurrencies**: Observable collection of cryptocurrency models to be displayed in the dashboard.
   - **SelectedCryptocurrency**: The currently selected cryptocurrency in the dashboard.
   - **SearchQuery**: The query string used for searching cryptocurrencies.
   - **SearchCommand**: Command to execute the search functionality based on the search query.
   - **LoadTopCryptosCommand**: Command to load the top cryptocurrencies.
   - **LoadAdditionalInfoCommand**: Command to load additional information for the selected cryptocurrency.
   - **LoadCandlestickDataCommand**: Command to load candlestick data for the selected cryptocurrency.
   - **LoadExchangeDataCommand**: Command to load exchange data for the selected cryptocurrency.

#### Commands

1. **SearchCommand**
   - **Execute**: Executes the search functionality to find cryptocurrencies based on the search query.
   - **CanExecute**: Determines if the search command can be executed.

2. **LoadTopCryptosCommand**
   - **Execute**: Loads the top cryptocurrencies and updates the observable collection.
   - **CanExecute**: Determines if the load top cryptos command can be executed.

3. **LoadAdditionalInfoCommand**
   - **Execute**: Loads additional information for the selected cryptocurrency.
   - **CanExecute**: Determines if the load additional info command can be executed.

4. **LoadCandlestickDataCommand**
   - **Execute**: Loads candlestick data for the selected cryptocurrency.
   - **CanExecute**: Determines if the load candlestick data command can be executed.

5. **LoadExchangeDataCommand**
   - **Execute**: Loads exchange data for the selected cryptocurrency.
   - **CanExecute**: Determines if the load exchange data command can be executed.

#### Properties

1. **Cryptocurrencies**: Represents a collection of cryptocurrencies to be displayed in the view.
2. **SelectedCryptocurrency**: Represents the currently selected cryptocurrency in the view.
3. **SearchQuery**: Represents the search query entered by the user.
4. **IsLoading**: Represents the loading state of the view model.

#### Methods

1. **SearchCryptocurrencies**: Searches for cryptocurrencies based on the search query and updates the observable collection.
2. **LoadTopCryptos**: Loads the top cryptocurrencies and updates the observable collection.
3. **LoadAdditionalInfo**: Loads additional information for the selected cryptocurrency.
4. **LoadCandlestickData**: Loads candlestick data for the selected cryptocurrency.
5. **LoadExchangeData**: Loads exchange data for the selected cryptocurrency.

## API Key Configuration

To use the APIs, you need to provide your API keys in the `appsettings.json` file. Here is an example configuration:

```json
{
  "ApiSettings": {
    "CoinGeckoApiKey": "Your_Key",
    "CoinCapApiKey": "Your_Key"
  }
}
```

You can obtain your API keys from the following sources:

- CoinCap: [CoinCap API Documentation](https://docs.coincap.io/)
- CoinGecko: [CoinGecko API Documentation](https://www.coingecko.com/en/api)