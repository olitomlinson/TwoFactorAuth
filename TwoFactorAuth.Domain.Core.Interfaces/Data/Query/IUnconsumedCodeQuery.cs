using System.Collections.Generic;
using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core.Interfaces.Data.Query
{
    public interface IUnconsumedCodeQuery
    {
        List<UnconsumedCode> Execute(string accountref, string phonenumber);
    }
}