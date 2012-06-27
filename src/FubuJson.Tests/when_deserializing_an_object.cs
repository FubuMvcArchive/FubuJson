using FubuCore.Binding;
using FubuTestingSupport;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rhino.Mocks;

namespace FubuJson.Tests
{
	[TestFixture]
	public class when_deserializing_an_object
	{
		private NewtonSoftJsonSerializer theSerializer;
		private IObjectResolver theObjectResolver;
		private string theInput;
		private ParentType theObject;

		[SetUp]
		public void SetUp()
		{
			theObjectResolver = MockRepository.GenerateStub<IObjectResolver>();
			theInput = "{\"Name\":\"Test\",\"Child\":\"x:123\"}";
			theSerializer = new NewtonSoftJsonSerializer(new JsonConverter[0], theObjectResolver);

			theObject = new ParentType
			{
				Name = "Test",
				Child = new ComplexType { Key = "x", Value = "123" }
			};

			theObjectResolver
				.Stub(x => x.BindModel(typeof (ParentType), new JObjectValues(new JObject())))
				.Constraints(Rhino.Mocks.Constraints.Is.Same(typeof(ParentType)), Rhino.Mocks.Constraints.Is.TypeOf<JObjectValues>())
				.Return(new BindResult {Value = theObject});
		}

		[Test]
		public void uses_the_object_resolver()
		{
			theSerializer.Deserialize<ParentType>(theInput).ShouldEqual(theObject);
		}
	}
}