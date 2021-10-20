using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace RegEdit.utils
{
    public static class FileManager
    {
        private const string PATH = "save.json";

        public static void save(RegItem root)
        {
            var data = FillData(root);
            var jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(PATH, jsonString);
        }

        public static RegItem load()
        {
            var jsonString = File.ReadAllText(PATH);
            var data =  JsonSerializer.Deserialize<Data>(jsonString);
            return FillRegItem(data);
        }

        private static RegItem FillRegItem(Data root)
        {
            RegItem regItem = new RegItem(root.Header);
            regItem.Parameters = root.Parameters;
            if (root.Children.Count > 0)
            {
                foreach (Data child in root.Children)
                {
                    regItem.Items.Add(FillRegItem(child));
                }
            }
            return regItem;
        }

        private static Data FillData(RegItem regItem)
        {
            Data data = new Data();
            data.Header = regItem.Header.ToString();
            data.Parameters = regItem.Parameters;
            data.Children = new List<Data>();
            if (regItem.Items.Count > 0)
            {
                foreach (RegItem child in regItem.Items)
                {
                    data.Children.Add(FillData(child));
                }
            }
            return data;
        }
    }
}