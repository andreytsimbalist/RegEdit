using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace RegEdit
{
    [Serializable]
    public class Data
    {
        public string Header { get; set; }
        public List<Data> Children { get; set; }
        public List<Parameter> Parameters { get; set; }

        public Data()
        {
            
        }
    }
}