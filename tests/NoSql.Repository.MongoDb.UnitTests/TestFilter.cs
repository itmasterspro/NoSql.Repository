using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItMastersPro.NoSql.Repository.MongoDb.Filter;
using MongoDB.Driver;
using NoSql.Repository.MongoDb.UnitTests.Entity;
using NoSql.Repository.MongoDb.UnitTests.Utility;
using Xunit;

namespace NoSql.Repository.MongoDb.UnitTests
{
    public class TestFilter : FilterGenerator<TesTEntity1, TesTEntity1>
    {
        private readonly TesTEntity1 _data;
        public TestFilter(TesTEntity1 data) : base(data)
        {
            _data = data;
        }

        private FilterDefinition<TesTEntity1> StringsTestFieldFilter()
        {
            return Builders<TesTEntity1>.Filter.Where(x => x.StringsTestField == _data.StringsTestField);
        }

        private FilterDefinition<TesTEntity1> IntTestFieldFilter()
        {
            return Builders<TesTEntity1>.Filter.Where(x => x.IntTestField == _data.IntTestField);
        }
    }

    public class CompositeFilter : FilterGenerator<TesTEntity1, TesTEntity1>
    {
        private readonly TesTEntity1 _data;
        public CompositeFilter(TesTEntity1 data) : base(data)
        {
            _data = data;
        }

        private FilterDefinition<TesTEntity1> CompositeFieldFilter()
        {
            return Builders<TesTEntity1>.Filter.Where(x => x.StringsTestField == _data.StringsTestField && x.IntTestField != _data.IntTestField);
        }
    }

    public class FilterGeneratorUnitTest
    {
        [Fact]
        public void StringsTestFieldFilterTest()
        {
            var data = new TesTEntity1 { StringsTestField = "Str 1", IntTestField = 1 };
            var filter = Builders<TesTEntity1>.Filter.Where(x => x.StringsTestField == data.StringsTestField);

            var realtySaleFilter = new TestFilter(data).GetFiltersList()?.FirstOrDefault();

            Assert.NotNull(realtySaleFilter);
            var t1 = realtySaleFilter.GetFilterExpresstion();
            var t2 = filter.GetFilterExpresstion();
            Assert.Equal(realtySaleFilter.GetFilterExpresstion(), filter.GetFilterExpresstion());
        }

        [Fact]
        public void IntTestFieldFilterTest()
        {
            var data = new TesTEntity1 { StringsTestField = "Str 1", IntTestField = 1 };
            var filter = Builders<TesTEntity1>.Filter.Where(x => x.IntTestField == data.IntTestField);

            var realtySaleFilter = new TestFilter(data).GetFiltersList()?.LastOrDefault();

            Assert.NotNull(realtySaleFilter);
            var t1 = realtySaleFilter.GetFilterExpresstion();
            var t2 = filter.GetFilterExpresstion();
            Assert.Equal(realtySaleFilter.GetFilterExpresstion(), filter.GetFilterExpresstion());
        }

        [Fact]
        public void AndConditionTest()
        {
            var data = new TesTEntity1 { StringsTestField = "Str 1", IntTestField = 1 };
            var filter = Builders<TesTEntity1>.Filter.Where(x => x.StringsTestField == data.StringsTestField && x.IntTestField == data.IntTestField);

            var realtySaleFilter = new TestFilter(data).And();

            Assert.NotNull(realtySaleFilter);
            var t1 = realtySaleFilter.GetFilterExpresstion();
            var t2 = filter.GetFilterExpresstion();
            Assert.Equal(realtySaleFilter.GetFilterExpresstion(), filter.GetFilterExpresstion());
        }

        [Fact]
        public void OrConditionTest()
        {
            var data = new TesTEntity1 { StringsTestField = "Str 1", IntTestField = 1 };
            var filter = Builders<TesTEntity1>.Filter.Where(x => x.StringsTestField == data.StringsTestField || x.IntTestField == data.IntTestField);

            var realtySaleFilter = new TestFilter(data).Or();

            Assert.NotNull(realtySaleFilter);
            var t1 = realtySaleFilter.GetFilterExpresstion();
            var t2 = filter.GetFilterExpresstion();
            Assert.Equal(realtySaleFilter.GetFilterExpresstion(), filter.GetFilterExpresstion());
        }

        [Fact]
        public void CompositeFieldTest()
        {
            var data = new TesTEntity1 { StringsTestField = "Str 1", IntTestField = 1 };
            var filter = Builders<TesTEntity1>.Filter.Where(x => x.StringsTestField == data.StringsTestField && x.IntTestField != data.IntTestField);

            var fl = new CompositeFilter(data).GetFiltersList();
            var realtySaleFilter = Builders<TesTEntity1>.Filter.And(fl);

            Assert.NotNull(realtySaleFilter);
            var t1 = realtySaleFilter.GetFilterExpresstion();
            var t2 = filter.GetFilterExpresstion();
            Assert.Equal(realtySaleFilter.GetFilterExpresstion(), filter.GetFilterExpresstion());
        }
    }
}
