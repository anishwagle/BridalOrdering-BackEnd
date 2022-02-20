using Microsoft.AspNetCore.Mvc;
using System.Net;
using BridalOrdering.Models;

namespace BridalOrdering.Controllers
{
    public class BaseApiController: ControllerBase
    {
        public ServiceResult<T> CreateSuccessResponse<T> (T result)
        {
            var response = new ServiceResult<T>
            {
                StatusCode = (int)HttpStatusCode.OK,
                IsSuccess = true,
                Message = "Success",
                Result = result
            };
            return response;
        }
        public ServiceResult<T> CreateSuccessResponse<T> (T result, string message)
        {
            var response = new ServiceResult<T>
            {
                StatusCode = (int)HttpStatusCode.OK,
                IsSuccess = true,
                Message = message,
                Result = result
            };
            return response;
        }
        public ServiceResult<T> CreateErrorResponse<T> (T result)
        {
            var response = new ServiceResult<T>
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                IsSuccess = false,
                IsError = true,
                Message = "Error",
                Result = result
            };
            return response;
        }
        public ServiceResult<T> CreateErrorResponse<T> (T result, HttpStatusCode statusCode)
        {
            var response = new ServiceResult<T>
            {
                StatusCode = (int)statusCode,
                IsSuccess = false,
                IsError = true,
                Message = "Error",
                Result = result
            };
            return response;
        }
        public ServiceResult<T> CreateErrorResponse<T> (T result, string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            var response = new ServiceResult<T>
            {
                StatusCode = (int)statusCode,
                IsSuccess = false,
                IsError = true,
                Message = message,
                Result = result
            };
            return response;
        }
    }
}
