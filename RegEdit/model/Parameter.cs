using System;

namespace RegEdit
{
    [Serializable]
    public class Parameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Parameter(string name, string type, string value)
        {
            if (name.Equals(""))
            {
                name = "(Default)";
            }
            Name = name;
            Type = type;
            Value = value;
        }
        
        public Parameter()
        {
        }
    }
}