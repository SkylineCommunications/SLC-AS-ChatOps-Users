/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

16/06/2023	1.0.0.1		JSL & JAY, Skyline	Initial version
****************************************************************************
*/

namespace Inform_Online_Users_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using AdaptiveCards;
	using Library.Logger;
	using Library.Users;
	using Newtonsoft.Json;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Broadcast;
	using Skyline.DataMiner.Net.Messages;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			try
			{
				var messageToSend = engine.GetScriptParam("MessageToBroadcast").Value;
				var connectedUsers = User.GetConnectedUsersByName(engine);

				SendPopUpMessageToOnlineUsers(engine, messageToSend, connectedUsers);
				GenerateUI(engine);
			}
			catch (Exception ex)
			{
				Log.ErrorMessage(engine, "Something went wrong when trying to inform all connected users", ex);
				GenerateUI(engine, false);
			}
		}

		private static void SendPopUpMessageToOnlineUsers(IEngine engine, string messageToSend, List<User> connectedUsers)
		{
			var popup = new BroadcastPopupRequestMessage
			{
				PopupInfo = new PopupInfo
				{
					Expiration = DateTime.Now.AddHours(1),
					Message = messageToSend,
					Source = Guid.NewGuid(),
					Title = "Coming from Teams Chat Bot",
				},
			};

			popup.PopupInfo.UserNames = connectedUsers.Select(x => x.UserName).ToList();
			popup.PopupInfo.GroupNames = new List<string>() { "none" };

			engine.SendSLNetMessage(popup);
		}

		private static void GenerateUI(IEngine engine, bool succeed = true)
		{
			var card = GenerateCardContent(succeed);

			var adaptiveCardBody = new List<AdaptiveElement>();
			adaptiveCardBody.AddRange(card);

			engine.AddScriptOutput("AdaptiveCard", JsonConvert.SerializeObject(adaptiveCardBody));
		}

		private static List<AdaptiveElement> GenerateCardContent(bool succeed)
		{
			string message = succeed ? "Message was sent to all on-line users." : "A problem occurred when sending the message to all on-line users.";

			return new List<AdaptiveElement>
					{
						new AdaptiveTextBlock(message) { Wrap = true },
					};
		}
	}
}