using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Library
{
    public sealed partial class MainWindow : Window
    {
        public static Frame AppFrame;

        public MainWindow()
        {
            this.InitializeComponent();
            AppFrame = RootFrame;
            AppFrame.Navigate(typeof(Views.LoginPage));
        }
    }
}