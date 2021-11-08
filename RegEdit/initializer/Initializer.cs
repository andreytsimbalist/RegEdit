using System.Collections.Generic;
using Microsoft.Win32;

namespace RegEdit.initializer
{
    public static class Initializer
    {
        private static readonly List<RegistryKey> HKeysEntries = new List<RegistryKey>
        {
            
            Registry.ClassesRoot,
            Registry.CurrentUser,
            Registry.LocalMachine,
            Registry.Users,
            Registry.CurrentConfig,
            Registry.PerformanceData
        };

        public static RegItem Init()
        {
            var root = RegItem.RootItem("Computer");
            foreach (var entry in HKeysEntries)
            {
                root.Items.Add(new RegItem(entry));
            }

            return root;
        }
    }
}