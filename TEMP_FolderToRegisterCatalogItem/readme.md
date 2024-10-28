# ChatOps Extensions about Users

This solution consists of two commands, so called *ChatsOps Extensions* that will help you to display or inform all online users in a convenient way via Microsoft Teams and our very own [DataMiner Teams Bot](https://docs.dataminer.services/user-guide/Cloud_Platform/TeamsBot/DataMiner_Teams_bot.html). 

## Pre-requisites

Kindly ensure that your DataMiner System (DMS) and your Microsoft Teams adhere to the pre-requisites described in [DM Docs](https://docs.dataminer.services/user-guide/Cloud_Platform/TeamsBot/Microsoft_Teams_Chat_Integration.html#server-side-prerequisites).

## Show Connected Users

With the command **Show Connected Users** you can easily display all users that are currently connected to the DataMiner System (DMS) via Cube, along with an indication of the single or multiple groups to which each user belongs.

![Animation of the command to show all connected users](./images/ShowConnectedUsersExample.gif)

## Inform Online Users

With the command **Inform Online Users** you can broadcast a message that is defined as an input parameter.

![Animation of the command to inform online users](./images/RunInformOnlineUsers.gif)

All online users will see a message appear on their Cube application in a pop-up. The user who executed the command will get a confirmation from the Teams Bot that the massage has been broadcasted.

![Example of broadcasted message in a message box](./images/BroadcastedMessagePoppingUpAtAllOnlineUsers.gif)