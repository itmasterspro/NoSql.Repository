using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace WebSite.Models
{
    public class ApplicationUser : NoSql.Repository.MongoDb.Identity.IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
