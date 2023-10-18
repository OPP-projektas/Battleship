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

namespace WPFClient.Pages
{
    public partial class PreparationPage : Page
    {
        Subject subject = new Subject();
        Context context = new Context();
        ConcreteObserver observer = new ConcreteObserver();
        HubConnection lobbyConnection = SignalRConnectionManager.GetInstance().LobbyConnection;
        string username = "Jonas";
        IShipFactory shipFactory;
        private Ship selectedShip;
        private List<string> previewShipCells = new List<string>();
        private Board board = new Board();
        private Logger logger;
        private ConcreteBuilder concreteBuilder = new ConcreteBuilder();
        public PreparationPage()
        {
            InitializeComponent();
            logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);

            var result = Task.Run(async() => await OpenPlayerLobbyConnection());
            result.Wait();

            subject.Attach(observer);
        }
        private async Task OpenPlayerLobbyConnection()
        {

            try
            {
                if (lobbyConnection.State != HubConnectionState.Connected)
                    await lobbyConnection.StartAsync();              
                await lobbyConnection.InvokeAsync("AdmitPlayer", username);
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
            //ShipFactory shipFactory = new BattleshipFactory();
            //var factoryShip = shipFactory.CreateShip("My username");
        }

        private void btnCarrier_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Carrier, username);
            selectedShip = ship;
            //ShipFactory shipFactory = new CarrierFactory();
            //var factoryShip = shipFactory.CreateShip("My username");
        }

        private void btnSubmarine_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Submarine, username);
            selectedShip = ship;
            //ShipFactory shipFactory = new SubmarineFactory();
            //var factoryShip = shipFactory.CreateShip("My username");
        }

        private void btnBoat_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(TypesOfShips.Boat, username);
            selectedShip = ship;
            //ShipFactory shipFactory = new BoatFactory();
            //var factoryShip = shipFactory.CreateShip("My username");

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
                    foreach (string cellToMark in previewShipCells)
                    {
                        int y = Int32.Parse(cellToMark.Substring(1, 1))-1;
                        int x = Int32.Parse(cellToMark.Substring(2))-1;

                        Button previewCell = FindName(cellToMark) as Button;
                        Position curPosition = new Position(x, y);
                        board.ReplaceCell(curPosition);
                        if (previewCell != null)
                        {
                            previewCell.Background = Brushes.Pink;
                        }
                    }
                    previewShipCells = new List<string>();
                }

            }
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
                    concreteBuilder.BuildBoat();
                    break;
                case 2:
                    concreteBuilder.BuildBattleship();
                    break;
                case 3:
                    concreteBuilder.BuildSubmarine();
                    break;
                case 4:
                    concreteBuilder.BuildCarrier();
                    break;
                default:
                    concreteBuilder.Reset();
                    break;
            }              
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
            var parts = concreteBuilder.GetProduct().ListParts();

            context.SetStrategy(new AscendingLength());
            context.DoStrategicSort(parts);

            context.SetStrategy(new DescedingLength());
            context.DoStrategicSort(parts);

            context.SetStrategy(new AscendingAmont());
            context.DoStrategicSort(parts);

            context.SetStrategy(new DescendingAmount());
            context.DoStrategicSort(parts);

            subject.Ready();
        }
    }
}
