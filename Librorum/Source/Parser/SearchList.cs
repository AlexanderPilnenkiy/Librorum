using System;
using System.Collections.Generic;
using System.Text;

namespace Librorum.Source.Parser
{
    class SearchList
    {
        public static List<string> WayToData = new List<string>()
        {
            ".//div/ul/li/a",   //Жанры
            ".//div/strong",    //Книги в списке жанра
            ".//p/a/img"        //Изображения
    };
    }
}
