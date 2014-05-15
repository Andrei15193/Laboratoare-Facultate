namespace Reservations.Model.Network
{
	public interface INetworkSerializer<TItem>
	{
		string Serialize(TItem item);
		TItem Deserialize(string serializedItem);
	}
}