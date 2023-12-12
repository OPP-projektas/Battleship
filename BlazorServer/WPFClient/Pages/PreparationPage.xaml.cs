using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFClient.Entities;
using WPFClient.Entities.AbstractFactory;
using WPFClient.Entities.Facotries;
using WPFClient.Entities.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using WPFClient.Entities.Observer;
using WPFClient.Entities.Strategy;
using System.Reflection;
using WPFClient.Entities.Prototype;
using WPFClient.Entities.Singelton;
using WPFClient.Entities.Command;
using ICommand = WPFClient.Entities.Command.ICommand;
using MediaPlayer = WPFClient.Entities.Adapter.MediaPlayer;
using WPFClient.Entities.Adapter;
using WPFClient.Entities.Facade;
using WPFClient.Entities.Bridge;
using System.Linq;
using WPFClient.Entities.State;
using WPFClient.Entities.Interpreter;
using WPFClient.Entities.Mediator;

namespace WPFClient.Pages
{
    public partial class PreparationPage : Page
    {
        Subject subject = new Subject();
        Context context = new Context();
        ConcreteObserver observer = new ConcreteObserver();
        HubConnection lobbyConnection = SignalRConnectionManager.GetInstance().LobbyConnection;
        string username = Player.Username;
        IShipFactory shipFactory;
        private Ship selectedShip;
        private List<string> previewShipCells = new List<string>();
        private Board board = new Board();
        private Logger logger;
        public List<TypesOfShips> shipList = new List<TypesOfShips>();
        public BuilderAbstract boatBuilder = new BoatBuilder();
        public BuilderAbstract battleshipBuilder = new BattleshipBuilder();
        public BuilderAbstract submarineBuilder = new SubmarineBuilder();
        public BuilderAbstract carrierBuilder = new CarrierBuilder();
        public Director shipDirector = new Director();
        ICommand placeShipCommand;
        ICommand readyCommand;
        Invoker commandInvoker = new Invoker();
        MediaPlayer mediaPlayer;
        IPlayer mp3Player = new Mp3Player();
        GameStateContext gameStateContext = new GameStateContext();
        private ReadyButtonComponent component1;
        private UndoButtonComponent component2;
        private ElementsHolder elementsHolder;

        public PreparationPage()
        {
            InitializeComponent();
            logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            component1 = new ReadyButtonComponent(btnCell);
            component2 = new UndoButtonComponent(btnUnplace, false);
            elementsHolder = new ElementsHolder();
            new ButtonPressMediator(component1, component2, elementsHolder);
            elementsHolder.Add(btnBoat);
            elementsHolder.Add(btnBattleship);
            elementsHolder.Add(btnCarrier);
            elementsHolder.Add(btnSubmarine);
            elementsHolder.Add(btnCell);
            elementsHolder.Add(cbLoggingOrder);
            elementsHolder.Add(horizontalCheckBox);
            cbLoggingOrder.SelectedIndex = 0;
            var result = Task.Run(async() => await OpenPlayerLobbyConnection());
            result.Wait();
            readyCommand = new ReadyCommand(subject, observer);
        }
        private async Task OpenPlayerLobbyConnection()
        {

            try
            {
                if (lobbyConnection.State != HubConnectionState.Connected)
                    await lobbyConnection.StartAsync();              
                await lobbyConnection.InvokeAsync("AdmitPlayer", username);
                lobbyConnection.On<string>("StartGame", (user) =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        gameStateContext.TransitionTo(new GameStartedState());
                        gameStateContext.ChangeGamePageRequest(this, board);
                    });
                });
            }
            catch (Exception ex)
            {
            }
        }
        private void UpdateShipFactory()
        {
            if (horizontalCheckBox.IsChecked == true)
                this.shipFactory = new HorizontalShipFactory();
            else
                this.shipFactory = new VerticalShipFactory();
        }



        private void btnBattleship_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Battleship, username);
            selectedShip = ship;
        }

        private void btnCarrier_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Carrier, username);
            selectedShip = ship;
        }

        private void btnSubmarine_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Submarine, username);
            selectedShip = ship;
        }

        private void btnBoat_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Boat, username);
            selectedShip = ship;
        }
        private void GameCell_Click(object sender, RoutedEventArgs e)
        {
            if (selectedShip != null)
            {
                Button cellButton = (Button)sender;
                string cellName = cellButton.Name;
                PlaceShip(selectedShip, cellName);
                BuildShip();              
            }
            else
            {
                MessageBox.Show("Please select a ship first.");
            }
        }


        private void PlaceShip(Ship selectedShip, string cellName)
        {
            if (selectedShip != null)
            {
                previewShipCells = CalculateCellsToMark(selectedShip, cellName);

                bool canPlace = CanPlaceShip(previewShipCells);

                if (canPlace)
                {
                    List<Position> positions = new List<Position>();
                    foreach (string cellToMark in previewShipCells)
                    {
                        int y = Int32.Parse(cellToMark.Substring(1, 1))-1;
                        int x = Int32.Parse(cellToMark.Substring(2))-1;
                        Position curPosition = new Position(x, y);
                        positions.Add(curPosition);
                    }
                    if(positions.Count > 0)
                    {
                        board.Add(selectedShip);
                        placeShipCommand = new PlaceShipCommand(positions, board);
                        commandInvoker.SetCommand(placeShipCommand);

                        //commandInvoker.DoCommand();
                        //IPlayer mp3Player = new Mp3Player();
                        //mediaPlayer = new MediaAdapter(mp3Player);
                        //mediaPlayer.PlayFullVolume("yeah_boy.mp3");

                        mediaPlayer = new MediaAdapter(mp3Player);
                        Facade facade = new Facade(commandInvoker, mediaPlayer);
                        ShipPlacingClient client = new ShipPlacingClient(facade);
                        client.PlaceShip();

                        UpdateBoardUI();
                    }
                }

            }
        }
        public void UpdateBoardUI()
        {
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Position curPosition = new Position(y, x);
                    string name = ConvertCoordinateToLetter(x);
                    if (name == null)
                        return;
                    name = name + (x+1).ToString() + (y + 1).ToString();
                    Button previewCell = FindName(name) as Button;
                    if (board.GetCellByPosition(curPosition).GetType() == new OccupiedCell().GetType())
                    {
                        if (previewCell != null)
                        {
                            previewCell.Background = Brushes.Pink;
                        }
                    }
                    else
                    {
                        if (previewCell != null)
                        {
                            Color defaultButtonColor = Color.FromRgb(221, 221, 221);
                            SolidColorBrush defaultBrush = new SolidColorBrush(defaultButtonColor);
                            previewCell.Background = defaultBrush;
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
        private bool CanPlaceShip(List<string> previewShipCells)
        {
            foreach (string cellToMark in previewShipCells)
            {
                int y = Int32.Parse(cellToMark.Substring(1, 1))-1;
                int x = Int32.Parse(cellToMark.Substring(2))-1;

                Position curPosition = new Position(x, y);
                if (board.GetCellByPosition(curPosition).GetType() == new OccupiedCell().GetType())
                {
                    return false;
                }
            }
            return true;
        }

        private void BuildShip()
        {
            switch (selectedShip.Length)
            {
                case 1:
                    shipDirector.AbstractBuilder = boatBuilder;
                    shipList.Add(TypesOfShips.Boat);
                    break;
                case 2:
                    shipDirector.AbstractBuilder = battleshipBuilder;
                    shipList.Add(TypesOfShips.Battleship);
                    break;
                case 3:
                    shipDirector.AbstractBuilder = submarineBuilder;
                    shipList.Add(TypesOfShips.Submarine);
                    break;
                case 4:
                    shipDirector.AbstractBuilder = carrierBuilder;
                    shipList.Add(TypesOfShips.Carrier);
                    break;
            }
            shipDirector.BuildShipWithMessage();
        }
        private string AddLetterToCoordinates(string x)
        {
            switch (x)
            {
                case "1":
                    return "A" + x;
                case "2":
                    return "B" + x;
                case "3":
                    return "C" + x;
                case "4":
                    return "D" + x;
                case "5":
                    return "E" + x;
                case "6":
                    return "F" + x;
            }
            return null;
        }

        private List<string> CalculateCellsToMark(Ship selectedShip, string cellName)
        {
            List<string> cells = new List<string>();
            int y = Int32.Parse(cellName.Substring(1,1));
            int x = Int32.Parse(cellName.Substring(2));


            if (horizontalCheckBox.IsChecked != true)
            {
                if (y - selectedShip.Length >= 0)
                {
                    for (int i = y - selectedShip.Length + 1; i <= y; i++)
                    {
                        string cords = AddLetterToCoordinates(i.ToString());
                        string cell = cords + x.ToString();

                        cells.Add(cell);
                    }
                }
            }
            else
            {
                if (x + selectedShip.Length-1 <= 6)
                {
                    for (int i = x; i <= x + selectedShip.Length-1; i++)
                    {
                        string cords = AddLetterToCoordinates(y.ToString());
                        string cell = cords + i.ToString();

                        cells.Add(cell);
                    }
                }
            }

            return cells;
        }

        private void GameCell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (selectedShip != null)
            {
                Button cellButton = (Button)sender;
                string cellName = cellButton.Name;

                previewShipCells = CalculateCellsToMark(selectedShip, cellName);
                bool paint = true;
                foreach (string cellToMark in previewShipCells)
                {
                    Button previewCell = FindName(cellToMark) as Button;
                    if (previewCell == null || previewCell.Background == Brushes.Pink)
                    {
                        paint = false;
                        break;
                    }
                }
                if (paint)
                {
                    foreach (string cellToMark in previewShipCells)
                    {
                        Button previewCell = FindName(cellToMark) as Button;
                        if (previewCell != null && previewCell.Background != Brushes.Pink)
                        {
                            previewCell.Background = Brushes.Yellow;
                        }
                    }
                }
            }
        }
        private void GameCell_MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (var cell in previewShipCells)
            {
                Button previewCell = FindName(cell) as Button;
                if (previewCell != null && previewCell.Background != Brushes.Pink)
                {
                    Color defaultButtonColor = Color.FromRgb(221, 221, 221);
                    SolidColorBrush defaultBrush = new SolidColorBrush(defaultButtonColor);
                    previewCell.Background = defaultBrush;
                }
            }
            previewShipCells.Clear();
        }

        private void horizontalCheckBox_Click(object sender, RoutedEventArgs e)
        {
            selectedShip = null;
        }

        private void btnCell_Click(object sender, RoutedEventArgs e)
        {
            var temp = Helper.ConcatDictionairies(boatBuilder.GetProduct().ListParts(), battleshipBuilder.GetProduct().ListParts());
            var temp2 = Helper.ConcatDictionairies(submarineBuilder.GetProduct().ListParts(), carrierBuilder.GetProduct().ListParts());
            var parts = Helper.ConcatDictionairies(temp, temp2);
            switch (cbLoggingOrder.SelectedValue.ToString()) 
            {
                case "Ascending Amount":
                    context.SetStrategy(new AscendingAmount());
                    context.DoStrategicSort(parts);
                    break;
                case "Ascending Length":
                    context.SetStrategy(new AscendingLength());
                    context.DoStrategicSort(parts);
                    break;
                case "Descending Amount":
                    context.SetStrategy(new DescendingAmount());
                    context.DoStrategicSort(parts);
                    break;
                case "Descending Length":
                    context.SetStrategy(new DescedingLength());
                    context.DoStrategicSort(parts);
                    break;
            }

            commandInvoker.SetCommand(readyCommand);
            commandInvoker.DoCommand();


            component1.Operation();

            lblStatus.Content = "Status: READY!";
            lblStatus.Foreground = Brushes.Green;
            //btnBattleship.IsEnabled = false;
            //btnBoat.IsEnabled = false;
            //btnCarrier.IsEnabled = false;
            //btnSubmarine.IsEnabled = false;
            cbLoggingOrder.IsEnabled = false;
            horizontalCheckBox.IsEnabled = false;
            //btnCell.IsEnabled = false;

            

        }

        private void btnUnplace_Click(object sender, RoutedEventArgs e)
        {
            IPlayer wavPlayer = new WavPlayer();
            mediaPlayer = new MediaAdapter(wavPlayer);
            mediaPlayer.PlayFullVolume("error.wav");
            var lastCommand = commandInvoker.UndoCommand();
            if(lastCommand != null  && lastCommand.GetType().Name != "ReadyCommand")
                Helper.RemoveLastShip(shipList, boatBuilder, battleshipBuilder, submarineBuilder, carrierBuilder);
            UpdateBoardUI();
            lblStatus.Content = "Status: NOT READY!";
            lblStatus.Foreground = Brushes.Red;

            component2.Operation();

            //btnBattleship.IsEnabled = true;
            //btnBoat.IsEnabled = true;
            //btnCarrier.IsEnabled = true;
            //btnSubmarine.IsEnabled = true;
            cbLoggingOrder.IsEnabled = true;
            horizontalCheckBox.IsEnabled = true;
            //btnCell.IsEnabled = true;
        }

        private void inputButton_Click(object sender, RoutedEventArgs e)
        {
            PanelContext context = new PanelContext(input.Text);
            ExpressionHandler handler = new ExpressionHandler();
            ClearConsoleExpression clearConsoleExpression = new ClearConsoleExpression();
            PlayerNameExpression playerNameExpression = new PlayerNameExpression();

            handler.Expression1 = clearConsoleExpression;
            handler.Expression2 = playerNameExpression;

            handler.Interpret(context);
        }

    }
}
