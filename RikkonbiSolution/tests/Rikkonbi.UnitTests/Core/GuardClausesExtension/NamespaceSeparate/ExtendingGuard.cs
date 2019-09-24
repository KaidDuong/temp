using Rikkonbi.Core.Extensions;
using System;

namespace Rikkonbi.UnitTests.Core.GuardClausesExtension
{
    /// <summary>
    /// An example Guard extension method. Throws if input is "foo".
    /// </summary>
    public static class FooGuard
    {
        public static void Foo(this IGuardClause guardClause, string input, string parameterName)
        {
            if (input?.ToLower() == "foo")
                throw new ArgumentException("Should not have been foo!", parameterName);
        }
    }
}