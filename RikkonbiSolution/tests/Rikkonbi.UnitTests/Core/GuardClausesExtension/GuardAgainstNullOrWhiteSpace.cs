﻿using Rikkonbi.Core.Extensions;
using System;
using Xunit;

namespace Rikkonbi.UnitTests.Core.GuardClausesExtension
{
    public class GuardAgainstNullOrWhiteSpace
    {
        [Theory]
        [InlineData("a")]
        [InlineData("1")]
        [InlineData("some text")]
        [InlineData(" leading whitespace")]
        [InlineData("trailing whitespace ")]
        public void DoesNothingGivenNonEmptyStringValue(string nonEmptyString)
        {
            Guard.Against.NullOrWhiteSpace(nonEmptyString, "string");
            Guard.Against.NullOrWhiteSpace(nonEmptyString, "aNumericString");
        }

        [Fact]
        public void ThrowsGivenNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.Against.NullOrWhiteSpace(null, "null"));
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrWhiteSpace("", "emptystring"));
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("   ")]
        public void ThrowsGivenWhiteSpaceString(string whiteSpaceString)
        {
            Assert.Throws<ArgumentException>(() => Guard.Against.NullOrWhiteSpace(whiteSpaceString, "whitespacestring"));
        }
    }
}