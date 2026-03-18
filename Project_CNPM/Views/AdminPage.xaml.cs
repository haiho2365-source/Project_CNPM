using Library;
using Library.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Project_CNPM.Services;

namespace Project_CNPM.Views
{
    public sealed partial class AdminPage : Page
    {
        int selectedId = -1;

        public AdminPage()
        {
            this.InitializeComponent();
            LoadBooks();
        }

        void LoadBooks()
        {
            list.ItemsSource = BookService.GetAll();
        }

        private void BorrowList_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = BorrowService.GetAllBorrow();
        }
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var b = list.SelectedItem as Book;
            if (b != null)
            {
                selectedId = b.Id;
                title.Text = b.Title;
                author.Text = b.Author;
                category.Text = b.Category;
                quantity.Text = b.Quantity.ToString();
            }
        }

        private void LoadBooks_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = BookService.GetAll();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            BookService.Add(new Book
            {
                Title = title.Text,
                Author = author.Text,
                Category = category.Text,
                Quantity = int.Parse(quantity.Text)
            });

            LoadBooks();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            BookService.Update(new Book
            {
                Id = selectedId,
                Title = title.Text,
                Author = author.Text,
                Category = category.Text,
                Quantity = int.Parse(quantity.Text)
            });

            LoadBooks();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            BookService.Delete(selectedId);
            LoadBooks();
        }

        private void LoadUser_Click(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = UserService.GetAll();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Navigate(typeof(LoginPage));
        }
    }
}