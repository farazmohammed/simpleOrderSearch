using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderSearch.Models
{
    public class OrderData
    {
        public int OrderID;
        public int ShipperID;
        public int DriverID;
        public DateTime CompletionDte;
        public int Status;
        public string Code;
        public int MSA;
        public Decimal Duration;
        public int OfferType;
    }

}