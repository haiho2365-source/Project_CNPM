using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Project_CNPM.Views;

namespace Project_CNPM
{
    public sealed partial class MainWindow : Window
    {
        public static Frame Frame;

        public MainWindow()
        {
            this.InitializeComponent();

            Frame = RootFrame;

            // 🔥 Quan trọng: phải navigate sau khi InitializeComponent
            Frame.Navigate(typeof(LoginPage));
        }
    }
}