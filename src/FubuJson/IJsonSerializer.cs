namespace FubuJson
{
	public interface IJsonSerializer
	{
		string Serialize(object target, bool includeMetadata = false);
		T Deserialize<T>(string input);
	}
}