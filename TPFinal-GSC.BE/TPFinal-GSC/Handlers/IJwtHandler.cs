using TPFinal_GSC.Dto;

namespace TPFinal_GSC.Handlers
{
    public interface IJwtHandler
    {
        string GenerateToken(UserDto user, IEnumerable<string> roles);
    }
}
