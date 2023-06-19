# SLC-AS-ChatOps-Users

This repository contains an automation script solution with scripts that can be used to retrieve Element information from your DataMiner system using the DataMiner Teams bot.

The following scrips are currently available:

- [Show Connected Users](#Show-Connected-Users)

- [Inform Online Users](#Inform-Online-Users)

## Pre-requisites

Kindly ensure that your DataMiner system and your Microsoft Teams adhere to the pre-requisites described in [DM Docs](https://docs.dataminer.services/user-guide/Cloud_Platform/TeamsBot/Microsoft_Teams_Chat_Integration.html#server-side-prerequisites).

## Show Connected Users

Automation script that returns all users currently connected to the connected DMS (DataMiner System) via Cube. Moreover, the single or multiple groups to which this user belongs are also indicated. 

![Animation of the command to show all connected users](/Documentation/ShowConnectedUsersExample.gif)


## Inform Online Users

Automation script that enables the user to inform all online users. A broadcast message can be defined as an input parameter. All online users will see a message appear on their Cube application in a message box. 

![Example of broadcasted message in a message box](/Documentation/ExampleBroadcastedMessageInAMessageBox.png)