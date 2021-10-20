using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace RegEdit
{
    public class RegItem : TreeViewItem
    {
        public List<Parameter> Parameters { get; set; }

        public RegItem(string name)
        {
            Header = name;
            Parameters = new List<Parameter>();
        }
    }
}