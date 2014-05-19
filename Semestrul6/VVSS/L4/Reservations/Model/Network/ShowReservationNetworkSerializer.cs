using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Reservations.Model.Network
{
	public class ShowReservationNetworkSerializer
		: INetworkSerializer<ShowReservation>
	{
		#region INetworkSerializer<ShowReservation> Members
		public string Serialize(ShowReservation showReservation)
		{
			if (showReservation == null)
				throw new ArgumentNullException("showReservation");

			return new StringBuilder().Append("{ShowReservation:")
									  .Append(_showNetworkSerializer.Serialize(showReservation.Show).Trim())
									  .Append(',')
									  .Append(showReservation.HallLocation.ToString())
									  .Append(",\"")
									  .Append(showReservation.HallPlacement.Replace("\"", "\\\""))
									  .Append("\"}\n")
									  .ToString();
		}
		public ShowReservation Deserialize(string serializedShowReservation)
		{
			if (serializedShowReservation == null)
				throw new ArgumentNullException("serializedShowReservation");

			Match match = Regex.Match(serializedShowReservation, @"\s*\{ShowReservation:(?<show>\{Show:""(?("")(?<=\\)""|.)*"",\d{4}-\d{2}-\d{2}\}),(?<hallLocation>" + string.Join("|", Enum.GetValues(typeof(HallLocation)).Cast<HallLocation>().Select(hallLocation => hallLocation.ToString())) + @"),""(?<hallPlacement>(?("")(?<=\\)""|.)*)""\}\s*");
			if (!match.Success)
				return null;

			return new ShowReservation(_showNetworkSerializer.Deserialize(match.Groups["show"].Value),
									   (HallLocation)Enum.Parse(typeof(HallLocation), match.Groups["hallLocation"].Value),
									   match.Groups["hallPlacement"].Value.Replace("\\\"", "\""));

		}
		#endregion

		private static readonly INetworkSerializer<Show> _showNetworkSerializer = new ShowNetworkSerializer();
	}
}