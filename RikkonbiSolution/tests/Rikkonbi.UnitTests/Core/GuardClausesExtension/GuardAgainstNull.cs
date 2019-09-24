using Rikkonbi.Core.Extensions;
using System;
using Xunit;

namespace Rikkonbi.UnitTests.Core.GuardClausesExtension
{
    public class GuardAgainstNull
    {
        [Fact]
        public void DoesNothingGivenNonNullValue()
        {
            Guard.Against.Null("", "string");
            Guard.Against.Null(1, "int");
            Guard.Against.Null(Guid.Empty, "guid");
            Guard.Against.Null(DateTime.Now, "datetime");
            Guard.Against.Null(new Object(), "object");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against.Null(null, "null"));
        }
    }
}