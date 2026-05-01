using Models;

namespace Contracts
{
    public interface IJwtService
    {
        Task<string> encode(TokenDetail tokenDetail);
        Task<TokenDetail?> decode(string token);
    }
}
