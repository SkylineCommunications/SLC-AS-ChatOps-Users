namespace Library.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Library.Messages;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Net.Messages;

    public class User
    {
        public string UserName { get; set; }

        public List<string> GroupNames { get; set; }

        public string ConnectionName { get; set; }

        public static List<User> GetConnectedUsersByFullName(IEngine engine, Dictionary<string, List<string>> userInfo)
        {
            var users = new Dictionary<string, User>();
            var responses = SLNetMessages.GetClientList(engine);

            foreach (var response in responses.Cast<LoginInfoResponseMessage>())
            {
                if (response.FriendlyName.ToLower().StartsWith("cube"))
                {
                    users[response.FullName] = new User
                    {
                        UserName = response.FullName,
                        GroupNames = userInfo.TryGetValue(response.FullName, out List<string> groupNames) ? groupNames : new List<string>(),
                        ConnectionName = "Cube",
                    };
                }
            }

            var sortedUsers = users.OrderBy(kvp => kvp.Value.UserName).Select(kvp => kvp.Value).ToList();
            return sortedUsers;
        }

        public static List<User> GetConnectedUsersByName(IEngine engine)
        {
            var users = new Dictionary<string, User>();
            var responses = SLNetMessages.GetClientList(engine);

            foreach (var response in responses.Cast<LoginInfoResponseMessage>())
            {
                if (response.FriendlyName.ToLower().StartsWith("cube"))
                {
                    users[response.Name] = new User
                    {
                        UserName = response.Name,
                        GroupNames = new List<string>(),
                        ConnectionName = "Cube",
                    };
                }
            }

            return users.Values.ToList();
        }
    }
}