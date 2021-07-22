// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    public abstract class ConcurrencyDetectorEnabledRelationalTestBase<TFixture> : ConcurrencyDetectorEnabledTestBase<TFixture>
        where TFixture : ConcurrencyDetectorTestBase<TFixture>.ConcurrencyDetectorFixtureBase, new()
    {
        protected ConcurrencyDetectorEnabledRelationalTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        protected string NormalizeDelimitersInRawString(string sql)
            => (Fixture.TestStore as RelationalTestStore)?.NormalizeDelimitersInRawString(sql) ?? sql;

        [ConditionalTheory]
        [MemberData(nameof(IsAsyncData))]
        public virtual Task FromSql(bool async)
            => ConcurrencyDetectorTest(async c => async
                ? await c.Products.FromSqlRaw(NormalizeDelimitersInRawString("select * from [Products]")).ToListAsync()
                : c.Products.FromSqlRaw(NormalizeDelimitersInRawString("select * from [Products]")).ToList());
    }
}