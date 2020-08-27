using Librorum.Pages.Main;
using Librorum.Source.Parser;
using Librorum.Views.CatalogTree;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Librorum.Source.ViewModel
{
    class JenresList
    {
        GetInfoManager getInfoManager = new GetInfoManager();

        public void LoadList(Catalog catalog)
        {
            try
            {
                catalog.catalogList.ItemsSource = getInfoManager.Info(SearchList.WayToData[0], Source.Configs.Connection.Library).Keys;
            }
            catch 
            {
                //TODO Connection Error Event
                catalog.DisplayAlert("Ошибка подключения", "Каталог библиотеки недоступен без подключения к сети. " +
                    "Доступна только сохранённая библиотека", "ОK");
            }
        }

        public async void ChooseJenre(Catalog catalog, string CurrentJenre)
        {
            await catalog.Navigation.PushAsync(new BooksInCategory(catalog.catalogList.SelectedItem.ToString(), CurrentJenre));
        }
    }
}
