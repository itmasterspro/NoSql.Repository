# NoSql.Repository

Implementation the Repository and Unit-of-Work patterns for use NoSql databases on .Net Core

## Getting Started
Use package is very simple:

### Step 1: 
Import the package, using the command 
```posh 
Install-Package ItMastersPro.NoSql.Repository 
```

### Step 2: 
You need to create an appetting.json file in every test project and specify the parameters of the connection string to MongoDB database.
```json
{
  "MongoConnection": {
    "ConnectionString": "mongodb://username:password@host:port/<DatabaseName>?ssl=true"
  }
}
```

### Step 3: 
Add service to the container
```csharp
services.UseMongoDb(Configuration.GetSection("MongoConnection:ConnectionString").Value);
```

### Step 4: 
Inherit the class from the interface ```csharp  IEntity```. The rest of the package will do all logic for you.
```csharp
 public class Post : IEntity
{
	public DateTime Date { get; set; }
	public string Author { get; set; }
	...
}
```

### Step 5: 
Implement the interface in the controller 
```csharp
private readonly IMongoDbContext _mongoDbContext;
private readonly IRepository<Post> _postRepository;

public HomeController(IRepository<Post> postRepository, IMongoDbContext mongoDbContext)
{
	_mongoDbContext = mongoDbContext;
	_postRepository = postRepository;
}
```
Alternate path for implement the interface shown in [DependencyInjectionUnitTests.cs](https://raw.githubusercontent.com/itmasterspro/MongoDb.Repository/master/tests/MongoDb.Repository.UnitTests/DependencyInjectionUnitTests.cs).

### Step 6: 
It's all. Now you can access the methods via the interface variable, like this
```csharp
_postRepository.Insert(new Post() {Author = "Author", Date = DateTime.Now});
```

### Filter generator: 
If you need to create a search query from a data model with many fields, you can use the FilterGenerator class. You must define filters for each of the fields.
```csharp
public class TestFilter : FilterGenerator<Entity, SearchRequest>
    {
        private readonly SearchRequest _data;
        public TestFilter(SearchRequest data) : base(data)
        {
            _data = data;
        }

        private FilterDefinition<Entity> StringsTestFieldFilter()
        {
            return Builders<Entity>.Filter.Where(x => x.StringsTestField == _data.StringsTestField);
        }

        private FilterDefinition<Entity> IntTestFieldFilter()
        {
            return Builders<Entity>.Filter.Where(x => x.IntTestField == _data.IntTestField);
        }
    }
```

You can also create a method in which you define a filter consisting of several fields joined by some kind of logic.
```csharp
 public class CompositeFilter : FilterGenerator<Entity, SearchRequest>
    {
        private readonly SearchRequest _data;
        public CompositeFilter(SearchRequest data) : base(data)
        {
            _data = data;
        }

        private FilterDefinition<Entity> CompositeFieldFilter()
        {
            return Builders<Entity>.Filter.Where(x => x.StringsTestField == _data.StringsTestField && x.IntTestField != _data.IntTestField);
        }
    }
```

Next, you must merge the filters for individual fields using the AND or OR operators or list of filters by calling the appropriate method.
```csharp
   var andFilter = new TestFilter(data).AndCondition();
   var orFilter = new TestFilter(data).OrCondition();
   var listFilter = new TestFilter(data).GetFiltersList();
```

To get the list of entities from the collection, you can use a method where the received filter should be passed as a parameter.
```csharp
   var filtredList = _postRepository.Query(andFilter);
```
For more information, see the test [projects](https://github.com/itmasterspro/NoSql.Repository/blob/master/tests/NoSql.Repository.MongoDb.UnitTests/TestFilter.cs).

### Usage Identity
* Create links to libraries
```
NoSql.Repository.MongoDb.Identity.Stores
NoSql.Repository.MongoDb.IdentityProvider
```
* Add service to the container
```csharp
services.UseMongoDb(Configuration.GetSection("MongoConnection:ConnectionString").Value);
services.AddDefaultIdentity<ApplicationUser>()
                .AddMongoDbStores<IMongoDbContext>();
```
See examples for more information.

### Addition
At the moment the library implements simple methods: Insert, Updage, Delete, Find (search one record), Query (search multiple  record). We tried to make the code intuitive, in addition, we supplemented it with an annotation. You can also see unit tests as an example. Detailed documentation find [here](https://itmasterspro.github.io/MongoDb.Repository/api/index.html).

## Running the tests

To run the tests correctly, you need to create an appetting.json file in every test project. You must also specify the parameters of the connection string to MongoDB database.
```json
{
  "MongoConnection": {
    "ConnectionString": "mongodb://username:password@host:port/<DatabaseName>?ssl=true"
  }
}
```

### If you have problems while integration tests execute
Sometimes integration tests may fail to perform. This is due to the fact that the IDE runs them in parallel in different threads. Therefore, if this happens, run tests that have not been tested separately.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [repository of packages](https://github.com/itmasterspro/NoSql.Repository). 

## Authors
* **Konstantin Anisimoff** - *Initial work* - [ItMasters.Pro](https://github.com/itmasterspro)

See also the list of [contributors](https://github.com/itmasterspro/NoSql.Repository/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Happy nice code!

