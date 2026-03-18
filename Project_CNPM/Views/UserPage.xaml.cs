using Library.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Project_CNPM.Services;
using System;
using System.Collections.Generic;

namespace Project_CNPM.Views
{
    public sealed partial class UserPage : Page
    {
        private int userId;
        private Book selectedBook;
        private Borrow selectedBorrow;

        public UserPage()
        {
            this.InitializeComponent();
        }

        // NHẬN USER ID TỪ LOGIN
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                userId = (int)e.Parameter;
        }

        // LOAD DATA
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                bookList.ItemsSource = BookService.GetAll();

                if (userId > 0)
                    borrowList.ItemsSource = BorrowService.GetByUser(userId);
            }
            catch (Exception ex)
            {
                await ShowMessage("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // CHỌN SÁCH
        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBook = (Book)bookList.SelectedItem;
        }

        // CHỌN SÁCH ĐÃ MƯỢN
        private void BorrowList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBorrow = (Borrow)borrowList.SelectedItem;
        }

        // MƯỢN SÁCH
        private async void Borrow_Click(object sender, RoutedEventArgs e)
        {
            selectedBook = (Book)bookList.SelectedItem;

            if (selectedBook == null)
            {
                await ShowMessage("Vui lòng chọn sách");
                return;
            }

            if (selectedBook.Quantity <= 0)
            {
                await ShowMessage("Sách đã hết");
                return;
            }

            BorrowService.BorrowBook(userId, selectedBook.Id);

            await ShowMessage("Mượn sách thành công");

            LoadData();
        }

        // TRẢ SÁCH
        private async void Return_Click(object sender, RoutedEventArgs e)
        {
            selectedBorrow = (Borrow)borrowList.SelectedItem;

            if (selectedBorrow == null)
            {
                await ShowMessage("Vui lòng chọn sách để trả");
                return;
            }

            BorrowService.ReturnBook(selectedBorrow.Id, selectedBorrow.BookId);

            await ShowMessage("Trả sách thành công");

            LoadData();
        }

        // DIALOG
        private async System.Threading.Tasks.Task ShowMessage(string message)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Thông báo",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}