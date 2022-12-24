using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EstoqueApi.Config.Identity {
    public class JwtSecurityKey {
        public static SymmetricSecurityKey Create(string secret) {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
