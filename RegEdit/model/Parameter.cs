namespace RegEdit
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Parameter(string name, string type, string value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }
}