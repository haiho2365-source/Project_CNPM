using Library;
using Library.Models;
using Library.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Project_CNPM.Services;

namespace Project_CNPM.Views
{
    public sealed partial class UserPage : Page
    {
        int selectedBookId = -1;
        int currentUserId = 2; // demo (sau nâng cấp login)

        public UserPage()
        {
            this.InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            list.ItemsSource = BookService.GetAll();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var b = list.SelectedItem as Book;
            if (b != null)
                selectedBookId = b.Id;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = BookService.Search(txtSearch.Text);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var item = cbCategory.SelectedItem as ComboBoxItem;
            if (item != null && item.Content.ToString() != "All")
                list.ItemsSource = BookService.Filter(item.Content.ToString());
            else
                LoadData();
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            if (selectedBookId == -1) return;

            BorrowService.BorrowBook(currentUserId, selectedBookId);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            var borrows = BorrowService.GetByUser(currentUserId);
            if (borrows.Count > 0)
                BorrowService.ReturnBook(borrows[0].Id, borrows[0].BookId);
        }

        private void MyBooks_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = BorrowService.GetByUser(currentUserId);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppFrame.Navigate(typeof(LoginPage));
        }
    }
}