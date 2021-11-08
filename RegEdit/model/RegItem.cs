using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Win32;
using RegEdit.util;

namespace RegEdit
{
    public class RegItem : TreeViewItem
    {
        public List<Parameter> Parameters { get; }

        public RegistryKey Key { get; }

        private bool _isLoad;
        private bool _isPartLoad;

        public RegItem(RegistryKey key)
        {
            Key = key;
            var split = key.Name.Split('\\');
            Header = split[split.Length - 1];
            Parameters = InitParameters(key);
            _isLoad = false;
            _isPartLoad = false;
        }

        public void Load()
        {
            if (_isLoad) return;
            this.Items.Clear();
            var keyNames = FuncHandleUtils.Execute(Key.GetSubKeyNames, new string[0]);
            foreach (var names in keyNames)
            {
                var subKey = FuncHandleUtils.Execute(() => Key.OpenSubKey(names), null);
                if (subKey == null)
                {
                    continue;
                }
                var subReg = new RegItem(subKey);
                subReg.LoadOnce();
                this.Items.Add(subReg);
            }
            _isLoad = true;
        }

        private void LoadOnce()
        {
            var keyNames = FuncHandleUtils.Execute(Key.GetSubKeyNames, new string[0]);
            foreach (var names in keyNames)
            {
                var subKey = FuncHandleUtils.Execute(() => Key.OpenSubKey(names), null);
                if (subKey == null)
                {
                    continue;
                }
                var subReg = new RegItem(subKey);
                this.Items.Add(subReg);
            }
        }

        private static List<Parameter> InitParameters(RegistryKey key)
        {
            var names = FuncHandleUtils.Execute(key.GetValueNames, new string[0]);
            return
            (
                from valueName in names
                let type = FuncHandleUtils.Execute(() => key.GetValueKind(valueName).ToString(), "unknown")
                let value = FuncHandleUtils.Execute(() => key.GetValue(valueName).ToString(), "unknown")
                select new Parameter(valueName, type, value)
            ).ToList();
        }

        private RegItem(string name)
        {
            Key = null;
            Header = name;
            Parameters = new List<Parameter>();
            _isLoad = true;
            _isPartLoad = true;
        }

        public static RegItem RootItem(string rootName)
        {
            return new RegItem(rootName);
        }
    }
}