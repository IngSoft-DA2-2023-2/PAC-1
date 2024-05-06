using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PAC.Vidly.WebApi.DataAccess;
using System.Data.Common;

namespace PAC.Vidly.WebApi.UnitTests.Repositories
{
    [TestClass]
    public sealed class RepositoryTests
    {
        private readonly DbConnection _connection;
        private readonly DbContext _dbContext;
        private readonly Repository<DummyEntity> _repository;

        public RepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            var options = new DbContextOptionsBuilder<DummyDbContext>()
            .UseSqlite(_connection)
                .Options;
            _dbContext = new DummyDbContext(options);
            _repository = new Repository<DummyEntity>(_dbContext);
        }

        [TestInitialize]
        public void Initialize()
        {
            _connection.Open();
            _dbContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
        }

        #region Add
        [TestMethod]
        public void Add_WhenInfoIsProvided_ShouldAddedToDatabase()
        {
            var entity = new DummyEntity("some name");

            _repository.Add(entity);

            var entitiesSaved = _dbContext.Set<DummyEntity>().ToList();
            entitiesSaved.Count.Should().Be(1);
            var entitySaved = entitiesSaved[0];
            entitySaved.Id.Should().Be(entity.Id);
            entitySaved.Name.Should().Be(entity.Name);
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistData_ShouldReturnIt()
        {
            var entity = new DummyEntity("some name");

            _dbContext.Set<DummyEntity>().Add(entity);

            var dummies = _repository.GetAll();

            dummies.Count.Should().Be(1);
            var entitySaved = dummies[0];
            entitySaved.Id.Should().Be(entity.Id);
            entitySaved.Name.Should().Be(entity.Name);
        }
        #endregion
    }

    internal sealed class DummyDbContext : DbContext
    {
        public DbSet<DummyEntity> Dummies { get; set; }

        public DummyDbContext(DbContextOptions options)
        : base(options)
        {
        }
    }

    internal sealed record class DummyEntity
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public DummyEntity()
        {
            Id = Guid.NewGuid().ToString(); 
        }

        public DummyEntity(string name)
            : this()
        {
            Name = name;
        }
    }
}
