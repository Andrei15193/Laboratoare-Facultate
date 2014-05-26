using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Reservations.Model
{
	public class Command<TParameter>
	{
		public Command(string name, IEnumerable<TParameter> parameters)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Cannot be empty or whitespace!", "name");
			if (!Regex.IsMatch(name, @"^\s*[_a-zA-Z][_a-zA-Z0-9]*\s*$"))
				throw new ArgumentException("Must be a valid identifier!", "name");

			if (parameters == null)
				throw new ArgumentNullException("parameters");

			_name = name.Trim();
			_parameters = parameters.ToList();
		}
		public Command(string commandName, params TParameter[] parameters)
			: this(commandName, (IEnumerable<TParameter>)parameters)
		{
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}
		public IReadOnlyList<TParameter> Parameters
		{
			get
			{
				return _parameters;
			}
		}

		private readonly string _name;
		private readonly IReadOnlyList<TParameter> _parameters;
	}
}