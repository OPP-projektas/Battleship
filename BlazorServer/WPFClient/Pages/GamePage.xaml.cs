using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPFClient.Entities;
using WPFClient.Entities.Adapter;
using WPFClient.Entities.Bridge;
using WPFClient.Entities.Facade;
using WPFClient.Entities.Facotries;
using WPFClient.Entities.Singelton;
using WPFClient.Entities.Template;

namespace WPFClient.Pages
{
    public partial class GamePage : Page
    {
        HubConnection lobbyConnection = SignalRConnectionManager.GetInstance().LobbyConnection;
        string username = Player.Username;
        public Board AllyBoard;
        public bool myTurn;
        IPlayer mp3Player = new Mp3Player();
        IPlayer wavPlayer = new WavPlayer();
        Entities.Adapter.MediaPlayer mediaAdapter;

        public GamePage(Board board)
        {
            InitializeComponent();
            var result = Task.Run(async () => await OpenPlayerLobbyConnection());
            result.Wait();

            lobbyConnection.On<bool>("DecideTurn", (isYourTurn) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.myTurn = isYourTurn;
                    if (myTurn) { lblTurn.Foreground = Brushes.Green; lblTurn.Content = "Your turn"; }
                    else {lblTurn.Foreground = Brushes.Red; lblTurn.Content = "Enemy turn"; }
                });
            });

            lobbyConnection.On<string>("CheckIfHit", (coords) =>
            {
                this.Dispatcher.Invoke(async () =>
                {
                    await ReportBack(coords);
                });
            });

            lobbyConnection.On<bool, string>("Hit", (hit, coords) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    IColorPicker backGroundColorPicker = new ColorBackground();
                    IColorPicker textColorPicker = new ColorText();
                    Theme buttonTheme;
                    Saver saverTxt = new TxtSaver();
                    Saver saverCsv = new ExcelSaver();
                    if (myTurn)
                    {
                        if (hit)
                        {
                            saverTxt.Save("leaderboard", 1);
                            saverCsv.Save("leaderboard", 1);


                            string name = "Enemy_" + coords;
                            Button previewCell = FindName(name) as Button;
                            if (previewCell != null)
                            {
                                buttonTheme = new LightTheme(backGroundColorPicker);
                                buttonTheme.ColorButton(previewCell);
                                buttonTheme = new WhiteTheme(textColorPicker);
                                buttonTheme.ColorButton(previewCell);
                                
                            }
                        }
                        else
                        {
                            string name = "Enemy_" + coords;
                            Button previewCell = FindName(name) as Button;
                            if (previewCell != null)
                            {
                                buttonTheme = new NeutralTheme(backGroundColorPicker);
                                buttonTheme.ColorButton(previewCell);
                                buttonTheme = new BlackTheme(textColorPicker);
                                buttonTheme.ColorButton(previewCell);
                                mediaAdapter.PlayFullVolume("error.wav");
                            }
                        }
                    }
                    else
                    {
                        if (hit)
                        {
                            string name = "Ally_" + coords;
                            Button previewCell = FindName(name) as Button;
                            if (previewCell != null)
                            {
                                buttonTheme = new DarkTheme(backGroundColorPicker);
                                buttonTheme.ColorButton(previewCell);
                                buttonTheme = new WhiteTheme(textColorPicker);
                                buttonTheme.ColorButton(previewCell);
                            }
                        }
                        else
                        {
                            string name = "Ally_" + coords;
                            Button previewCell = FindName(name) as Button;
                            if (previewCell != null)
                            {
                                buttonTheme = new NeutralTheme(backGroundColorPicker);
                                buttonTheme.ColorButton(previewCell);
                                buttonTheme = new BlackTheme(textColorPicker);
                                buttonTheme.ColorButton(previewCell);
                            }
                        }
                    }
                });
            });

            AllyBoard = board;
            ColorBoard();

            lblhp.Content = AllyBoard.GetLength().ToString();
        }

        private async Task ReportBack(string coords)
        {
            if (CheckIfHit(coords))
            {
                await lobbyConnection.InvokeAsync("ReportBack", true, coords);
            }
            else
            {
                await lobbyConnection.InvokeAsync("ReportBack", false, coords);
            }

        }
        private async Task OpenPlayerLobbyConnection()
        {
            try
            {
                if (lobbyConnection.State != HubConnectionState.Connected)
                    await lobbyConnection.StartAsync();
            }
            catch (Exception ex)
            {
            }
        }
        
        private async void Enemy_Cell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!myTurn)
                    return;

                Button cellButton = (Button)sender;
                string cellName = cellButton.Name;
                cellName = cellName.Substring(cellName.Length-2);
                Shot.CellName = cellName;

                mediaAdapter = new MediaAdapter(mp3Player);
                Facade facade = new Facade(lobbyConnection, mediaAdapter);

                ShootingClient client = new ShootingClient(facade);
                client.Shoot();

                //await lobbyConnection.InvokeAsync("Shoot", Player.Username, cellName);
            }
            catch (Exception ex) 
            {
            }
        }
        public bool CheckIfHit(string coords)
        {
            bool ret = false;
            if (AllyBoard == null)
                return ret;
            int x = ConvertLetterToCoordinate(coords[0]);
            int y = Int32.Parse(coords[1].ToString());
            Position pos = new Position(y-1, x);
            if (AllyBoard.GetCellByPosition(pos).GetType() == new OccupiedCell().GetType())
            {
                ret = true;
            }
            return ret;
        }
        public void ColorBoard()
        {
            if (AllyBoard == null)
                return;
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Position curPosition = new Position(y, x);
                    if (AllyBoard.GetCellByPosition(curPosition).GetType() == new OccupiedCell().GetType())
                    {
                        string name = ConvertCoordinateToLetter(x);
                        if (name == null)
                            return;
                        name = "Ally_" + name + (y+1).ToString();
                        Button previewCell = FindName(name) as Button;
                        if (previewCell != null)
                        {
                            previewCell.Background = Brushes.Pink;
                        }

                    }
                }
            }
        }
        private string ConvertCoordinateToLetter(int x)
        {
            switch (x)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                case 5:
                    return "F";
            }
            return null;
        }
        private int ConvertLetterToCoordinate(char letter)
        {
            switch (letter)
            {
                case 'A':
                    return 0;
                case 'B':
                    return 1;
                case 'C':
                    return 2;
                case 'D':
                    return 3;
                case 'E':
                    return 4;
                case 'F':
                    return 5;
            }
            return 0;
        }

    }
}
