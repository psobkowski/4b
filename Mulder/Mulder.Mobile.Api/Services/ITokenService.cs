namespace Mulder.Mobile.Api.Services
{
    public interface ITokenService
    {
        string GenerateToken(string requestSource);
        bool IsRequestSourceValid(string requestSource);
    }
}