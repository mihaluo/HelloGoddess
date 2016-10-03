using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HelloGoddess.Infrastructure.Reflection
{
    internal static class AssemblyHelper
    {
        public static List<Assembly> GetAllAssembliesInFolder(string folderPath, SearchOption searchOption)
        {
            var assemblyFiles = Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));

            return null;//todo yw 
            //return assemblyFiles.Select(Assembly.Load()).ToList();
        }
    }
}
