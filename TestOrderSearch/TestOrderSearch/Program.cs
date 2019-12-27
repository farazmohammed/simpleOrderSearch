using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Configuration;

namespace TestOrderSearch
{
    class Program
    {
        static int pOrderID, pMSA, pStatus;
        static DateTime pCompletionDte;
        static bool validInput = false;
        static string struserInput = "";

        static void Main()
        {
            // Update port # in the following line.
            HttpClient client; //= new HttpClient();
            string struri = "";
            string responseAsString = "";
            //List<OrderSearchOutput> ord = null;
            
            int userinput;
            int menuRes = 0;
            try
            {
                while(true)
                {
                    validInput = true;
                    menuRes = 0;
                    //struri = "http://localhost:64628/api/OrderSearch?";
                    struri = ConfigurationManager.AppSettings["SearchURL"];
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    menuRes = PrepareMenu(0);
                    if(!int.TryParse(struserInput,out userinput))
                    {
                        Console.WriteLine("Invalid input.. Please retry or enter 9999 to exit");
                        continue;
                    }
                    if (userinput == 1)
                    {
                        menuRes= PrepareMenu(1);
                        struri += "orderid=" + pOrderID.ToString();
                    }
                    else if( userinput == 2)
                    {
                        menuRes= PrepareMenu(2);
                        struri += "MSA=" + pMSA.ToString() + "&Status=" + pStatus.ToString();
                    }
                    else if (userinput == 9999)
                        return;
                    else
                    {
                        Console.WriteLine("Invalid input.. Please retry or enter 9999 to exit");
                        continue;
                    }

                    if(menuRes == 0)
                    {
                        // Get the order data
                        struri += "&completionDte=" + pCompletionDte.ToString();
                        client.BaseAddress = new Uri(struri);
                        HttpResponseMessage response = client.GetAsync(client.BaseAddress.AbsoluteUri).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            //Convert result into business object
                            responseAsString = response.Content.ReadAsStringAsync().Result;
                            //ord = JsonConvert.DeserializeObject<List<OrderSearchOutput>>(responseAsString);
                            //Console.WriteLine(responseAsString);
                        }

                        Console.WriteLine(responseAsString);
                    }
                    
                }
               
    
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static int PrepareMenu(int id)
        {
            Console.WriteLine();
            if (id == 0)
            {
                Console.WriteLine("Search Main Menu:..");
                Console.WriteLine("1. Search by OrderID and Completion date");
                Console.WriteLine("2. Search by MSA, Status and Completion date");
                Console.WriteLine("9999. Exit");
                Console.WriteLine("Select one of the above: ");
                struserInput = Console.ReadLine();
                return 0;
            }
            else if (id == 1)
            {
                Console.WriteLine("Input search criteria");
                Console.WriteLine("Enter Order ID (9999 to exit) : ");
                struserInput = Console.ReadLine();
                validInput = int.TryParse(struserInput, out pOrderID);
                if (validInput)
                {
                    if (pOrderID == 9999)
                    {
                        Console.WriteLine("Exiting...");
                        return 9999;
                    }

                }
            }
            else if (id == 2)
            {
                Console.WriteLine("Input search criteria");
                Console.WriteLine("Enter MSA : ");
                struserInput = Console.ReadLine();
                validInput = int.TryParse(struserInput, out pMSA);


                Console.WriteLine("Enter Status : ");
                struserInput = Console.ReadLine();
                validInput = validInput && int.TryParse(struserInput, out pStatus);

            }

            Console.WriteLine("Enter Complete Date : ");
            struserInput = Console.ReadLine();
            validInput = validInput && DateTime.TryParse(struserInput, out pCompletionDte);


            if (validInput == true)
            {
                Console.WriteLine("Getting data for order: " + pOrderID.ToString());
                return 0;
            }
            else
            {
                Console.WriteLine("Invalid input, please enter valid input ...");
                return -1;
            }


        }
    }
}