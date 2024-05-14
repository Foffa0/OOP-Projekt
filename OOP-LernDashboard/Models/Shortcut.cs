using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
