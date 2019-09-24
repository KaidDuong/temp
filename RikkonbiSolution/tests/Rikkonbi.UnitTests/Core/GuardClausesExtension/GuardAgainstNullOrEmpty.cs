using Rikkonbi.Core.Extensions;
using System;
using System.Linq;
using Xunit;

namespace Rikkonbi.UnitTests.Core.GuardClausesExtension
{
    public class GuardAgainstNullOrEmpty
    {
        [Fact]
        public void DoesNothingGivenNonEmptyStringValue()
        {
            Guard.Against.NullOrEmpty("a", "string");
            Guard.Against.NullOrEmpty("1", "aNumericString");
        }

        [Fact]
        public void DoesNothingGivenNonEmptyEnumerable()
        {
            Guard.Against.NullOrEmpty(new[] { "foo", "bar" }, "stringArray");
            Guard.Against.NullOrEmpty(new[] { 1, 2 }, "intArray");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrEmpty(null, "null"));
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty("", "emptystring"));
        }

        [Fact]
        public void ThrowsGivenEmptyEnumerable()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrEmpty(Enumerable.Empty<string>(), "emptyStringEnumerable"));
        }
    }
}