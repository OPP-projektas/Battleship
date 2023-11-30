using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFClient.Entities.State
{
    public class LoggedInState : GameState
    {
        public override void HandleGamePageChange(Page page, Board? board)
        {
            Uri preparationPageUri = new Uri("../Pages/PreparationPage.xaml", UriKind.Relative);

            MainWindow parent = Window.GetWindow(page) as MainWindow;

            if (parent != null)
            {
                parent.MainFrame.Navigate(preparationPageUri);
            }
        }
    }
}
