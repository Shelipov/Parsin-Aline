using Dapper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;


namespace CoreParsingSite.Models
{
    public class Parse
    {

        int Count = 1;
        const int Count_list = 111; //страниц
        const int Count_GoodsonPage = 12; //товаров на странице
        const string Url = ("http://aline-auto.com.ua/brake-disk/?page=");
        

        public int Parsing()
        {
            Goods goods = new Goods();
            
            for (int i = 0; i < Count_list; i++)
            {
               
                var pageContent = goods.LoadPage(Url + Count);
                var document = new HtmlDocument();
                document.LoadHtml(pageContent);
                List<string> Link = new List<string>(12);
                List<string> _Price = new List<string>(12);
                List<string> _Name = new List<string>(12);
                List<string> _Stock = new List<string>(12);
                List<string> _Description = new List<string>(12);
                HtmlNodeCollection links = document.DocumentNode.SelectNodes("//a[@class='lazy']/img");
                foreach (HtmlNode link in links)
                {

                    Link.Add((link.Attributes["data-src"].Value).ToString().Replace(" ", ""));
                }
                var price = document.DocumentNode.SelectNodes("//div[@class='price']");
                foreach (HtmlNode link in price)
                {

                    _Price.Add((link.InnerText, link.GetAttributeValue("price", "")).ToString().Replace("грн", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(",", "").Replace(" ", "").Replace("(", "").Replace(")", ""));
                }
                var name = document.DocumentNode.SelectNodes("//div[@class='name']");
                foreach (HtmlNode link in name)
                {

                    _Name.Add((link.InnerText, link.GetAttributeValue("name", "")).ToString().Replace("\t", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace("(", "").Replace(")", ""));
                }
                var stock = document.DocumentNode.SelectNodes("//div[@class='stock']");
                foreach (HtmlNode link in stock)
                {

                    _Stock.Add((link.InnerText, link.GetAttributeValue("stock", "")).ToString().Replace("\t", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace("(", "").Replace(")", ""));
                }
                var description = document.DocumentNode.SelectNodes("//div[@class='description']");
                foreach (HtmlNode link in description)
                {

                    _Description.Add((link.InnerText, link.GetAttributeValue("description", "")).ToString().Replace("\t", "").Replace("\n", "").Replace("\r", "").Replace(",", "").Replace("..", "").Replace("(", "").Replace(")", ""));
                }
                for (int k = 0; k < Count_GoodsonPage; k++)
                {
                    //var id = from f in db.Goods_Table  //почему то на второй круг цикла IDENTITY - выводило в EXEPTION выкручиваемся таким образом                             
                    //         select f.Id;
                    //goods.Id = (id.Max() + 1);

                    using (WebClient webClient = new WebClient())
                    {
                        byte[] data = webClient.DownloadData(Link[k]);  //ФОТО загружать правильно в папку а в базу ссылки, тут я просто эксперементирую со скоростью
                        goods.Foto = data;
                    }
                    goods.Description = _Description[k];
                    goods.Name = _Name[k];

                    double price_double = 0.0;
                    NumberStyles style;
                    CultureInfo culture;
                    style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
                    culture = CultureInfo.CreateSpecificCulture("en-GB");
                    Double.TryParse(_Price[k], style, culture, out price_double);
                    goods.Price = price_double;

                    goods.Stock = _Stock[k];

                    
                    using (IDbConnection db = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=ParsingSite;Trusted_Connection=True;MultipleActiveResultSets=true"))
                    {
                        var sqlQuery = "INSERT INTO Goods (Foto, Description,Name,Price,Stock) VALUES(@Foto,@Description,@Name,@Price,@Stock)";
                        db.Execute(sqlQuery, goods);

                    }



                }
                Count++;

                Thread.Sleep(3000);
            }

            return Count;
        }

    }
}
