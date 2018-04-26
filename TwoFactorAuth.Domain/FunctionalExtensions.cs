using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoFactorAuth.Domain
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
