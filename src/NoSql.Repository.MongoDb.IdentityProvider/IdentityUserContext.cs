using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItMastersPro.NoSql.Repository.MongoDb;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace NoSql.Repository.MongoDb.IdentityProvider
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class IdentityUserContext<TUser> : IdentityUserContext<TUser, Identity.IdentityUserClaim, Identity.IdentityUserLogin, Identity.IdentityUserToken>
        where TUser : Identity.IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityUserContext(string connectionString) : base(connectionString) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
    /// <typeparam name="TUserToken">The type of the user token object.</typeparam>
    public abstract class IdentityUserContext<TUser, TUserClaim, TUserLogin, TUserToken> : MongoDbContext
        where TUser : Identity.IdentityUser
        where TUserClaim : Identity.IdentityUserClaim
        where TUserLogin : Identity.IdentityUserLogin
        where TUserToken : Identity.IdentityUserToken
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="MongoDbContext"/>.</param>
        public IdentityUserContext(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of Users.
        /// </summary>
        public IMongoDbRepository<TUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of User claims.
        /// </summary>
        public IMongoDbRepository<TUserClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of User logins.
        /// </summary>
        public IMongoDbRepository<TUserLogin> UserLogins { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of User tokens.
        /// </summary>
        public IMongoDbRepository<TUserToken> UserTokens { get; set; }
    }
}
