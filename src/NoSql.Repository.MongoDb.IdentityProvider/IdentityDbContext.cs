using System;
using System.Collections.Generic;
using System.Text;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;
using MongoDB.Bson;

namespace NoSql.Repository.MongoDb.IdentityProvider
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<Identity.IdentityUser, Identity.IdentityRole>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(string connectionString) : base(connectionString) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, Identity.IdentityRole> where TUser : Identity.IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(string connectionString) : base(connectionString) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, Identity.IdentityUserClaim, Identity.IdentityUserRole, Identity.IdentityUserLogin, Identity.IdentityRoleClaim, Identity.IdentityUserToken>
        where TUser : Identity.IdentityUser
        where TRole : Identity.IdentityRole
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(string connectionString) : base(connectionString) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
    /// <typeparam name="TUserRole">The type of the user role object.</typeparam>
    /// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
    /// <typeparam name="TRoleClaim">The type of the role claim object.</typeparam>
    /// <typeparam name="TUserToken">The type of the user token object.</typeparam>
    public abstract class IdentityDbContext<TUser, TRole, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityUserContext<TUser, TUserClaim, TUserLogin, TUserToken>
        where TUser : Identity.IdentityUser
        where TRole : Identity.IdentityRole
        where TUserClaim : Identity.IdentityUserClaim
        where TUserRole : Identity.IdentityUserRole
        where TUserLogin : Identity.IdentityUserLogin
        where TRoleClaim : Identity.IdentityRoleClaim
        where TUserToken : Identity.IdentityUserToken
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public IdentityDbContext(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of User roles.
        /// </summary>
        public IMongoDbRepository<TUserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of roles.
        /// </summary>
        public IMongoDbRepository<TRole> Roles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMongoDbRepository{TEntity}"/> of role claims.
        /// </summary>
        public IMongoDbRepository<TRoleClaim> RoleClaims { get; set; }
    }
}
