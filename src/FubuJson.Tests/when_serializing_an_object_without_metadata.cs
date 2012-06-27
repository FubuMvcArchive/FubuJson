using FubuCore.Binding;
using FubuCore.Conversion;
using FubuTestingSupport;
using NUnit.Framework;
using Newtonsoft.Json;
using Rhino.Mocks;

namespace FubuJson.Tests
{
	[TestFixture]
	public class when_serializing_an_object_without_metadata
	{
		private NewtonSoftJsonSerializer theSerializer;
		private ComplexTypeConverter theConverter;
		private ParentType theTarget;
		private string theResult;

		[SetUp]
		public void SetUp()
		{
			theConverter = new ComplexTypeConverter(new ObjectConverter());
			theSerializer = new NewtonSoftJsonSerializer(new JsonConverter[] { theConverter }, MockRepository.GenerateStub<IObjectResolver>());

			theTarget = new ParentType
			            	{
			            		Name = "Test",
								Child = new ComplexType { Key = "x", Value = "123" }
			            	};

			theResult = theSerializer.Serialize(theTarget);
		}

		[Test]
		public void uses_the_provided_converters()
		{
			theResult.ShouldEqual("{\"Name\":\"Test\",\"Child\":\"x:123\"}");
		}
	}
}