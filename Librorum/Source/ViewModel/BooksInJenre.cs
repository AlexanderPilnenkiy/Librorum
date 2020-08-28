using Librorum.Source.Model;
using Librorum.Source.Parser;
using Librorum.Views.CatalogTree;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace Librorum.Source.ViewModel
{
    class BooksInJenre
    {
        public List<BookPreview> Book { get; set; }

        public void GetBooksList(BooksInCategory booksInCategory, string Path)
        {
            int author = 0, title = 0, pages = 0, description = 0;

            GetImageManager.DownLoadFileInBackground2(Path);
            Book = new List<BookPreview>();

            for (int q = 0; q < GetImageManager.Author.Count; q++)
            {
                if (GetImageManager.Author[author].Contains("Серия"))
                {
                    GetImageManager.Author.RemoveAt(author);
                }
            }

            foreach (Uri ImageUri in GetImageManager.Images)
            {
                try
                {
                    Book.Add(new BookPreview()
                    {
                        Title = GetImageManager.Name[title],
                        Author = GetImageManager.Author[author],
                        Pages = GetImageManager.Pages[pages],
                        Description = GetImageManager.Description[description],
                        ImagePath = ImageUri.ToString()
                    });
                }
                catch
                {
                    Book.Add(new BookPreview()
                    {
                        Title = GetImageManager.Name[title],
                        Author = GetImageManager.Author[author],
                        Pages = "-//-",
                        Description = GetImageManager.Description[description],
                        ImagePath = ImageUri.ToString()
                    });
                }
                title++;
                author++;
                pages++;
                description++;
            }

            ListView listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = Book,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Black, DetailColor = Color.Black };
                    imageCell.Height = 100;
                    imageCell.SetBinding(ImageCell.TextProperty, "Title");
                    Binding companyBinding = new Binding { Path = "Author", StringFormat = "{0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "ImagePath");
                    return imageCell;
                })
            };
            listView.ItemSelected += booksInCategory.SelectBook;

            Button bNavigationForward = new Button
            {
                Text = "Вперёд",
                BorderColor = Color.Red,
                BorderWidth = 3,
                CornerRadius = 15,
                BackgroundColor = Color.White
            };
            bNavigationForward.Clicked += booksInCategory.ClickedForwardButton;

            Button bNavigationBack = new Button
            {
                Text = "Назад",
                BorderColor = Color.Red,
                BorderWidth = 3,
                CornerRadius = 15,
                BackgroundColor = Color.White,
            };
            bNavigationBack.Clicked += booksInCategory.ClickedBackButton;

            Grid grid = new Grid();
            grid.Children.Add(bNavigationBack);
            grid.Children.Add(bNavigationForward);
            Grid.SetColumn(bNavigationBack, 0);
            Grid.SetColumn(bNavigationForward, 1);

            booksInCategory.Content = new StackLayout { Children = { listView, grid }, Margin = new Thickness(0, 10, 0, 0) };
        }
    }
}
