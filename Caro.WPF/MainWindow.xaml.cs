using Caro.WPF.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Caro.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Cell> Maps { get; set; } = new ObservableCollection<Cell>();
        private Status _currentStatus { get; set; }
        public Status CurrentStatus { get => _currentStatus; set { _currentStatus = value; OnPropertyChanged(); } }
        public string TurnStatus { get => _status; set { _status = value; OnPropertyChanged(); } }
        private string _status { get; set; }
        public string _notify { get; set; }
        public string notify { get => _notify; set { _notify = value; OnPropertyChanged(); } }
        HubConnection connect;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
            ConnectToSignalR();
            CurrentStatus = Status.X;
            BindingOperations.EnableCollectionSynchronization(Maps, new object());
        }
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await FirstLoad();
        }
        async Task FirstLoad()
        {
            await ReloadMap();
        }
        async Task ReloadMap()
        {
            Maps.Clear();
            HttpClient httpclient = new HttpClient { BaseAddress = new Uri("http://localhost:5197") };
            HttpResponseMessage response = await httpclient.GetAsync("api/Map");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<ObservableCollection<Cell>>(json);
            if (list != null)
            {
                foreach (var cell in list)
                {
                    Maps.Add(cell);
                }
            }
            CurrentStatus = Status.X;
        }

        private async void ConnectToSignalR()
        {
            connect = new HubConnectionBuilder().WithUrl("http://localhost:5197/chathub").Build();
            // hàm nhận thông tin từ server 
            connect.On<int, Status>("Click", async (index, status) =>
            {
                CurrentStatus = status;
                if (Maps[index].Value == Status.None)
                {
                    Maps[index].Value = CurrentStatus;
                    await connect.SendAsync("CheckWin", Maps, index);
                }
            });
            connect.On<Status>("CheckWin", async message =>
            {
                TurnStatus = message.ToString() + " Win";
                MessageBox.Show(TurnStatus);
                await ReloadMap();
            });
            connect.On<string>("UserJoined", x =>
            {
                notify = "Phòng : " + x;
            });
            connect.On<string>("RoomFull", x =>
            {
                notify = x;
                MessageBox.Show(x);
            });
            connect.On<Status>("ChangeTurn", x =>
            {
                CurrentStatus = x;
            });
            connect.On<string>("NotifyStatus", x =>
            {
                TurnStatus = "Lượt của bạn là :" + x;
            });
            await connect.StartAsync();
            await connect.SendAsync("JoinChat");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = (sender as Button).DataContext as Cell;
            var index = Maps.IndexOf(data);
            Task.Run(async () =>
            {
                await connect.SendAsync("Click", index, CurrentStatus);

            });
        }
    }
}
