using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FubuJson
{
	public class NewtonSoftJsonSerializer : IJsonSerializer
	{
		private readonly IEnumerable<JsonConverter> _converters;

		public NewtonSoftJsonSerializer(IEnumerable<JsonConverter> converters)
		{
			_converters = converters;
		}

		private JsonSerializer buildSerializer(TypeNameHandling naming)
		{
			var jsonSerializer = new JsonSerializer
			{
				TypeNameHandling = naming
			};
			jsonSerializer.Converters.AddRange(_converters);
			
			return jsonSerializer;
		}

		public string Serialize(object target, bool includeMetadata = false)
		{
			var naming = includeMetadata ? TypeNameHandling.All : TypeNameHandling.None;
			var stringWriter = new StringWriter();
			var writer = new JsonTextWriter(stringWriter);

			var serializer = buildSerializer(naming);
			serializer.Serialize(writer, target);

			return stringWriter.ToString();
		}

		public T Deserialize<T>(string input)
		{
			var serializer = buildSerializer(TypeNameHandling.All);
			return serializer.Deserialize<T>(new JsonTextReader(new StringReader(input)));
		}
	}
}