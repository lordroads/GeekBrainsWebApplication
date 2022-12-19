using EmployeeService.Database.Data;
using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests;
using EmployeeService.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeService.Services.Implementations
{
    public class AuthenticateService : IAuthenticateService
    {
        public const string SecretKey = "j&fOM5%kk>BgcFcLt}L7";
        private readonly Dictionary<string, SessionDto> _sessions = new Dictionary<string, SessionDto>();
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public SessionDto GetSession(string sessionToken)
        {
            SessionDto sessionDto;

            lock (_sessions)
            {
                _sessions.TryGetValue(sessionToken, out sessionDto);
            }

            if (sessionDto == null)
            {
                using IServiceScope scope = _serviceScopeFactory.CreateScope();
                EmployeeServiceDbContext context = scope.ServiceProvider.GetRequiredService<EmployeeServiceDbContext>();

                AccountSession session = context.AccountSessions.FirstOrDefault(item => item.SessionToken == sessionToken);

                if (session == null)
                {
                    return null;
                }

                Account account = context.Accounts.FirstOrDefault(account => account.AccountId == session.AccountId);

                sessionDto = GetSessionDto(account, session);

                lock (_sessions)
                {
                    _sessions[sessionToken] = sessionDto;
                }
            }

            return sessionDto;
        }

        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
            EmployeeServiceDbContext context = serviceScope.ServiceProvider.GetRequiredService<EmployeeServiceDbContext>();

            Account account = FindAccountByLogin(context, authenticationRequest.Login);

            if (account == null)
            {
                return new AuthenticationResponse
                {
                    Status = AuthenticationStatus.UserNotFound
                };
            }

            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse { Status = AuthenticationStatus.InvalidPassword };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeLastRequest = DateTime.Now,
                IsClosed = false
            };

            context.AccountSessions.Add(session);
            context.SaveChanges();

            SessionDto sessionDto = GetSessionDto(account, session);

            lock (_sessions)
            {
                _sessions[session.SessionToken] = sessionDto;
            }

            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                Session = sessionDto
            };
        }

        private Account FindAccountByLogin(EmployeeServiceDbContext context, string login)
        {
            return context.Accounts.FirstOrDefault(account => account.EMail == login);
        }

        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
            securityTokenDescriptor.Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, account.EMail),
            new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString())
            });
            securityTokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(15);
            securityTokenDescriptor.SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private SessionDto GetSessionDto (Account account, AccountSession accountSession)
        {
            return new SessionDto
            {
                SessionId = accountSession.SessionId,
                SessionToken = accountSession.SessionToken,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked
                }
            };
        }
    }
}
