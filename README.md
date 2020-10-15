﻿# BoomerBot #
## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes, also the steps to configure to your discord application bot.

## Prerequisites
To build the project it is necessary to have installed .NET 3.0 version. You can install it on the following link:


Also is needed create a application and a bot on Discord. You can do it [here](https://discord.com/developers/applications). For more information about how to create a bot on discord, you can check the [this guide](https://discordpy.readthedocs.io/en/latest/discord.html).

## Configuration
The first thing you should do is configure your appsettings.Developmnent.json with your Discord Application Token. You can do it just editing the appsettings.json file
```
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "DiscordToken": "YOUR TOKEN HERE",
  "DiscordCommandPrefix": "okb!"```
```
You can find tour token on the Bots section of your application settings at https://discord.com/developers/ applications. Remember that you should **NOT** share the bot token with anyone.


## Run
Just compile and execute the project and wait for the _Ok boomer_ on http://localhost:4437 . If you dont know how to execute it, just put the following command on the terminal:
```
dotnet build --configuration Developmnent
```

## Functions
Currently, the following commands has been implemented:
* **(Ping) Say ok** -> The bot will anwer "boomer" to you.
* **Say Boomer** -> The bot will insult you.
* **Include an negative adjective on your message** -> The bot will insult you.
* **Cry with _Llorón_ rol** -> The bot will insult you.


## Developed by
* [Paula (Latra) Gallucci Zurita](https://github.com/latra)
