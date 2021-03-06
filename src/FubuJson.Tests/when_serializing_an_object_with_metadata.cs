using FubuCore;
using FubuCore.Conversion;
using FubuTestingSupport;
using NUnit.Framework;
using Newtonsoft.Json;

namespace FubuJson.Tests
{
	[TestFixture]
	public class when_serializing_an_object_with_metadata
	{
		private NewtonSoftJsonSerializer theSerializer;
		private ComplexTypeConverter theConverter;
		private ParentType theTarget;
		private string theResult;

		[SetUp]
		public void SetUp()
		{
			theConverter = new ComplexTypeConverter(new ObjectConverter());
			theSerializer = new NewtonSoftJsonSerializer(new JsonSerializerSettings(), new JsonConverter[] { theConverter });

			theTarget = new ParentType
			            	{
			            		Name = "Test",
			            		Child = new ComplexType { Key = "x", Value = "123" }
			            	};

			theResult = theSerializer.Serialize(theTarget, true);
		}

		[Test]
		public void uses_the_provided_converters()
		{
			var targetType = typeof(ParentType);
			var type = "\"$type\":\"{0}, {1}\"".ToFormat(targetType.FullName, targetType.Assembly.GetName().Name);
			theResult.ShouldEqual("{" + type + ",\"Name\":\"Test\",\"Child\":\"x:123\"}");
		}
	}
}