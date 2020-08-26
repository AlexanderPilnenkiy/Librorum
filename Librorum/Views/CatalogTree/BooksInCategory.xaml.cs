using Librorum.Source.Model;
using Librorum.Source.Parser;
using Librorum.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Librorum.Views.CatalogTree
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksInCategory : ContentPage
    {
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public string Pages { get; set; }
        public string Description { get; set; }

        public BooksInCategory(string Name, string Path)
        {
            Title = "Книги в жанре " + Name;
            BooksInJenre booksInJenre = new BooksInJenre();
            booksInJenre.GetBooksList(this, Path);
        }

        public async void SelectBook(object sender, SelectedItemChangedEventArgs e)
        {
            BookPreview selectedBook = e.SelectedItem as BookPreview;
            BookTitle = selectedBook.Title;
            Author = selectedBook.Author;
            ImagePath = selectedBook.ImagePath;
            Pages = selectedBook.Pages;
            Description = selectedBook.Description;
            if (selectedBook != null)
            {
                await Navigation.PushAsync(new BookInfo(BookTitle, Author, ImagePath, Pages, Description));
            }
        }
    }
}
