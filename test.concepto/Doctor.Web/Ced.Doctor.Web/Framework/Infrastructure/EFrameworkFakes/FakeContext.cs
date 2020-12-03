using Microsoft.EntityFrameworkCore;
using Framework.Core;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Infrastructure.EFrameworkFakes
{
    public static class FakeContext
    {
        public static DbSet<T> ContextSet<T>(IEnumerable<T> entities) where T : BaseEntity
        {

            var result = entities.GetQueryableMockDbSet();
            foreach (var entity in entities)
            {
                result.Add(entity);
            }

            return result;

        }

        private static DbSet<T> GetQueryableMockDbSet<T>(this IEnumerable<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}
