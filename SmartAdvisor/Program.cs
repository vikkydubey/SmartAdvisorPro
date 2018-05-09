using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

namespace SmartAdvisor
{
    static class Program
    {
        /// <summary>
        /// This is a smart Advisor Tool which will validate the schema of the data returned by the REST API.
        /// It will call a set of urls and validate its responses using a schema provided in below class named 
        /// </summary>
        /// 

        //Schema Class as provided in Problem set
        public class SchemaDef
        {
            public List<string> web_pages { get; set; }
            public string name { get; set; }
            public string alpha_two_code { get; set; }

            //Had to seprately define this property because json string contains '-' in node name state-province.  Hence while Deserializing the raw json it will return always null even though a value is present
            [JsonProperty(PropertyName = "state-province")]
            public string stateprovince { get; set; }
            public List<string> domains { get; set; }
            public string country { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SmartAdvisor. A Tool to validate Json result against a schema.");

            //Store the URLs in string array
            string[] url = new string[2];
            url[0] = "https://git.io/vpg9V";
            url[1] = "https://git.io/vpg95";
            
            //Loop through string array and perform operation on each item of array
            foreach (string str in url)
            {
                
                //using web client object to fetch the json using the URL. Since it represents a REST i have utilized the web client object. 
                WebClient wp = new WebClient();
                //Get the json string in response variable
                var response = wp.DownloadString(str);
                try
                {
                    //Deserialize Raw string of json and type cast using schema class to form a list object
                    var SerializedSrray = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SchemaDef>>(response);
                    Console.WriteLine("*****************************************************************************************");
                    //Validate the response against the schema
                    Console.WriteLine("For URL: " + str + " Schema Parsed Successfully with out any errors!!!");
                    Console.WriteLine("*****************************************************************************************");
                    //Number of Objects Parsed

                    Console.WriteLine("Number Of Objects Parsed: " + Convert.ToString(SerializedSrray.Count));
                    Console.WriteLine("*****************************************************************************************");
                    //Check Null values in each node and display the null count
                    Console.WriteLine("Schema Objects with Null count:");
                    Console.WriteLine("alpha_two_code: " + SerializedSrray.Where(s => s.alpha_two_code == null).Count().ToString());
                    Console.WriteLine("country: " + SerializedSrray.Where(s => s.country == null).Count().ToString());
                    Console.WriteLine("domains: " + SerializedSrray.Where(s => s.domains.Count == 0).Count().ToString());
                    Console.WriteLine("name: " + SerializedSrray.Where(s => s.name == null).Count().ToString());
                    Console.WriteLine("stateprovince: " + SerializedSrray.Where(s => s.stateprovince == null).Count().ToString());
                    Console.WriteLine("web_pages: " + SerializedSrray.Where(s => s.web_pages.Count == 0).Count().ToString());
                    Console.WriteLine("*****************************************************************************************");


                }
                //If parsing of schema is failed then appropriate exception will be showed on console.
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
            Console.WriteLine("\n");
            Console.WriteLine("Opearation Over. Hit enter to close window.");
            //This Line is intentionally written so that console window doesnt close down automatically.
            Console.ReadLine();

        }

    }
}
