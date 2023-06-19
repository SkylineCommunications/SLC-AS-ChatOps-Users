namespace Library.Groups
{
    using System.Collections.Generic;
    using Library.Messages;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Net.Messages;

    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Dictionary<string, List<string>> GetUserNameToGroupsNameMap(IEngine engine)
        {
            var groups = new Dictionary<int, Group>();
            var response = SLNetMessages.GetSecurityInfo(engine);
            ProcessGroups(groups, response);

            var userInfo = new Dictionary<string, List<string>>();
            ProcessUsers(groups, response, userInfo);

            return userInfo;
        }

        private static void ProcessGroups(Dictionary<int, Group> groups, GetUserInfoResponseMessage response)
        {
            foreach (var group in response.Groups)
            {
                groups[group.ID] = new Group
                {
                    Id = group.ID,
                    Name = group.Name,
                };
            }
        }

        private static void ProcessUsers(Dictionary<int, Group> groups, GetUserInfoResponseMessage response, Dictionary<string, List<string>> userInfo)
        {
            foreach (var user in response.Users)
            {
                var groupNames = new List<string>();
                foreach (var groupId in user.Groups)
                {
                    if (groups.TryGetValue(groupId, out Group group))
                    {
                        groupNames.Add(group.Name);
                    }
                }

                userInfo[user.FullName] = groupNames;
            }
        }
    }
}