using System;

namespace TwoFactorAuth.Domain.Core.Extensions
{
    public static class FunctionalExtensions
    {
        public static T Tee<T>(this T @this, Action<T> act)
        {
            act(@this);
            return @this;
        }
    }
}
