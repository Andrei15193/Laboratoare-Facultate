using System;
using System.Collections.Generic;
using System.IO;
namespace ReservationsServer.Logging
{
	internal class FileLogger
		: ILogger
	{
		internal FileLogger(TextWriter textWriter, ILogger decoratedLogger = null)
		{
			if (textWriter == null)
				throw new ArgumentNullException("textWriter");

			_textWriter = textWriter;
			_decoratedLogger = decoratedLogger;
		}
		internal FileLogger(string path, ILogger decoratedLogger = null)
			: this(new StreamWriter(path))
		{
		}

		#region ILogger Members
		public void WriteLine()
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLine();

			_textWriter.WriteLine();
		}

		public void Write(string message)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message);

			_textWriter.Write(message);
		}
		public void WriteLine(string message)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message);

			_textWriter.WriteLine(message);
		}

		public void Write(string message, string category)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message, category);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.Write(message);
		}
		public void WriteLine(string message, string category)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.Write(message, category);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.WriteLine(message);
		}

		public void WriteFormat(string format, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, argument);

			_textWriter.Write(string.Format(format, argument));
		}
		public void WriteLineFormat(string format, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, argument);

			_textWriter.WriteLine(string.Format(format, argument));
		}

		public void WriteFormat(string format, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, argument1, argument2);

			_textWriter.Write(string.Format(format, argument1, argument2));
		}
		public void WriteLineFormat(string format, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, argument1, argument2);

			_textWriter.WriteLine(string.Format(format, argument1, argument2));
		}

		public void WriteFormat(string format, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, argument1, argument2, argument3);

			_textWriter.Write(string.Format(format, argument1, argument2, argument3));
		}
		public void WriteLineFormat(string format, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, argument1, argument2, argument3);

			_textWriter.WriteLine(string.Format(format, argument1, argument2, argument3));
		}

		public void WriteFormat(string format, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, arguments);

			_textWriter.Write(string.Format(format, arguments));
		}
		public void WriteLineFormat(string format, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, arguments);

			_textWriter.WriteLine(string.Format(format, arguments));
		}

		public void WriteFormat(string format, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, arguments);

			_textWriter.Write(string.Format(format, arguments));
		}
		public void WriteLineFormat(string format, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, arguments);

			_textWriter.WriteLine(string.Format(format, arguments));
		}

		public void WriteFormat(string format, string category, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, argument);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.Write(string.Format(format, argument));
		}
		public void WriteLineFormat(string format, string category, object argument)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, argument);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.WriteLine(string.Format(format, argument));
		}

		public void WriteFormat(string format, string category, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, argument1, argument2);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.Write(string.Format(format, argument1, argument2));
		}
		public void WriteLineFormat(string format, string category, object argument1, object argument2)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, argument1, argument2);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.WriteLine(string.Format(format, argument1, argument2));
		}

		public void WriteFormat(string format, string category, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, argument1, argument2, argument3);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.Write(string.Format(format, argument1, argument2, argument3));
		}
		public void WriteLineFormat(string format, string category, object argument1, object argument2, object argument3)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, argument1, argument2, argument3);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.WriteLine(string.Format(format, argument1, argument2, argument3));
		}

		public void WriteFormat(string format, string category, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, arguments);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.Write(string.Format(format, arguments));
		}
		public void WriteLineFormat(string format, string category, params object[] arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, arguments);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.WriteLine(string.Format(format, arguments));
		}

		public void WriteFormat(string format, string category, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteFormat(format, category, arguments);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.Write(string.Format(format, arguments));
		}
		public void WriteLineFormat(string format, string category, IEnumerable<object> arguments)
		{
			if (_decoratedLogger != null)
				_decoratedLogger.WriteLineFormat(format, category, arguments);

			_textWriter.Write(category);
			_textWriter.Write(": ");
			_textWriter.WriteLine(string.Format(format, arguments));
		}
		#endregion

		private readonly ILogger _decoratedLogger;
		private readonly TextWriter _textWriter;
	}
}