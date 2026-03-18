using Library;
using Library.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Project_CNPM.Services;
using System;
using System.Threading.Tasks;

namespace Project_CNPM.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(user.Text) || string.IsNullOrEmpty(pass.Password))
            {
                await ShowMessage("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            var u = AuthService.Login(user.Text, pass.Password);

            if (u == null)
            {
                await ShowMessage("Sai tài khoản hoặc mật khẩu");
                return;
            }

            if (u.Role == "Admin")
                MainWindow.Frame.Navigate(typeof(AdminPage));
            else
                MainWindow.Frame.Navigate(typeof(UserPage), u.Id);
        }

        private void GoRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Navigate(typeof(RegisterPage));
        }

        private async Task ShowMessage(string msg)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Thông báo",
                Content = msg,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}