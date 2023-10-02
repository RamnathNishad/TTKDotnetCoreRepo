namespace SecuringWebAPI.Models
{
    public interface ITokenRepository
    {
        Tokens Authenticate(Users users);
    }
}
