﻿
namespace DTO.DTOs.Responses
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
