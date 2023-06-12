using System;

namespace DOSE.BASE.Platform.Common
{
    public class ConfigTableAttribute: Attribute
    {
        public ConfigTableAttribute(string configTable)
        {
            _configTable = configTable;
        }

        // Keep a variable internally ...
        protected string _configTable;

        // .. and show a copy to the outside world.
        public string ConfigTable
        {
            get { return _configTable; }
            set { _configTable = value; }
        }
    }
}
