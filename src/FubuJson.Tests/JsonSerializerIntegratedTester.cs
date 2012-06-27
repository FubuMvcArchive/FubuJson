using FubuCore.Binding;
using FubuTestingSupport;
using NUnit.Framework;
using Newtonsoft.Json;

namespace FubuJson.Tests
{
	[TestFixture]
	public class JsonSerializerIntegratedTester
	{
		[Test]
		public void blah()
		{
			var resolver = ObjectResolver.Basic();
			var serializer = new NewtonSoftJsonSerializer(new JsonConverter[0], resolver);
			var original = new ParentType
							{
								Name = "Hello",
								Child = new ComplexType { Key = "x", Value = "abc" }
							};

			var json = serializer.Serialize(original);
			serializer.Deserialize<ParentType>(json).ShouldEqual(original);
		}
	}
}