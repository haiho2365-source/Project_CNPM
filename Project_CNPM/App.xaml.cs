using Library;
using Library.Services;
using Microsoft.UI.Xaml;
using Project_CNPM; // sửa đúng namespace của bạn

using Project_CNPM.Services;

namespace Project_CNPM
{
    public partial class App : Application
    {
        private Window m_window;

        public App()
        {
            this.InitializeComponent(); // dòng này sẽ hết lỗi nếu fix đúng
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Database.Initialize();

            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}