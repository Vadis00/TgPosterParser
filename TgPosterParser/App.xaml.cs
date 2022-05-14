using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TgPosterParser.Telegram;

namespace TgPosterParser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ClientApplication.ClientId = "1330678";
            ClientApplication.ClientHash = "a13f6deffbcb38283cb5da8a5d04d0b3";

            GlobalData.ApplicationDataFolder = "ApplicationData";
            GlobalData.TgAccountsFolder = "AccountsTelegram";
            GlobalData.ContentFolder = "Content";
        }
    }
}
