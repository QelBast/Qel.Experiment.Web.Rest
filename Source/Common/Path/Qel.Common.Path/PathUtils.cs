using System.Reflection;

namespace Qel.Common.Path;

using Path = System.IO.Path;

public static class PathUtils
{
    public static string GetExecutingAssemblyPath()
    {
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
            throw new DirectoryNotFoundException("Папка исполняемой сборки не была найдена");
    }
}
