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

DATE		VERSION		AUTHOR			    COMMENTS

14/06/2023	1.0.0.1		JAY & JSL, Skyline	Initial version
****************************************************************************
*/

namespace Show_Connected_Users_1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using AdaptiveCards;
	using ChatOps_Users_1.Groups;
	using ChatOps_Users_1.Logger;
	using ChatOps_Users_1.Users;
	using Newtonsoft.Json;
	using Skyline.DataMiner.Automation;

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
				// Get all info
				var userInfo = Group.GetUserNameToGroupsNameMap(engine);
				var connectedUsers = User.GetConnectedUsersByFullName(engine, userInfo);

				GenerateUI(engine, connectedUsers);
			}
			catch (Exception ex)
			{
				Log.ErrorMessage(engine, "Something went wrong when trying to get all connected users", ex);
			}
		}

		private static void GenerateUI(IEngine engine, List<User> connectedUsers)
		{
			var card = GenerateCardContent(connectedUsers);
			var table = GenerateTableContent(connectedUsers);

			var adaptiveCardBody = new List<AdaptiveElement>();
			adaptiveCardBody.AddRange(card);

			if (connectedUsers.Count > 0)
			{
				adaptiveCardBody.Add(table);
			}

			engine.AddScriptOutput("AdaptiveCard", JsonConvert.SerializeObject(adaptiveCardBody));
		}

		private static List<AdaptiveElement> GenerateCardContent(List<User> connectedUsers)
		{
			return new List<AdaptiveElement>
					{
						new AdaptiveTextBlock($"At the moment there are {connectedUsers.Count} active users.") { Wrap = true },
					};
		}

		private static AdaptiveTable GenerateTableContent(List<User> connectedUsers)
		{
			var table = new AdaptiveTable
			{
				Type = "Table",
				FirstRowAsHeaders = true,
				Columns = new List<AdaptiveTableColumnDefinition>
				{
					new AdaptiveTableColumnDefinition
					{
						Width = 100,
					},
					new AdaptiveTableColumnDefinition
					{
						Width = 250,
					},
					new AdaptiveTableColumnDefinition
					{
						Width = 100,
					},
				},
				Rows = new List<AdaptiveTableRow>
				{
					new AdaptiveTableRow
					{
						Type = "TableRow",
						Cells = new List<AdaptiveTableCell>
						{
							new AdaptiveTableCell
							{
								Type = "TableCell",
								Items = new List<AdaptiveElement>
								{
									new AdaptiveTextBlock("Name")
									{
										Type = "TextBlock",
										Weight = AdaptiveTextWeight.Bolder,
									},
								},
							},
							new AdaptiveTableCell
							{
								Type = "TableCell",
								Items = new List<AdaptiveElement>
								{
									new AdaptiveTextBlock("Group(s)")
									{
										Type = "TextBlock",
										Weight = AdaptiveTextWeight.Bolder,
									},
								},
							},
							new AdaptiveTableCell
							{
								Type = "TableCell",
								Items = new List<AdaptiveElement>
								{
									new AdaptiveTextBlock("Client")
									{
										Type = "TextBlock",
										Weight = AdaptiveTextWeight.Bolder,
									},
								},
							},
						},
					},
				},
			};
			foreach (var user in connectedUsers)
			{
				var row = new AdaptiveTableRow
				{
					Type = "TableRow",
					Cells = new List<AdaptiveTableCell>
					{
						new AdaptiveTableCell
						{
							Type = "TableCell",
							Items = new List<AdaptiveElement>
							{
								new AdaptiveTextBlock(user.UserName)
								{
									Type = "TextBlock",
									Wrap = true,
								},
							},
						},
						new AdaptiveTableCell
						{
							Type = "TableCell",
							Items = user.GroupNames.Select(x => new AdaptiveTextBlock(x)
								{
									Type = "TextBlock",
									Wrap = true,
								}).ToList<AdaptiveElement>(),
						},
						new AdaptiveTableCell
						{
							Type = "TableCell",
							Items = new List<AdaptiveElement>
							{
								new AdaptiveTextBlock(user.ConnectionName)
								{
									Type = "TextBlock",
									Wrap = true,
								},
							},
						},
					},
				};

				table.Rows.Add(row);
			}

			return table;
		}
	}
}