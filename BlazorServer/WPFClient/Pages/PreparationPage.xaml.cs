using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPFClient.Entities;
using WPFClient.Entities.Facotries;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for PreparationPage.xaml
    /// </summary>
    public partial class PreparationPage : Page
    {
        public PreparationPage()
        {
            InitializeComponent();
            Logger logger = Logger.GetInstance();
            logger.SetMessageListBox(messages);
        }

        private void btnBattleship_Click(object sender, RoutedEventArgs e)
        {
            ShipFactory shipFactory = new BattleshipFactory();
            shipFactory.CreateShip("My username");
        }

        private void btnCarrier_Click(object sender, RoutedEventArgs e)
        {
            ShipFactory shipFactory = new CarrierFactory();
            shipFactory.CreateShip("My username");
        }

        private void btnSubmarine_Click(object sender, RoutedEventArgs e)
        {
            ShipFactory shipFactory = new SubmarineFactory();
            shipFactory.CreateShip("My username");
        }

        private void btnBoat_Click(object sender, RoutedEventArgs e)
        {
            ShipFactory shipFactory = new BoatFactory();
            shipFactory.CreateShip("My username");
        }
    }
}
