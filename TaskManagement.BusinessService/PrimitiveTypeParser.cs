using System.ComponentModel;

namespace TaskManagement.BusinessService
{
    public static class PrimitiveTypeParser
    {
        public static T Parse<T>(string valueAsString)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            var result = converter.ConvertFromString(valueAsString);
            return (T)result;
        }
    }
}