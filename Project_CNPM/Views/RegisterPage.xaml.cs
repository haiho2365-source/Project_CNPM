using Library;
using Library.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Project_CNPM.Services;
using System;
using System.Threading.Tasks;

namespace Project_CNPM.Views
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(user.Text) ||
                string.IsNullOrEmpty(pass.Password) ||
                string.IsNullOrEmpty(confirm.Password))
            {
                await ShowMessage("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            if (pass.Password != confirm.Password)
            {
                await ShowMessage("Mật khẩu không khớp");
                return;
            }

            bool ok = AuthService.Register(user.Text, pass.Password);

            if (ok)
            {
                await ShowMessage("Đăng ký thành công");
                MainWindow.Frame.Navigate(typeof(LoginPage));
            }
            else
            {
                await ShowMessage("Username đã tồn tại");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Navigate(typeof(LoginPage));
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