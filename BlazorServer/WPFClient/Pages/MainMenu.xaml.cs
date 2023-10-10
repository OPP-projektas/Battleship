using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WPFClient.Entities;
using WPFClient.Components;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        HubConnection connection;
        HubConnection counterConnection;

        public MainMenu()
        {
            InitializeComponent();

            Logger logger = Logger.GetInstance();
            logger.Log("Main menu started");



            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7263/chathub")
                .WithAutomaticReconnect()
                .Build();

            counterConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7263/counterhub")
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += (sender) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = "Attempting to reconnect...";
                    messages.Items.Add(newMessage);
                });

                return Task.CompletedTask;
            };

            connection.Reconnected += (sender) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = "Reconnected to the server";
                    messages.Items.Clear();
                    messages.Items.Add(newMessage);
                });

                return Task.CompletedTask;
            };

            connection.Closed += (sender) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = "Connection closed";
                    messages.Items.Add(newMessage);
                    openConnection.IsEnabled = true;
                    sendMessage.IsEnabled = false;
                });

                return Task.CompletedTask;
            };
        }

        private async void openConnection_Click(object sender, RoutedEventArgs e)
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messages.Items.Add(newMessage);
                });
            });
            try
            {
                await connection.StartAsync();
                messages.Items.Add("Connection started");
                openConnection.IsEnabled = false;
                sendMessage.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }

        }

        private async void sendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", "WPF Client", messageInput.Text);

            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        private async void openCounter_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                await counterConnection.StartAsync();
                openCounter.IsEnabled = false;
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        private async void incrementCounter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await counterConnection.InvokeAsync("AddToTotal", "WPF Client", 1);

            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            Uri preparationPageUri = new Uri("../Pages/PreparationPage.xaml", UriKind.Relative);

            MainWindow parent = Window.GetWindow(this) as MainWindow;

            if(parent != null)
            {
                parent.MainFrame.Navigate(preparationPageUri);
            }
        }
    }
}
