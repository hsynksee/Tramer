using HandlebarsDotNet;
using System.Reflection;

namespace SharedKernel.Helpers
{
    public static class RenderHelper
    {
        public static string RenderTemplateWithHtml(string htmlContent, object data)
        {
            var template = Handlebars.Compile(htmlContent);
            return template(data);
        }

        public static string RenderTemplateWithLayout(Assembly assembly, string templateFolderName, string name, object data)
        {
            var template = GetCompiledTemplate(assembly, templateFolderName, name);
            return template(data);
        }

        private static HandlebarsTemplate<object, object> GetCompiledTemplate(Assembly assembly, string templateFolderName, string name)
        {
            var attrs = assembly.GetCustomAttributesData().Single(a => a.AttributeType == typeof(AssemblyTitleAttribute));
            var resource = attrs.ConstructorArguments.First().Value;
            string resourceName = $"{resource}.{templateFolderName}.{name}.html";
            string partialSource = string.Empty;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    partialSource = reader.ReadToEnd();
                }
            }
            return Handlebars.Compile(partialSource);
        }
    }
}
