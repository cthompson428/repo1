using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        private const string _url = "http://files.olo.com/pizzas.json";

        public static void Main()
        {

            try
            {
                WebClient webClient = new WebClient();
                string request = webClient.DownloadString(_url);
                Dictionary<string, int> pizzaCombinations = new Dictionary<string, int>();

 
                List<PizzaToppings> downloadedToppings = JsonConvert.DeserializeObject<List<PizzaToppings>>(request);

                downloadedToppings.ForEach(topping =>
                {
                    string combination = string.Join(",", topping.toppings.OrderBy(x => x));

                    if (pizzaCombinations.ContainsKey(combination))
                    {
                        pizzaCombinations[combination]++;
                    }
                    else
                    {
                        pizzaCombinations.Add(combination, 1);
                    }
                });


                Console.WriteLine("Top 20 most frequently ordered pizza topping combinations" + Environment.NewLine);

                Console.WriteLine(
                    string.Join(Environment.NewLine, pizzaCombinations
                        .OrderByDescending(x => x.Value)
                        .Take(20)
                        .Select(x => "Combination: " + x.Key  + ", Ordered: " + x.Value + " times, Rank:" + pizzaCombinations[x.Key]))
                );

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }

            Console.ReadLine();
        }
    }





    public class Rootobject
    {
        public List<PizzaToppings> Property1 { get; set; }
    }

    public class PizzaToppings
    {
        public List<string> toppings { get; set; }
    }



}
