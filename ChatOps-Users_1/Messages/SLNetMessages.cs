namespace ChatOps_Users_1.Messages
{
	using Skyline.DataMiner.Automation;
	using Skyline.DataMiner.Net.Messages;

	public static class SLNetMessages
	{
		public static DMSMessage[] GetClientList(IEngine engine)
		{
			var getInfoMessage = new GetInfoMessage(InfoType.ClientList);
			return engine.SendSLNetMessage(getInfoMessage);
		}

		public static GetUserInfoResponseMessage GetSecurityInfo(IEngine engine)
		{
			var getInfoMessage = new GetInfoMessage(InfoType.SecurityInfo);
			return engine.SendSLNetSingleResponseMessage(getInfoMessage) as GetUserInfoResponseMessage;
		}
	}
}