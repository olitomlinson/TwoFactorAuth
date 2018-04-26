using System.Collections.Generic;
using TwoFactorAuth.Domain.Models;

namespace TwoFactorAuth.Domain.Data.Query
{
    public interface IUnconsumedCodeQuery
    {
        List<UnconsumedCode> Execute(string accountref, string phonenumber);
    }
}