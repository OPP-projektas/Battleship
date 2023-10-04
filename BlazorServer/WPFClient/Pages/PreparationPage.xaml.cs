using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFClient.Entities;
using WPFClient.Entities.AbstractFactory;
using WPFClient.Entities.Facotries;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for PreparationPage.xaml
    /// </summary>
    public partial class PreparationPage : Page
    {
        IShipFactory shipFactory;
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
            Ship ship = this.shipFactory.CreateBattleship("John");

            ShipFactory shipFactory = new BattleshipFactory();
            shipFactory.CreateShip("My username");
        }

        private void btnCarrier_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateCarrier("John");



            ShipFactory shipFactory = new CarrierFactory();
            shipFactory.CreateShip("My username");
        }

        private void btnSubmarine_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateSubmarine("John");

            ShipFactory shipFactory = new SubmarineFactory();
            shipFactory.CreateShip("My username");
        }

        private void btnBoat_Click(object sender, RoutedEventArgs e)
        {
            UpdateShipFactory();
            Ship ship = this.shipFactory.CreateBoat("John");

            ShipFactory shipFactory = new BoatFactory();
            shipFactory.CreateShip("My username");
        }
    }
}
