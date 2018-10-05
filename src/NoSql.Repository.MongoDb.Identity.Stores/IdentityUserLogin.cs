using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.Interfaces;
using MongoDB.Bson;

namespace ItMastersPro.NoSql.Repository.MongoDb.Identity
{
    /// <summary>
    /// Represents a login and its associated provider for a user.
    /// </summary>
    public class IdentityUserLogin : IEntity
    {
        /// <summary>
        /// Gets or sets the login provider for the login (e.g. facebook, google)
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the unique provider identifier for this login.
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the friendly name used in a UI for this login.
        /// </summary>
        public virtual string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the user associated with this login.
        /// </summary>
        public virtual ObjectId UserId { get; set; }

        /// <inheritdoc cref="IEntity"/>
        public ObjectId Id { get; set; }
    }
}
