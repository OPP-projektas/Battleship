using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClient.Entities.Proxy;

namespace WPFClient.Entities.State
{
    public class GameWonState : GameState, IProxyInterface
    {
        public override void HandleGamePageChange(Page page, Board? board)
        {
            Uri preparationPageUri = new Uri("../Pages/GameWon.xaml", UriKind.Relative);

            MainWindow parent = Window.GetWindow(page) as MainWindow;

            if (parent != null)
            {
                parent.MainFrame.Navigate(preparationPageUri);
            }
        }

        public void Request(Page page, Board? board)
        {
            HandleGamePageChange(page, board);
        }
    }
}
