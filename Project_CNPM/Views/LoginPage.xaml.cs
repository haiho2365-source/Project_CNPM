using Library.Models;
using Library.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Project_CNPM.Views;

namespace Library.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var userData = AuthService.Login(user.Text, pass.Password);

            if (userData != null)
            {
                if (userData.Role == "Admin")
                    MainWindow.AppFrame.Navigate(typeof(AdminPage));
                else
                    MainWindow.AppFrame.Navigate(typeof(UserPage));
            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Sai tài khoản hoặc mật khẩu",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                dialog.ShowAsync();
            }
        }

        

        private async void Forgot_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Reset Password",
                Content = new StackPanel
                {
                    Children =
            {
                new TextBox { PlaceholderText = "Username", Name="u"},
                new TextBox { PlaceholderText = "New Password", Name="p"}
            }
                },
                PrimaryButtonText = "Reset",
                XamlRoot = this.XamlRoot
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                var panel = dialog.Content as StackPanel;
                var username = (panel.Children[0] as TextBox).Text;
                var pass = (panel.Children[1] as TextBox).Text;

                AuthService.ResetPassword(username, pass);
            }
        }
        private async void ShowMessage(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Thông báo",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot   // QUAN TRỌNG (WinUI 3 bắt buộc)
            };

            await dialog.ShowAsync();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool ok = AuthService.Register(user.Text, pass.Password);

            if (ok)
                ShowMessage("Đăng ký thành công");
            else
                ShowMessage("Username đã tồn tại");
        }
    }
}