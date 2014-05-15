using System;
namespace ReservationsServer.Logging
{
	internal sealed class ConsoleLogger
		: FileLogger, ILogger
	{
		internal ConsoleLogger(ILogger decoratedLogger = null)
			: base(Console.Out, decoratedLogger)
		{
			_decoratedLogger = decoratedLogger;
		}

		internal static ILogger Instance
		{
			get
			{
				lock (_instanceLock)
					if (_instance == null)
						_instance = new ConsoleLogger();

				return _instance;
			}
		}

		private readonly ILogger _decoratedLogger;
		private static ILogger _instance = null;
		private static readonly object _instanceLock = new object();
	}
}