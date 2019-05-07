using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace surbus_scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            String baseUrl = "http://www.surbusalmeria.es";
            HtmlWeb cWeb = new HtmlWeb();


            HtmlDocument doc = cWeb.Load(baseUrl+"/lineas");

            //Lineas
            foreach (var Nodo in doc.DocumentNode.CssSelect("a.lines"))
            {
                if (Nodo.GetAttributeValue("href") == "/lineas") continue;
                doc = cWeb.Load(baseUrl + Nodo.GetAttributeValue("href"));

                var Nodo2 = doc.DocumentNode.CssSelect("div.name").First();
                string nombreLinea = Nodo2.InnerHtml;
                string imagenLinea = baseUrl + doc.DocumentNode.CssSelect("div.info").First().InnerHtml.Split('"')[5];
                int numeroLinea = int.Parse(doc.DocumentNode.CssSelect("div.icon").First().InnerHtml.Split('>')[11].Split('<')[0].Replace("L",""));

               // Console.WriteLine(nombreLinea + "\n" + imagenLinea + "\n" + numeroLinea);

                //Inserccion en la API
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/buses");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"nombre\":\""+nombreLinea+"\"," + "\"imagen\":\"" + imagenLinea +"\","+
                   "\"numLinea\":\""+numeroLinea+"\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            //Paradas
            doc = cWeb.Load(baseUrl+"/tiempos-de-espera/paradas");
            foreach (var Nodo in doc.DocumentNode.CssSelect("a.busStopLink"))
            {
                doc = cWeb.Load(baseUrl + Nodo.GetAttributeValue("href"));

                var Nodo2 = doc.DocumentNode.CssSelect("div.name").First();
                string nombreParada = Nodo2.InnerHtml;
                int numeroParada = int.Parse(doc.DocumentNode.CssSelect("div.icon").First().InnerHtml.Split('>')[11].Split('<')[0].Replace("L", ""));

                //Console.WriteLine(nombreParada + "\n" + numeroParada);

                //Inserccion en la API

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/paradas");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"name\":\"" + nombreParada + "\"," +
                   "\"numeroParada\":\"" + numeroParada + "\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            //Console.ReadLine();
        }
    }
}
