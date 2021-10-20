using System.Collections.Generic;
using System.Windows.Controls;

namespace RegEdit.initializer
{
    public class Initializer
    {
        private static readonly Parameter DefaultParameter = new Parameter("(default)",
            "REG_SZ", "(value not assign)");

        public static RegItem Init()
        {
            RegItem rootFolder = new RegItem("Computer");
            ItemCollection regItems = rootFolder.Items;

            regItems.Add(InitHKey("HKEY_CLASSES_ROOT", "classesRoot_L"));
            regItems.Add(InitHKey("HKEY_CURRENT_USER", "currentUser_L"));
            regItems.Add(InitHKey("HKEY_LOCAL_MACHINE", "localMachine_L"));
            regItems.Add(InitHKey("HKEY_USERS", "users_L"));
            regItems.Add(InitHKey("HKEY_CURRENT_CONFIG", "currentConfig_L"));

            RecursiveInitFolders(regItems, "SubFolder_L", 1);

            return rootFolder;
        }

        private static RegItem InitHKey(string name, string shortName)
        {
            RegItem hKey = new RegItem(name);
            hKey.Parameters.Add(DefaultParameter);

            return hKey;
        }

        private static void RecursiveInitFolders(ItemCollection collection, string folderName, int layer)
        {
            int amount = layer == 1 ? 10 : 5;

            foreach (RegItem regItem in collection)
            {
                regItem.Parameters.Add(DefaultParameter);
                if (layer < 5)
                {
                    List<RegItem> regItems = InitFolders(folderName + layer, amount);
                    regItems.ForEach(item => regItem.Items.Add(item));
                    RecursiveInitFolders(regItem.Items, folderName, layer + 1);
                }
                else if (layer == 5)
                {
                    regItem.Parameters = InitParameters("Parameter", "REG_SZ", "0x0000000", amount);
                }
            }
        }

        private static List<RegItem> InitFolders(string name, int amount)
        {
            List<RegItem> regItems = new List<RegItem>();

            for (int i = 0; i < amount; i++)
            {
                RegItem subItem = new RegItem(name + '_' + i);
                subItem.Parameters.Add(DefaultParameter);
                regItems.Add(subItem);
            }

            return regItems;
        }

        private static List<Parameter> InitParameters(string name, string type, string value, int amount)
        {
            List<Parameter> parameters = new List<Parameter>();

            for (int i = 0; i < amount; i++)
            {
                parameters.Add(new Parameter(name + '_' + i, type, value + i));
            }

            return parameters;
        }
    }
}