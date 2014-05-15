using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ReservationsServer.Logging
{
	internal static class Logger
	{
		internal static ILogger Instance
		{
			get
			{
				lock (_instanceLock)
					return _instance;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("Logger");

				lock (_instanceLock)
					_instance = value;
			}
		}
		internal static void AwaitLogging()
		{
			lock (_logTaskLock)
				if (_logTask != null)
				{
					_logTask.Wait();
					_logTask = null;
				}
		}

		internal static void WriteLine()
		{
			lock (_instanceLock)
				_instance.WriteLine();
		}
		internal static Task WriteLineAsync()
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(new Action(WriteLine));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLine());

				return _logTask;
			}
		}

		internal static void Write(string message)
		{
			lock (_instanceLock)
				_instance.Write(message);
		}
		internal static void WriteLine(string message)
		{
			lock (_instanceLock)
				_instance.WriteLine(message);
		}
		internal static Task WriteAsync(string message)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => Write(message));
				else
					_logTask = _logTask.ContinueWith((task) => Write(message));

				return _logTask;
			}
		}
		internal static Task WriteLineAsync(string message)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLine(message));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLine(message));

				return _logTask;
			}
		}

		internal static void Write(string message, string category)
		{
			lock (_instanceLock)
				_instance.Write(message, category);
		}
		internal static void WriteLine(string message, string category)
		{
			lock (_instanceLock)
				_instance.WriteLine(message, category);
		}
		internal static Task WriteAsync(string message, string category)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => Write(message, category));
				else
					_logTask = _logTask.ContinueWith((task) => Write(message, category));

				return _logTask;
			}
		}
		internal static Task WriteLineAsync(string message, string category)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLine(message, category));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLine(message, category));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, object argument)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, argument);
		}
		internal static void WriteLineFormat(string format, object argument)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, argument);
		}
		internal static Task WriteFormatAsync(string format, object argument)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, argument));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, argument));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, object argument)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, argument));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, argument));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, object argument1, object argument2)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, argument1, argument2);
		}
		internal static void WriteLineFormat(string format, object argument1, object argument2)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, argument1, argument2);
		}
		internal static Task WriteFormatAsync(string format, object argument1, object argument2)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, argument1, argument2));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, argument1, argument2));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, object argument1, object argument2)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, argument1, argument2));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, argument1, argument2));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, object argument1, object argument2, object argument3)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, argument1, argument2, argument3);
		}
		internal static void WriteLineFormat(string format, object argument1, object argument2, object argument3)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, argument1, argument2, argument3);
		}
		internal static Task WriteFormatAsync(string format, object argument1, object argument2, object argument3)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, argument1, argument2, argument3));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, argument1, argument2, argument3));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, object argument1, object argument2, object argument3)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, argument1, argument2, argument3));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, argument1, argument2, argument3));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, params object[] arguments)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, arguments);
		}
		internal static void WriteLineFormat(string format, params object[] arguments)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, arguments);
		}
		internal static Task WriteFormatAsync(string format, params object[] arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, arguments));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, params object[] arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, arguments));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, IEnumerable<object> arguments)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, arguments);
		}
		internal static void WriteLineFormat(string format, IEnumerable<object> arguments)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, arguments);
		}
		internal static Task WriteFormatAsync(string format, IEnumerable<object> arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, arguments));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, IEnumerable<object> arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, arguments));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, string category, object argument)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, category, argument);
		}
		internal static void WriteLineFormat(string format, string category, object argument)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, category, argument);
		}
		internal static Task WriteFormatAsync(string format, string category, object argument)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, category, argument));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, category, argument));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, string category, object argument)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, category, argument));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, category, argument));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, string category, object argument1, object argument2)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, category, argument1, argument2);
		}
		internal static void WriteLineFormat(string format, string category, object argument1, object argument2)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, category, argument1, argument2);
		}
		internal static Task WriteFormatAsync(string format, string category, object argument1, object argument2)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, category, argument1, argument2));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, category, argument1, argument2));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, string category, object argument1, object argument2)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, category, argument1, argument2));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, category, argument1, argument2));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, string category, object argument1, object argument2, object argument3)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, category, argument1, argument2, argument3);
		}
		internal static void WriteLineFormat(string format, string category, object argument1, object argument2, object argument3)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, category, argument1, argument2, argument3);
		}
		internal static Task WriteFormatAsync(string format, string category, object argument1, object argument2, object argument3)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, category, argument1, argument2, argument3));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, category, argument1, argument2, argument3));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, string category, object argument1, object argument2, object argument3)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, category, argument1, argument2, argument3));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, category, argument1, argument2, argument3));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, string category, params object[] arguments)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, category, arguments);
		}
		internal static void WriteLineFormat(string format, string category, params object[] arguments)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, category, arguments);
		}
		internal static Task WriteFormatAsync(string format, string category, params object[] arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, category, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, category, arguments));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, string category, params object[] arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, category, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, category, arguments));

				return _logTask;
			}
		}

		internal static void WriteFormat(string format, string category, IEnumerable<object> arguments)
		{
			lock (_instanceLock)
				_instance.WriteFormat(format, category, arguments);
		}
		internal static void WriteLineFormat(string format, string category, IEnumerable<object> arguments)
		{
			lock (_instanceLock)
				_instance.WriteLineFormat(format, category, arguments);
		}
		internal static Task WriteFormatAsync(string format, string category, IEnumerable<object> arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteFormat(format, category, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteFormat(format, category, arguments));

				return _logTask;
			}
		}
		internal static Task WriteLineFormatAsync(string format, string category, IEnumerable<object> arguments)
		{
			lock (_logTaskLock)
			{
				if (_logTask == null)
					_logTask = Task.Run(() => WriteLineFormat(format, category, arguments));
				else
					_logTask = _logTask.ContinueWith((task) => WriteLineFormat(format, category, arguments));

				return _logTask;
			}
		}

		private static ILogger _instance = DebugLogger.Instance;
		private static readonly object _instanceLock = new object();
		private static Task _logTask = null;
		private static readonly object _logTaskLock = new object();
	}
}