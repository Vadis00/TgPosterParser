using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TgPosterParser.Content;
using TgPosterParser.DB;
using TgPosterParser.Telegram;
using TgPosterParser.Telegram.WTelegramClient;

namespace TgPosterParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WTelegramClient wTelegram;
        bool isSendSMS = false;
        string SMS;
        private static ObservableCollection<TgWorker> Workers;
          
        public MainWindow()
        {
            InitializeComponent();

            Workers = TgWorker.GetTgWorkers(new TelegaPosterContext().Accaunts);

            TelegramTasksGrid.ItemsSource = Workers;
            TgWorker.Notify += GlobalData.GridRefresh;
            GlobalData.TelegramGrid = TelegramTasksGrid;

            GlobalData.Log = new Progress<string>(s => Log.Text += s + Environment.NewLine);

            //   WTelegram.Helpers.Log += (lvl, str) => log.Report(str);

        }

        private void SendSMS_Click(object sender, RoutedEventArgs e)
        {
            TgPosterParser.Telegram.TgWorker accaunt = new(Phone.Text);



            Task worker = Task.Factory.StartNew(() =>
            {
                // wTelegram = new(accaunt);
                //   wTelegram = accaunt.LogIn();

                wTelegram.isAuthorization();

                wTelegram.Notify += CheckSMS;

                wTelegram.ListenUpdate();
            });

        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            SMS = ConfirmationCode.Text;
            isSendSMS = true;
        }
        string CheckSMS(string message)
        {
            for (; ; )
                if (isSendSMS)
                    return SMS;
        }

        private async void ListenUpdates_Click(object sender, RoutedEventArgs e)
        {
            foreach (var Worker in Workers)
            {
                await Worker.LogIn();

                await Worker.Client.GetAllDialogs(); // заменить на accaunt.UpdateChannelsList

                Worker.ShowAllChats();

                await Worker.ListenUpdate();
            }
        }
    }
}
