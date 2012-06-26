using System;
using FubuCore;
using FubuCore.Conversion;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuJson.Tests
{
	[TestFixture]
	public class when_deserializing_an_object
	{
		private NewtonSoftJsonSerializer theSerializer;
		private string theInput;
		private ParentType theObject;

		[SetUp]
		public void SetUp()
		{
			var locator = new InMemoryServiceLocator();
			var objectConverter = new ObjectConverter(locator, new ConverterLibrary(new[] {new StatelessComplexTypeConverter()}));
			locator.Add<IObjectConverter>(objectConverter);

			var converter = new ComplexTypeConverter(objectConverter);

			theInput = "{\"Name\":\"Test\",\"Child\":\"x:123\"}";
			theSerializer = new NewtonSoftJsonSerializer(new[] { converter });

			theObject = theSerializer.Deserialize<ParentType>(theInput);
		}

		[Test]
		public void uses_the_object_converter()
		{
			theObject.Name.ShouldEqual("Test");
			theObject.Child.ShouldEqual(new ComplexType {Key = "x", Value = "123"});
		}
	}

	public class StatelessComplexTypeConverter : StatelessConverter<ComplexType>
	{
		protected override ComplexType convert(string text)
		{
			var values = text.Split(new[] { ":" }, StringSplitOptions.None);
			return new ComplexType {Key = values[0], Value = values[1]};
		}
	}
}