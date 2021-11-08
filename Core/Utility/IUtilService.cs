
namespace Core.Utility
{
    public interface IUtilService
    {
        public int GetUserIdFromToken(string tokenString);
        public bool IsValidToken(string tokenString);
    }
}
