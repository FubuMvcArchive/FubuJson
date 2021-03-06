using FubuCore.Conversion;

namespace FubuJson.Tests
{
	public class ComplexTypeConverter : FubuJsonConverter<ComplexType>
	{
		public ComplexTypeConverter(IObjectConverter converter) 
			: base(converter)
		{
		}

		protected override string toJsonValue(ComplexType value)
		{
			return value.ToString();
		}
	}
}