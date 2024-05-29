using System.Collections.Generic;
using System;

namespace OAuth2_CoreMVC_Sample.Models
{
    public class CurrencyRef
    {
        public string value { get; set; }
        public string name { get; set; }
    }

    public class MetaData
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

    public class Mobile
    {
        public string FreeFormNumber { get; set; }
    }

    public class PrimaryEmailAddr
    {
        public string Address { get; set; }
    }
    public class QueryResponse
    {
        public List<Intuit.Ipp.Data.Customer> Customer { get; set; }
        public int startPosition { get; set; }
        public int maxResults { get; set; }
    }

    public class RootObject
    {
        public QueryResponse QueryResponse { get; set; }
        public DateTime time { get; set; }
    }

}
