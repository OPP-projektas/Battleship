using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Windows;
using System.Windows.Controls;
using WPFClient.Entities;
using WPFClient.Entities.Prototype;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        Logger logger = Logger.GetInstance();
        public Register()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {

            Player.Username = username.Text;
            SetTextComponent();

            Uri preparationPageUri = new Uri("../Pages/MainMenu.xaml", UriKind.Relative);

            MainWindow parent = Window.GetWindow(this) as MainWindow;

            if (parent != null)
            {
                parent.MainFrame.Navigate(preparationPageUri);
            }
        }
        private void SetTextComponent()
        {
            if (cbBold.IsChecked == true)
            {
                logger.cbBold = true;      
            }
            if (cbItalic.IsChecked == true)
            {
                logger.cbItalic = true;
            }
            if (cbUnderline.IsChecked == true)
            {
                logger.cbUnderline = true;
            }
        }
    }
}
