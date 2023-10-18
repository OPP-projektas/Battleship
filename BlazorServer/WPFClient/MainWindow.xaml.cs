using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using System;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WPFClient.Pages;

namespace WPFClient;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        Uri defaultPageUri = new Uri("./Pages/Register.xaml", UriKind.Relative);
        MainFrame.Source = defaultPageUri;
        MainFrame.Visibility = Visibility.Visible;
    }
}
