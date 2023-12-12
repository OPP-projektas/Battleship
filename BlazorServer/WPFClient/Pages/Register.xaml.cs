using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WPFClient.Entities;
using WPFClient.Entities.Prototype;
using WPFClient.Entities.State;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        Logger logger = Logger.GetInstance();
        GameStateContext gameStateContext = new GameStateContext();
        public Register()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            Player.Username = username.Text;
            SetTextComponent();

            gameStateContext.TransitionTo(new LoggedInState());
            gameStateContext.ChangeGamePageRequest(this, null);
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
