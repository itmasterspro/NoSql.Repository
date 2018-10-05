using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMastersPro.NoSql.Repository.MongoDb.Identity;
using MongoDB.Bson;

namespace WebSite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
