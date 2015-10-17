using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Common
{
    public static class PluginRegister
    {
        public static List<IPlugin> Plugins { get; private set; } = new List<IPlugin>();
    }
}
