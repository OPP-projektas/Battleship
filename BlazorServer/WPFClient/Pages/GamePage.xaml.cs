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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WPFClient.Components;
using WPFClient.Entities;
using WPFClient.Entities.Adapter;
using WPFClient.Entities.Bridge;
using WPFClient.Entities.Facade;
using WPFClient.Entities.Facotries;
using WPFClient.Entities.Flyweight;
using WPFClient.Entities.Prototype;
using WPFClient.Entities.Singelton;
using WPFClient.Entities.State;
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
        private Logger logger;
        FlyweightFactory flyWeightFactory = new FlyweightFactory();
        private int currentLives = 0;

        public GamePage(Board board)
        {
            InitializeComponent();
            var result = Task.Run(async () => await OpenPlayerLobbyConnection());
            result.Wait();
            logger = Logger.GetInstance();

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

                            currentLives--;
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

                    if(currentLives < 1)
                    {
                        GameStateContext gameStateContext = new GameStateContext(new GameLostState());
                        gameStateContext.ChangeGamePageRequest(this, null);
                    }
                });
            });

            AllyBoard = board;
            ColorBoard();

            lblhp.Content = AllyBoard.GetLength().ToString();
            currentLives = AllyBoard.GetLength();
            canvas.Width = this.ActualWidth; 
            canvas.Height = this.ActualHeight;
            for (int i = 0; i < 150; i++) 
            {
                MovingRectangle baseMovingRectangle = new MovingRectangle(Brushes.Blue);
                IFlyweight ImovingRectangle = flyWeightFactory.GetFlyweight(baseMovingRectangle.rectangle);
                if (i % 3 == 0)
                {
                    ImovingRectangle = flyWeightFactory.GetFlyweight(new MovingRectangle(Brushes.LightBlue).rectangle);
                }
                else if (i % 3 == 1)
                {
                    ImovingRectangle = flyWeightFactory.GetFlyweight(new MovingRectangle(Brushes.Black).rectangle);
                }

                var movingRectangle = new MovingRectangle(ImovingRectangle.Operation().Fill);

                canvas.Children.Add(movingRectangle);
                
                Canvas.SetLeft(movingRectangle, (i * 25)-250); 

                StartAnimation(movingRectangle);
                StartAnimationHorizontal(movingRectangle);
            }
            Message message = new Message();
            var flyweights = flyWeightFactory.ListFlyweights();
            foreach(var fw in flyweights)
            {
                message.SetMessage(fw);
                logger.Log(message);
            }
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
        private void StartAnimation(MovingRectangle movingRectangle)
        {
            Random rnd = new Random();
            int min = 20;
            int max = 1250 - 20;
            DoubleAnimation animation = new DoubleAnimation
            {
                From = -250,
                To = min + (rnd.NextDouble() * (max - min)),
                Duration = new Duration(TimeSpan.FromSeconds(10)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            movingRectangle.BeginAnimation(Canvas.TopProperty, animation);
        }
        private void StartAnimationHorizontal(MovingRectangle movingRectangle)
        {
            Random rnd = new Random();
            int min = 1000;
            int max = 1500;
            double from = min + (rnd.NextDouble() * (max - min));
            int i = rnd.Next(0, 5);
            {
                if (i < 0.5)
                {
                    DoubleAnimation animation = new DoubleAnimation
                    {
                        From = from,
                        To = min-max,
                        Duration = new Duration(TimeSpan.FromSeconds(10)),
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    movingRectangle.BeginAnimation(Canvas.LeftProperty, animation);
                }
                else if(i < 1.5)
                {
                    DoubleAnimation animation = new DoubleAnimation
                    {
                        From = from-(1.25*max),
                        To = max,
                        Duration = new Duration(TimeSpan.FromSeconds(10)),
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    movingRectangle.BeginAnimation(Canvas.LeftProperty, animation);
                }
                else if (i < 2.5)
                {
                    DoubleAnimation animation = new DoubleAnimation
                    {
                        From = 500,
                        To = from,
                        Duration = new Duration(TimeSpan.FromSeconds(10)),
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    movingRectangle.BeginAnimation(Canvas.LeftProperty, animation);
                }
                else
                {
                    DoubleAnimation animation = new DoubleAnimation
                    {
                        From = 500,
                        To = min + (rnd.NextDouble() * (max - min))-max,
                        Duration = new Duration(TimeSpan.FromSeconds(10)),
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    movingRectangle.BeginAnimation(Canvas.LeftProperty, animation);
                }
            }

        }
    }
}
