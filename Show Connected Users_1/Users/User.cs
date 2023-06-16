namespace Show_Connected_Users_1.Users
{
	using System.Collections.Generic;
	using System.Linq;
	using Show_Connected_Users_1.Messages;
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Messages;

	public class User
	{
		public string UserName { get; set; }

		public List<string>	GroupNames { get; set; }

		public string ConnectionName { get; set; }

		public static List<User> GetConnectedUsers(IEngine engine, Dictionary<string, List<string>> userInfo)
		{
			var users = new List<User>();
			var responses = SLNetMessages.GetClientList(engine);

			foreach (var response in responses.Cast<LoginInfoResponseMessage>())
			{
				if (response.FriendlyName.ToLower().StartsWith("cube"))
				{
					users.Add(new User
					{
						UserName = response.FullName,
						GroupNames = userInfo.TryGetValue(response.FullName, out List<string> groupNames) ? groupNames : new List<string>(),
						ConnectionName = "Cube",
					});
				}
			}

			users.Sort((user1, user2) => string.Compare(user1.UserName, user2.UserName));
			return users;
		}
	}
}