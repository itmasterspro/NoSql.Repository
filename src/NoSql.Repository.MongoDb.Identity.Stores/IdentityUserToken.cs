using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;

namespace NoSql.Repository.MongoDb.Identity
{
    /// <summary>
    /// Represents an authentication token for a user.
    /// </summary>
    public class IdentityUserToken : IEntity
    {
        /// <summary>
        /// Gets or sets the primary key of the user that the token belongs to.
        /// </summary>
        public virtual ObjectId UserId { get; set; }

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        [ProtectedPersonalData]
        public virtual string Value { get; set; }

        /// <inheritdoc cref="IEntity"/>
        public ObjectId Id { get; set; }
    }
}
