using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using WPFClient.Entities;
using WPFClient.Entities.AbstractFactory;
using WPFClient.Entities.Facotries;

namespace WPFClient.Pages
{
    public partial class PreparationPage : Page
    {
        IShipFactory shipFactory;
        private Ship selectedShip;
        private List<string> previewShipCells = new List<string>();
        private Board board = new Board();
        public PreparationPage()
        {
            InitializeComponent();
            Logger logger = Logger.GetInstance();
            logger.SetMessageListBox(messages);
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
            Ship ship = this.shipFactory.CreateShip(IShipFactory.ShipType.Battleship, "John");
            selectedShip = ship;
            //ShipFactory shipFactory = new BattleshipFactory();
            //var factoryShip = shipFactory.CreateShip("My username");
        }

        private void btnCarrier_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(IShipFactory.ShipType.Carrier, "John");
            selectedShip = ship;
            //ShipFactory shipFactory = new CarrierFactory();
            //var factoryShip = shipFactory.CreateShip("My username");
        }

        private void btnSubmarine_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(IShipFactory.ShipType.Submarine, "John");
            selectedShip = ship;
            //ShipFactory shipFactory = new SubmarineFactory();
            //var factoryShip = shipFactory.CreateShip("My username");
        }

        private void btnBoat_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateShip(IShipFactory.ShipType.Boat, "John");
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
                        int y = Int32.Parse(cellToMark.Substring(1, 1));
                        int x = Int32.Parse(cellToMark.Substring(2));
                        Button previewCell = FindName(cellToMark) as Button;
                      //  board.boardMatrix[x-1, y-1].isOccupied = true;
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
                int y = Int32.Parse(cellToMark.Substring(1, 1));
                int x = Int32.Parse(cellToMark.Substring(2));

               /* if (board.boardMatrix[x - 1, y - 1].isOccupied)
                {
                    return false;
                }*/
            }
            return true;
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

                foreach (string cellToMark in previewShipCells)
                {
                    Button previewCell = FindName(cellToMark) as Button;
                    if (previewCell != null && previewCell.Background != Brushes.Pink)
                    {
                        previewCell.Background = Brushes.Yellow; // You can change the background color or any other visual indicator
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
            CellFactory concreteCellFactory = new ConcreteCellFactory();

            var concreteCell = concreteCellFactory.GetCell(CellFactory.CellType.Occupied);

            concreteCell.Place();
        }
    }
}
