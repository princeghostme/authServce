using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IJwtService
    {
        Task<string> encode(TokenDetail userData);
        Task<TokenDetail> decode(string Token);
    }
}
