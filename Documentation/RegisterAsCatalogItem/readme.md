# ChatOps Extensions about Users

This solution consists of commands or "ChatOps Extensions" that allow you to interact with a DataMiner System using the [DataMiner Teams bot](https://docs.dataminer.services/user-guide/Cloud_Platform/TeamsBot/DataMiner_Teams_bot.html).

> [!TIP]
> See more info on how you can create your own Custom Chats Operator in the tutorial video [Kata #6: Custom ChatOps operator](https://community.dataminer.services/courses/kata-6/) on DataMiner Dojo.

## Available scripts

The following scripts are currently available:

- [Show Connected Users](#show-connected-users)
- [Inform Online Users](#inform-online-users)

### Show Connected Users

This Automation script returns all users who are currently using DataMiner Cube to connect to the connected DMS. The group or groups that each user is a member of are also indicated. 

![Animation of the command to show all connected users](./images/ShowConnectedUsersExample.gif)

### Inform Online Users

This Automation script enables users to inform all online users with a message. A broadcast message can be defined as an input parameter. The user running the script will get a confirmation from the Teams bot when the message has been broadcast. 

![Animation of the command to inform online users](./images/RunInformOnlineUsers.gif)

All online users will see a pop-up message appear in DataMiner Cube.

![Example of broadcasted message in a message box](./images/BroadcastedMessagePoppingUpAtAllOnlineUsers.gif)

## Prerequisites

Make sure that your DMS and your Microsoft Teams installation meet the prerequisites described on [DataMiner Docs](https://aka.dataminer.services/ChatOps-server-side-prerequisites).