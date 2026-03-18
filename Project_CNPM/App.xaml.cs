using Microsoft.UI.Xaml;

namespace Project_CNPM
{
    public partial class App : Application
    {
        private Window m_window;

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate(); // 🔥 nếu thiếu dòng này = KHÔNG HIỆN APP
        }
    }
}