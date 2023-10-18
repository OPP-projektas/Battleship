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

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {

            UserInfo.Username = username.Text;

            Uri preparationPageUri = new Uri("../Pages/MainMenu.xaml", UriKind.Relative);

            MainWindow parent = Window.GetWindow(this) as MainWindow;

            if (parent != null)
            {
                parent.MainFrame.Navigate(preparationPageUri);
            }
        }
    }
}
