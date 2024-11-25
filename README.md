# SLC-AS-ChatOps-Users

This repository contains an Automation script solution with scripts that can be used to interact with a DataMiner System (DMS) using the DataMiner Teams bot.

The following scripts are currently available:

- [Show Connected Users](#Show-Connected-Users)

- [Inform Online Users](#Inform-Online-Users)

## Prerequisites

Make sure that your DMS and your Microsoft Teams installation meet the prerequisites described on [DataMiner Docs](https://aka.dataminer.services/ChatOps-server-side-prerequisites).

## Show Connected Users

This Automation script returns all users who are currently using DataMiner Cube to connect to the connected DMS. The group or groups that each user is a member of are also indicated. 

![Animation of the command to show all connected users](/Documentation/ShowConnectedUsersExample.gif)


## Inform Online Users

This Automation script enables users to inform all online users with a message. A broadcast message can be defined as an input parameter. The user running the script will get a confirmation from the Teams bot when the message has been broadcast. 

![Animation of the command to inform online users](/Documentation/RunInformOnlineUsers.gif)

All online users will see a pop-up message appear in DataMiner Cube.

![Example of broadcasted message in a message box](/Documentation/BroadcastedMessagePoppingUpAtAllOnlineUsers.gif)
