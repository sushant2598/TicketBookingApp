namespace UserService.Service
{
    public interface ITokenGeneratorService
    {
        string GenerateJWTToken(string userid, string role);
    }
}