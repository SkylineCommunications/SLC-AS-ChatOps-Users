namespace Show_Connected_Users_1.Logger
{
	using System;
	using Skyline.DataMiner.Automation;

	public static class Log
	{
		public static void ErrorMessage(IEngine engine, string error, Exception exception)
		{
			engine.Log($"{error}: {exception}");
			engine.ExitFail($"{error}: {exception.Message}");
		}
	}
}