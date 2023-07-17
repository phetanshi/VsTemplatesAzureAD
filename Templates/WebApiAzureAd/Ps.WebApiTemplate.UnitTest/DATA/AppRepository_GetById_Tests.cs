﻿using Ps.EfCoreRepository.SqlServer;

namespace $safeprojectname$.Data
{
    public partial class AppRepositoryTests
    {
        [Fact]
        public void GetById_WhenCalledWithAValidId_ReturnsARecordWhichMatchesWithId()
        {

            Mock<ILogger<Repository>> mockLogging = new Mock<ILogger<Repository>>();

            var dbOptions = TestDatabaseHelper.CreateTestDatabase();

            using (var testDbContext = new TestDbContext(dbOptions))
            {
                Repository repository = new Repository(testDbContext, mockLogging.Object);
                Employee emp = repository.GetById<Employee>(1);

                Assert.Equal("TestOne", emp.FirstName);
            }
        }
    }
}
