using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFClient.Pages;

namespace WPFClient.Entities.State
{
    public class GameStartedState : GameState
    {
        public override void HandleGamePageChange(Page page, Board? board)
        {
            MainWindow parent = Window.GetWindow(page) as MainWindow;

            if (parent != null)
            {
                GamePage startPage = new GamePage(board);
                parent.MainFrame.Navigate(startPage);
            }
        }
    }
}
