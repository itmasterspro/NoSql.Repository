using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.Interfaces;
using MongoDB.Bson;

namespace ItMastersPro.NoSql.Repository.MongoDb.Identity
{
    /// <summary>
    /// Represents the link between a user and a role.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key used for users and roles.</typeparam>
    public class IdentityUserRole : IEntity
    {
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a role.
        /// </summary>
        public virtual ObjectId UserId { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        public virtual ObjectId RoleId { get; set; }

        /// <inheritdoc cref="IEntity"/>
        public ObjectId Id { get; set; }
    }
}
