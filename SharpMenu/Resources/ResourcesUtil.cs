
using System.Reflection;

namespace SharpMenu.Resources
{
    internal static class ResourcesUtil
    {
        internal static byte[] ReadResource(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var consolaFontPath = resourcePath;
            using var stream = assembly.GetManifestResourceStream(consolaFontPath);
            return ReadFully(stream);
        }

        private static byte[] ReadFully(Stream input)
        {
            using var memoryStream = new MemoryStream();
            input.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
