using System.ComponentModel;

namespace SharedKernel.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription<T>(T enumValue)
        {
            if (enumValue == null)
                return "";

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
    }
}
