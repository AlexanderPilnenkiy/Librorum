using Librorum.Source.Model;
using Librorum.Source.Parser;
using Librorum.Views.CatalogTree;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Librorum.Source.ViewModel
{
    class BooksInJenre
    {
        public List<BookPreview> Book { get; set; }

        public void GetBooksList(BooksInCategory booksInCategory, string Path)
        {
            int i = 0, j = 0, c = 0;

            GetImageManager getImageManager = new GetImageManager();
            getImageManager.DownLoadFileInBackground2(Path);
            Book = new List<BookPreview>();

            for (int q = 0; q < GetImageManager.Author.Count; q++)
            {
                if (GetImageManager.Author[i].Contains("Серия"))
                {
                    GetImageManager.Author.RemoveAt(i);
                }
            }

            string CountOfPages;

            foreach (Uri ImageUri in GetImageManager.Images)
            {
                try
                {
                    Book.Add(new BookPreview()
                    {
                        Title = GetImageManager.Name[j],
                        Author = GetImageManager.Author[i],
                        Pages = GetImageManager.Pages[c],
                        Description = "",
                        ImagePath = ImageUri.ToString()
                    });
                }
                catch
                {
                    Book.Add(new BookPreview()
                    {
                        Title = GetImageManager.Name[j],
                        Author = GetImageManager.Author[i],
                        Pages = "-//-",
                        Description = "",
                        ImagePath = ImageUri.ToString()
                    });
                }
                j++;
                i++;
                c++;
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
            booksInCategory.Content = new StackLayout { Children = { listView }, Margin = new Thickness(0, 10, 0, 0) };
        }
    }
}
