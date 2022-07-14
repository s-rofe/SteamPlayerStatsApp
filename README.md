# SteamPlayerStatsApp

This app provides API to retrieve information about a Steam User and their total playtime for games.

**Note:** The players Steam profile must be public in order to retrieve the data

## Getting Started
This application is build with **.Net 6**

Please clone this repo and create a text file named 'Keys.txt' in the project directory.

In `Keys.txt` write `Steam Key: {Your Steam API key}` on the first line. 

`{Your Steam API key}` should be your access key to Steam API's. 

![Steam Key Example](/Images/ReadMe/KeyFileExample.jpg)

To find out more about Steam API and get an access key click [here](https://steamcommunity.com/dev)

## Usage
The application can be run from the command line, and API's accessed using Swagger at port 8080 [localhost:8080](https://localhost:8080/swagger/index.html).

**OR**

The application can be run in Visual Studio.
## API's
![Steam Key Example](/Images/ReadMe/SwaggerV1.jpg)

### GetSteamData

*Param: string steamID*

Returns: A JSON reprosentation of the SteamData object that contains the response from the Steam API.

This response includes information about the games in a Steam user's library, including a total count of games and playtimes for various devices.

![Steam Data Response](/Images/ReadMe/SteamDataResponse.jpg)

### GetTotalPlayTime

*Param: string steamID*

Returns: The total playtime for all games across all devices for a user as an *int*.

### GetTotalGames

*Param: string steamID*

Returns: The total number of games in the users Steam library as an *int*.
