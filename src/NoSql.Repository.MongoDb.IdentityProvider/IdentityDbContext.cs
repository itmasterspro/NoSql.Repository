using ItMastersPro.NoSql.Repository.MongoDb.Identity;
using ItMastersPro.NoSql.Repository.MongoDb.Interfaces;

namespace ItMastersPro.NoSql.Repository.MongoDb.Identity
{
    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="MongoDbContext"/>.</param>
        public IdentityDbContext(string connectionString) : base(connectionString) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of the user objects.</typeparam>
    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole> where TUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityDbContext"/>.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="MongoDbContext"/>.</param>
        public IdentityDbContext(string connectionString) : base(connectionString) { }
    }

    /// <summary>
    /// Base class for the Entity Framework database context used for identity.
    /// </summary>
    /// <typeparam name="TUser">The type of user objects.</typeparam>
    /// <typeparam name="TRole">The type of role objects.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, IdentityUserClaim, IdentityUserRole, IdentityUserLogin, IdentityRoleClaim, IdentityUserToken>
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
        /// <summary>
        /// Initializes a new instance of the db context.
        /// </summary>
        /// <param name="connectionString">The options to be used by a <see cref="MongoDbContext"/>.</param>
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
        where TUser : IdentityUser
        where TRole : IdentityRole
        where TUserClaim : IdentityUserClaim
        where TUserRole : IdentityUserRole
        where TUserLogin : IdentityUserLogin
        where TRoleClaim : IdentityRoleClaim
        where TUserToken : IdentityUserToken
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="MongoDbContext"/>.</param>
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
