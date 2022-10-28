using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtSample;

//For Decode - https://jwt.io/
public class UserServices
{
    private const string SecretKey = "j&fOM5%kk>BgcFcLt}L7"; //160-bit WPA Key

    private IDictionary<string, string> _users = new Dictionary<string, string>()
    {
        {"user1", "test1" },
        {"user2", "test2" },
        {"user3", "test3" },
        {"user4", "test4" },
        {"user5", "test5" }
    };
    public string Login(string userName, string userPassword)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPassword))
        {
            return string.Empty;
        }

        int count = 0;
        foreach (var user in _users)
        {
            if (string.CompareOrdinal(user.Key, userName) == 0 && 
                string.CompareOrdinal(user.Value, userPassword) == 0)
            {
                return GenerateJwtToken(count, userName);
            }

            count++;
        }

        return string.Empty;
    }

    private string GenerateJwtToken(int id, string userName)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(SecretKey);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
        securityTokenDescriptor.Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, id.ToString())
        });
        securityTokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(15);
        securityTokenDescriptor.SigningCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(securityToken);
    }
}
