using System.Collections.Generic;
namespace ReservationsServer.Logging
{
	internal interface ILogger
	{
		void WriteLine();

		void Write(string message);
		void WriteLine(string message);

		void Write(string message, string category);
		void WriteLine(string message, string category);

		void WriteFormat(string format, object argument);
		void WriteLineFormat(string format, object argument);

		void WriteFormat(string format, object argument1, object argument2);
		void WriteLineFormat(string format, object argument1, object argument2);

		void WriteFormat(string format, object argument1, object argument2, object argument3);
		void WriteLineFormat(string format, object argument1, object argument2, object argument3);

		void WriteFormat(string format, params object[] arguments);
		void WriteLineFormat(string format, params object[] arguments);

		void WriteFormat(string format, IEnumerable<object> arguments);
		void WriteLineFormat(string format, IEnumerable<object> arguments);

		void WriteFormat(string format, string category, object argument);
		void WriteLineFormat(string format, string category, object argument);

		void WriteFormat(string format, string category, object argument1, object argument2);
		void WriteLineFormat(string format, string category, object argument1, object argument2);

		void WriteFormat(string format, string category, object argument1, object argument2, object argument3);
		void WriteLineFormat(string format, string category, object argument1, object argument2, object argument3);

		void WriteFormat(string format, string category, params object[] arguments);
		void WriteLineFormat(string format, string category, params object[] arguments);

		void WriteFormat(string format, string category, IEnumerable<object> arguments);
		void WriteLineFormat(string format, string category, IEnumerable<object> arguments);
	}
}