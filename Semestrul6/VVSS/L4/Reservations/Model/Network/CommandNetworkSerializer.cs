using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Reservations.Model.Network
{
	public class CommandNetworkSerializer<TParameter>
		: INetworkSerializer<Command<TParameter>>
	{
		public CommandNetworkSerializer(INetworkSerializer<TParameter> parameterNetworkSerializer)
		{
			if (parameterNetworkSerializer == null)
				throw new ArgumentNullException("parameterNetworkSerializer");

			_parameterNetworkSerializer = parameterNetworkSerializer;
		}

		#region INetworkSerializer<Command<TParameter>> Members
		public string Serialize(Command<TParameter> command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			return new StringBuilder().Append('{')
									  .Append(command.Name)
									  .Append(':')
									  .Append(string.Join(",", command.Parameters
																	  .Select(parameter => _parameterNetworkSerializer.Serialize(parameter)
																													  .Trim())))
									  .Append("}\n")
									  .ToString();
		}
		public Command<TParameter> Deserialize(string serializedCommand)
		{
			Match match = Regex.Match(serializedCommand, @"^\s*\{(?<commandName>[_\w][_\w\d]*):");

			if (!match.Success)
				return null;

			return new Command<TParameter>(match.Groups["commandName"].Value,
										   _GetParameters(serializedCommand, match.Length).Select(serializedParameter => _parameterNetworkSerializer.Deserialize(serializedParameter))
																						  .ToList());
		}
		#endregion

		private IEnumerable<string> _GetParameters(string serializedCommandParameters, int startIndex = 0)
		{
			int openCurlyBracketCount = 1;
			int parameterStartIndex = startIndex, currentIndex = startIndex;
			IList<string> commandParameters = new List<string>();

			while (currentIndex < serializedCommandParameters.Length && openCurlyBracketCount > 0)
			{
				switch (serializedCommandParameters[currentIndex])
				{
					case '{':
						if (openCurlyBracketCount == 1)
							parameterStartIndex = currentIndex;
						openCurlyBracketCount++;
						break;
					case '}':
						openCurlyBracketCount--;
						if (openCurlyBracketCount == 1)
						{
							commandParameters.Add(serializedCommandParameters.Substring(parameterStartIndex, currentIndex - parameterStartIndex + 1));
							parameterStartIndex = currentIndex;
						}
						break;
					default:
						break;
				}
				currentIndex++;
			}

			if (openCurlyBracketCount > 0)
				throw new ArgumentException("Not a valid parameters sequence!", "serializedCommandParameters");
			return commandParameters;
		}

		private readonly INetworkSerializer<TParameter> _parameterNetworkSerializer;
	}
}