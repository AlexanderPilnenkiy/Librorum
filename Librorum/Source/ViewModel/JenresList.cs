using Librorum.Pages.Main;
using Librorum.Source.Parser;
using Librorum.Views.CatalogTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Librorum.Source.ViewModel
{
    class JenresList
    {
        GetInfoManager getInfoManager = new GetInfoManager();

        public void LoadList(Catalog catalog)
        {
            catalog.catalogList.ItemsSource = getInfoManager.Info(SearchList.WayToData[0], Source.Configs.Connection.Library).Keys;
        }

        public async void ChooseJenre(Catalog catalog, string CurrentJenre)
        {
            await catalog.Navigation.PushAsync(new BooksInCategory(catalog.catalogList.SelectedItem.ToString(), CurrentJenre));
        }
    }
}
