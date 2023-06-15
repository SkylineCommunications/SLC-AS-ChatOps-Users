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

14/06/2023	1.0.0.1		JAY, Skyline	Initial version
****************************************************************************
*/

namespace Show_Connected_Users_1
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using AdaptiveCards;
	using Newtonsoft.Json;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.PerformanceIndication;
	using Skyline.DataMiner.Net;
	using Skyline.DataMiner.Net.Messages;
	using System.Linq;

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
			var card = new List<AdaptiveElement>
			{
				new AdaptiveTextBlock($"Below you can find the list of all the users connected and via which client.") { Wrap = true },
			};

			var getInfoMessage = new GetInfoMessage(InfoType.ClientList);
			DMSMessage[] response = engine.SendSLNetMessage(getInfoMessage);
			int counter = 0;
			int responsesCount = response.Count();
			bool adminRecordFoundHTML5App = false;

			card.Add(new AdaptiveTextBlock($"Found {responsesCount} response(s) in total of type 'LoginInfoResponseMessage'."));

			foreach (Skyline.DataMiner.Net.Messages.LoginInfoResponseMessage responseMessage in response)
			{
				if (responseMessage.FriendlyName.ToLower().Contains("cube"))
				{
					card.Add(new AdaptiveTextBlock($"Via Cube: {responseMessage.FullName}"));
					counter++;
				};
				if (responseMessage.FriendlyName.ToLower().Contains("html5"))
				{
					if (!adminRecordFoundHTML5App && responseMessage.FullName.ToLower().Contains("administrator")
					{
						adminRecordFoundHTML5App = true;
						continue;
					}
					card.Add(new AdaptiveTextBlock($"Via HTML5 App: {responseMessage.FullName}"));
					counter++;
				};
			}

			card.Add(new AdaptiveTextBlock($"Found {counter} user(s) in total."));

			engine.AddScriptOutput("AdaptiveCard", JsonConvert.SerializeObject(card));
		}
	}
}