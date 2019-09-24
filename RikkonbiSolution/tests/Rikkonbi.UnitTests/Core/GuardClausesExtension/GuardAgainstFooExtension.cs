using Rikkonbi.Core.Extensions;
using System;
using Xunit;

namespace Rikkonbi.UnitTests.Core.GuardClausesExtension
{
    public class GuardAgainstFooExtension
    {
        [Fact]
        public void ThrowsGivenFoo()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.Foo("foo", "aParameterName"));
        }

        [Fact]
        public void DoesNothingGivenAnythingElse()
        {
            Guard.Against.Foo("anythingElse", "aParameterName");
            Guard.Against.Foo(null, "aParameterName");
        }
    }
}