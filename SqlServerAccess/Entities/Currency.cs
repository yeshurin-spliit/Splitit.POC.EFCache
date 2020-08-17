using System;
using EFCache.POC.DatabaseAccess.Entities;

namespace EFCache.POC.SqlServer.Entities
{
    public class Currency : IEntity
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyIsoNumber { get; set; }


    }
}