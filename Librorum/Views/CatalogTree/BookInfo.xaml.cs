using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Librorum.Views.CatalogTree
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookInfo : ContentPage
    {
        public BookInfo(string BookTitle, string Author, string ImagePath, string Pages, string Description)
        {
            InitializeComponent();
            Title = "Книга \"" + BookTitle + "\"";
            iImage.Source = ImagePath;
            lTitle.Text = BookTitle;
            lAuthor.Text = Author;
            lPages.Text = Pages;
            lDescription.Text = Description;
        }
    }
}