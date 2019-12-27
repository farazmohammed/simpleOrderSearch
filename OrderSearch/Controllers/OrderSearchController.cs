using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using OrderSearch.Models;
using System.IO;

namespace OrderSearch.Controllers
{
    public class OrderSearchController : ApiController
    {
        List<OrderData> orders;
        public OrderSearchController()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"\bin\orderInfo.json";
            //filepath = @"..\\OrderSearch\bin\orderInfo.json";
              using (StreamReader r = new StreamReader(filepath))
                {
                    string json = r.ReadToEnd();
                    orders = JsonConvert.DeserializeObject<List<OrderData>>(json);
                }
        }

        // GET api/ordersearch
        public OrderSearchOutput Get()
        {
            OrderSearchOutput resOutput = new OrderSearchOutput();
            resOutput.ServiceError = 1; // Warning to indicate incorrect use of service
            resOutput.ErrorMessage = "Please provide Order ID..";
            
            //return new string[] { "value1", "value2" };
            return resOutput;
        }

        // GET api/ordersearch
        /// <summary>
        /// This program searches the list of orders using order ID and completion date from parameters
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="completionDte"></param>
        /// <returns></returns>
        public OrderSearchOutput Get(int orderid, string completionDte)
        {
            OrderSearchOutput resOutput = new OrderSearchOutput();
            OrderData res = null;
            DateTime pcompletionDte;
            try
            {
                if (!DateTime.TryParse(completionDte, out pcompletionDte))
                {
                    resOutput.ServiceError = 1; // Warning to indicate incorrect use of service
                    resOutput.ErrorMessage = "Invalid completion date..";

                    //return new string[] { "value1", "value2" };
                    return resOutput;
                }
                foreach (OrderData o in orders)
                {
                    if (o.OrderID == orderid)
                    {
                        if (DateTime.Equals(o.CompletionDte, pcompletionDte))
                        {
                            res = o;
                            break;
                        }
                    }
                }
                if (res == null)
                {
                    resOutput.ServiceError = 1;
                    resOutput.ErrorMessage = "Order not found";
                }
                else
                {
                    //return JsonConvert.SerializeObject(res);
                    resOutput.ServiceError = 0;
                    resOutput.ErrorMessage = "";
                    resOutput.OrderData = JsonConvert.SerializeObject(res); 
                }
            }
            catch (FileNotFoundException fex)
            {
                //return "The order file: " + fex.FileName + " was not found. Contact system admin";
                resOutput.ServiceError = 2;
                resOutput.ErrorMessage = "The order file: " + fex.FileName + " was not found. Contact system admin";
                resOutput.OrderData = "";
            }
            catch (Exception ex)
            {
                resOutput.ServiceError = 2;
                resOutput.ErrorMessage = "Unhandled exception: " + ex.ToString();
                resOutput.OrderData = "";
            }
            return resOutput;
        }
        // GET api/ordersearch
        /// <summary>
        /// This function searches an order based on MSA, pStatus and completion date
        /// </summary>
        /// <param name="msa"></param>
        /// <param name="Status"></param>
        /// <param name="pcomletionDte"></param>
        /// <returns></returns>
        public OrderSearchOutput Get(int msa, int Status, string completionDte)
        {
            OrderSearchOutput resOutput = new OrderSearchOutput();
            OrderData res = null;
            DateTime pcompletionDte;
            try
            {
                if (!DateTime.TryParse(completionDte, out pcompletionDte))
                {
                    resOutput.ServiceError = 1; // Warning to indicate incorrect use of service
                    resOutput.ErrorMessage = "Invalid completion date..";

                    //return new string[] { "value1", "value2" };
                    return resOutput;
                }
                foreach (OrderData o in orders)
                {
                    if (o.MSA == msa && o.Status == Status)
                    {
                        if (DateTime.Equals(o.CompletionDte, pcompletionDte))
                        {
                            res = o;
                            break;
                        }
                    }
                }
                if (res == null)
                {
                    resOutput.ServiceError = 1;
                    resOutput.ErrorMessage = "Order not found";
                }
                else
                {
                    //return JsonConvert.SerializeObject(res);
                    resOutput.ServiceError = 0;
                    resOutput.ErrorMessage = "";
                    resOutput.OrderData = JsonConvert.SerializeObject(res);
                }
            }
            catch (FileNotFoundException fex)
            {
                //return "The order file: " + fex.FileName + " was not found. Contact system admin";
                resOutput.ServiceError = 2;
                resOutput.ErrorMessage = "The order file: " + fex.FileName + " was not found. Contact system admin";
                resOutput.OrderData = "";
            }
            catch (Exception ex)
            {
                resOutput.ServiceError = 2;
                resOutput.ErrorMessage = "Unhandled exception: " + ex.ToString();
                resOutput.OrderData = "";
            }
            return resOutput;
        }

        // POST api/ordersearch
        public void Post([FromBody]string value)
        {
        }

        // PUT api/ordersearch/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ordersearch/5
        public void Delete(int id)
        {
        }
    }
}
