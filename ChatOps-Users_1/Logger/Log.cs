namespace ChatOps_Users_1.Logger
{
	using System;
	using Skyline.DataMiner.Automation;

	public static class Log
	{
		public static void ErrorMessage(IEngine engine, string error, Exception exception)
		{
			engine.Log($"{error}: {exception.Message}");
			engine.ExitFail($"{error}: {exception.Message}");
		}
	}
}