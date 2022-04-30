using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.ViewModel
{
    public class UserToken
    {
        public string Token { get; private set; }
        public DateTime Expiration { get; private set; }
    }
}
