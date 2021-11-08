
namespace DTO.DTOs.Responses
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
