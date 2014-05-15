using System.Collections.Generic;
using System.Diagnostics;
namespace ReservationsServer.Logging
{
	internal sealed class TraceLogger
		: ILogger
	{
		internal TraceLogger(ILogger decoratedLogger = null)
		{
			_decoratedLogger = decoratedLogger;
		}

		#region ILogger Members
		public void WriteLine()
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLine();

			WriteLine(string.Empty);
		}

		public void Write(string message)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message);

			Trace.Write(message);
		}
		public void WriteLine(string message)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message);

			Trace.WriteLine(message);
		}

		public void Write(string message, string category)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message, category);

			Trace.Write(message, category);
		}
		public void WriteLine(string message, string category)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message, category);

			Trace.WriteLine(message, category);
		}

		public void WriteFormat(string format, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, argument);

			Trace.Write(string.Format(format, argument));
		}
		public void WriteLineFormat(string format, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, argument);

			Trace.WriteLine(string.Format(format, argument));
		}

		public void WriteFormat(string format, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, argument1, argument2);

			Trace.Write(string.Format(format, argument1, argument2));
		}
		public void WriteLineFormat(string format, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, argument1, argument2);

			Trace.WriteLine(string.Format(format, argument1, argument2));
		}

		public void WriteFormat(string format, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, argument1, argument2, argument3);

			Trace.Write(string.Format(format, argument1, argument2, argument3));
		}
		public void WriteLineFormat(string format, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, argument1, argument2, argument3);

			Trace.WriteLine(string.Format(format, argument1, argument2, argument3));
		}

		public void WriteFormat(string format, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, arguments);

			Trace.Write(string.Format(format, arguments));
		}
		public void WriteLineFormat(string format, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, arguments);

			Trace.WriteLine(string.Format(format, arguments));
		}

		public void WriteFormat(string format, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, arguments);

			Trace.Write(string.Format(format, arguments));
		}
		public void WriteLineFormat(string format, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, arguments);

			Trace.WriteLine(string.Format(format, arguments));
		}

		public void WriteFormat(string format, string category, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, argument);

			Trace.Write(string.Format(format, argument), category);
		}
		public void WriteLineFormat(string format, string category, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, argument);

			Trace.WriteLine(string.Format(format, argument), category);
		}

		public void WriteFormat(string format, string category, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, argument1, argument2);

			Trace.Write(string.Format(format, argument1, argument2), category);
		}
		public void WriteLineFormat(string format, string category, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, argument1, argument2);

			Trace.WriteLine(string.Format(format, argument1, argument2), category);
		}

		public void WriteFormat(string format, string category, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, argument1, argument2, argument3);

			Trace.Write(string.Format(format, argument1, argument2, argument3), category);
		}
		public void WriteLineFormat(string format, string category, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, argument1, argument2, argument3);

			Trace.WriteLine(string.Format(format, argument1, argument2, argument3), category);
		}

		public void WriteFormat(string format, string category, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, arguments);

			Trace.Write(string.Format(format, arguments), category);
		}
		public void WriteLineFormat(string format, string category, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, arguments);

			Trace.WriteLine(string.Format(format, arguments), category);
		}

		public void WriteFormat(string format, string category, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, arguments);

			Trace.Write(string.Format(format, arguments), category);
		}
		public void WriteLineFormat(string format, string category, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, arguments);

			Trace.WriteLine(string.Format(format, arguments), category);
		}
		#endregion
		internal static ILogger Instance
		{
			get
			{
				lock (_instanceLock)
					if (_instance == null)
						_instance = new TraceLogger();

				return _instance;
			}
		}

		private readonly ILogger _decoratedLogger;
		private static ILogger _instance = null;
		private static readonly object _instanceLock = new object();
	}
}