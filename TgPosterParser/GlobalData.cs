using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TgPosterParser
{
    class GlobalData
    {
        static string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
        static string path = Path.GetDirectoryName(location);

        private static string applicationDataFolder;
        private static string tgAccountsFolder;
        public static string ApplicationDataFolder
        {
            get
            {
                return applicationDataFolder;
            }
            set
            {
                if (!Directory.Exists($@"{path}\{value}"))
                    Directory.CreateDirectory($@"{path}\{value}");

                applicationDataFolder = $@"{path}\{value}";

            }
        }
        public static string TgAccountsFolder
        {
            get
            {               
                return tgAccountsFolder;
            }
            set
            {
                if (!Directory.Exists($@"{ApplicationDataFolder}\{value}"))
                    Directory.CreateDirectory($@"{ApplicationDataFolder}\{value}");

                tgAccountsFolder = $@"{ApplicationDataFolder}\{value}";
            }
            
        }
         public static string ContentFolder
        {
            get
            {               
                return tgAccountsFolder;
            }
            set
            {
                if (!Directory.Exists($@"{ApplicationDataFolder}\{value}"))
                    Directory.CreateDirectory($@"{ApplicationDataFolder}\{value}");

                tgAccountsFolder = $@"{ApplicationDataFolder}\{value}";
            }
            
        }

        static public IProgress<string> Log;
        static public DataGrid TelegramGrid;

        static public void GridRefresh()
        {
            
            TelegramGrid.Items.Refresh();
        }

    }
}
