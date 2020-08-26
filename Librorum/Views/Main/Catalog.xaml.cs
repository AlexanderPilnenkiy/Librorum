using Android.Telecom;
using Librorum.Source.Parser;
using Librorum.Source.ViewModel;
using Librorum.Views.CatalogTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Librorum.Pages.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Catalog : ContentPage
    {
        JenresList jenresList = new JenresList();

        public Catalog()
        {
            InitializeComponent();
            jenresList.LoadList(this);
        }

        private void catalogList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string CurrentJenre = e.SelectedItem.ToString();
            if (CurrentJenre.ToString() == "Главная")
            {
                CurrentJenre = "";
            }
            else
            {
                CurrentJenre = GetInfoManager._Info[CurrentJenre];
            }
            jenresList.ChooseJenre(this, CurrentJenre);
        }
    }
}