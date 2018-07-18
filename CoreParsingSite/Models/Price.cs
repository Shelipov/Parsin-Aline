using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreParsingSite.Models
{
    public class Price  //проверка на наличие изменений в цене товара
    {
        
        
        Dictionary<string, string> Name = new Dictionary<string, string>();
        public Dictionary<string, string> Changes(List<Goods>list)
        {
            

            string url = "http://aline-auto.com.ua/index.php?route=product/search&search=";
            Goods goods = new Goods();
            string Price_String;
            foreach (var count in list)
            {
                string Count = count.Name.Replace("FREMAX ", "").Replace("Zimmermann ", "");
                var pageContent = goods.LoadPage(url + Count);
                var document = new HtmlDocument();
                document.LoadHtml(pageContent);
                var price = document.DocumentNode.SelectNodes("//div[@class='price']");
                var DOOM = from name in list
                           where name.Name == count.Name
                           select name.Price.ToString();

                if (price != null)
                {

                    foreach (HtmlNode link in price)
                    {

                        Price_String = ((link.InnerText, link.GetAttributeValue("price", "")).ToString().Replace("грн", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(",", "").Replace(" ", "").Replace("(", "").Replace(")", ""));

                        foreach (var doom in DOOM)
                        {
                            
                                if (doom != Price_String)
                                {
                                    Name.Add(count.Name, Price_String);
                                }
                            
                            
                        }
                        break; //интересует первый только элемент

                    }
                }
                else { continue; }


            }
            
            return Name;
        }
    }
}
