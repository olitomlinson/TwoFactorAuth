using System;

namespace TwoFactorAuth.Domain.Extensions
{
    public static class StringExtensions
    {
        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}