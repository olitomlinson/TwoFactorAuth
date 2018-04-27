using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Interfaces.Data.Query
{
    public interface IUnconsumedCodeQuery
    {
        List<UnconsumedCode> Execute(string accountref, string phonenumber);
    }
}