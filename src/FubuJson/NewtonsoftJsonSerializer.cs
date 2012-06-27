using System.Collections.Generic;
using System.IO;
using FubuCore.Binding;
using Newtonsoft.Json;

namespace FubuJson
{
	public class NewtonSoftJsonSerializer : IJsonSerializer
	{
		private readonly IEnumerable<JsonConverter> _converters;
		private readonly IObjectResolver _resolver;

		public NewtonSoftJsonSerializer(IEnumerable<JsonConverter> converters, IObjectResolver resolver)
		{
			_converters = converters;
			_resolver = resolver;
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
			var values = new JObjectValues(input);
			return (T)_resolver.BindModel(typeof(T), values).Value;
		}
	}
}