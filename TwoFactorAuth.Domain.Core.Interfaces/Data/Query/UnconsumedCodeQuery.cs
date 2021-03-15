using System;
using System.Collections.Generic;
using TwoFactorAuth.Domain.Core.Interfaces.Data.Query;
using TwoFactorAuth.Domain.Core.Interfaces.Models;

namespace TwoFactorAuth.Domain.Core.Data.Query
{
    public class UnconsumedCodeQuery : IUnconsumedCodeQuery
    {
        public UnconsumedCodeQuery()
        {
            //pass in a session obj   
        }
        public List<UnconsumedCode> Execute(string accountref, string phonenumber)
        {
            throw new NotImplementedException();
        }
    }
}