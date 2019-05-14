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
                string nombreLinea = Nodo2.InnerHtml.Trim();
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
                    Console.WriteLine(json);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }

            }

            //Paradas
            doc = cWeb.Load(baseUrl+"/tiempos-de-espera/paradas");
            foreach (var Nodo in doc.DocumentNode.CssSelect("a.busStopLink"))
            {
                doc = cWeb.Load(baseUrl + Nodo.GetAttributeValue("href"));

                var Nodo2 = doc.DocumentNode.CssSelect("div.name").First();
                string nombreParada = Nodo2.InnerHtml.Trim();
                int numeroParada = int.Parse(doc.DocumentNode.CssSelect("div.icon").First().InnerHtml.Split('>')[11].Split('<')[0].Replace("L", ""));
                var lines = doc.DocumentNode.CssSelect("a.iconLine");
                List<int> lineas = new List<int>();
                foreach (var l in lines)
                {
                    int numeroLinea = int.Parse(l.GetAttributeValue("href").Split('/')[3]);
                    lineas.Add(numeroLinea);

                    HtmlDocument doc2 = cWeb.Load("http://localhost:8080/buses/search/findByLinea?numLinea=" + numeroLinea);
                    //var LineaID = doc2.DocumentNode.InnerHtml.Split("bus")[0].Split("href")[1].Split('"')[2];
                    //Console.WriteLine(LineaID);

                    //Paradas_X_Linea
                    var httpWebRequest2 = (HttpWebRequest)WebRequest.Create("http://localhost:8080/paradas/parada_x_linea");
                    httpWebRequest2.ContentType = "application/json";
                    httpWebRequest2.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest2.GetRequestStream()))
                    {
                        string json = "{\"bus_id\":\"" + numeroLinea + "\"," +
                       "\"parada_id\":" + numeroParada + "}";

                        Console.WriteLine(json);
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                    /*var httpResponse2 = (HttpWebResponse)httpWebRequest2.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse2.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();
                        Console.WriteLine(responseText);

                        //Now you have your response.
                        //or false depending on information in the response     
                    }*/

                }

                Console.WriteLine(lineas[0].ToString());
                //Console.WriteLine(nombreParada + "\n" + numeroParada);

                //Inserccion en la API

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/paradas");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"name\":\"" + nombreParada + "\"," +
                   "\"numeroParada\":" + numeroParada + "}";

                    Console.WriteLine(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }

            }
            /*
            //LIneas de cada parada
            doc = cWeb.Load(baseUrl + "/tiempos-de-espera/paradas");
            foreach (var Nodo in doc.DocumentNode.CssSelect("a.busStopLink"))
            {
                doc = cWeb.Load(baseUrl + Nodo.GetAttributeValue("href"));

                var Nodo2 = doc.DocumentNode.CssSelect("div.name").First();
                string nombreParada = Nodo2.InnerHtml.Trim();
                int numeroParada = int.Parse(doc.DocumentNode.CssSelect("div.icon").First().InnerHtml.Split('>')[11].Split('<')[0].Replace("L", ""));

                //Console.WriteLine(nombreParada + "\n" + numeroParada);

                //Inserccion en la API

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/paradas");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"name\":\"" + nombreParada + "\"," +
                   "\"numeroParada\":" + numeroParada + "}";

                    Console.WriteLine(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }

            }*/
            //Console.ReadLine();
        }
    }
}
