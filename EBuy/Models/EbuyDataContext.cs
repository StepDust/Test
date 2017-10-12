using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EBuy.Models {
    public class EbuyDataContext : DbContext {
        public DbSet<Auction> Auctions { get; set; }
    }
}