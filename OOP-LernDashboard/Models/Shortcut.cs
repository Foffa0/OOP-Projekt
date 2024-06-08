using System.IO;

namespace OOP_LernDashboard.Models
{
    enum ShortcutType
    {
        Link,
        Application
    }
    internal class Shortcut
    {
        public Guid Id { get; }
        public ShortcutType Type { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }

        public Shortcut(ShortcutType type, string path, string name, string iconPath)
        {
            Id = Guid.NewGuid();
            Type = type;
            Path = path;
            Name = name;
            IconPath = iconPath;
        }

        public Shortcut(Guid id, ShortcutType type, string path, string name, string iconPath)
        {
            Id = id;
            Type = type;
            Path = path;
            Name = name;
            IconPath = iconPath;
        }

        /// <summary>
        /// Returns whether the given Path is valid
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValidPath(string path, ShortcutType type)
        {
            if (type == ShortcutType.Link)
            {
                if (Uri.TryCreate(path, UriKind.Absolute, out Uri uriResult))
                {
                    return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
                }
                return false;
            }
            else if (type == ShortcutType.Application)
            {
                return File.Exists(path);
            }
            return false;
        }
    }
}
