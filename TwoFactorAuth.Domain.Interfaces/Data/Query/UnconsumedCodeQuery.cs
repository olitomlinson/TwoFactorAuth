using System;
using System.Collections.Generic;
using TwoFactorAuth.Domain.Interfaces.Data.Query;
using TwoFactorAuth.Domain.Interfaces.Models;

namespace TwoFactorAuth.Domain.Data.Query
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