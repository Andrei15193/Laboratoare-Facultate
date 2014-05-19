using System;
using System.Text;
using System.Text.RegularExpressions;
namespace Reservations.Model.Network
{
	public class ShowNetworkSerializer
		: INetworkSerializer<Show>
	{
		#region INetworkSerializer<Show> Members
		public string Serialize(Show show)
		{
			if (show == null)
				throw new ArgumentNullException("show");

			return new StringBuilder().Append("{Show:\"")
									  .Append(show.Name.Replace("\"", "\\\""))
									  .Append("\",")
									  .Append(show.Scheduled.ToString("yyyy-MM-dd"))
									  .Append("}\n")
									  .ToString();
		}
		public Show Deserialize(string serializedItem)
		{
			if (serializedItem == null)
				throw new ArgumentNullException("serializedItem");

			Match match = Regex.Match(serializedItem, @"\s*\{Show:""(?<showName>(?("")(?<=\\)""|.)*)"",(?<showSchedule>\d{4}-\d{2}-\d{2})\}\s*");
			if (!match.Success)
				return null;

			return new Show(match.Groups["showName"].Value.Replace("\\\"", "\""), DateTime.ParseExact(match.Groups["showSchedule"].Value, "yyyy-MM-dd", null));
		}
		#endregion
	}
}