using DevAssignment.CommonLayer.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace DevAssignment.CommonLayer.Core.ResponseModels
{
    public class Response : ObjectResult
    {
        public Response(string message) : base(new ResultData(message, HttpStatusCode.OK))
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public Response(HttpStatusCode httpStatusCode, string message) : base(new ResultData(message, httpStatusCode))
        {
            StatusCode = (int)httpStatusCode;
        }

        public Response(HttpStatusCode httpStatusCode,List<ValidationFailureDto> validationFailures) : base(new ResultData(httpStatusCode , validationFailures))
        {
            StatusCode = (int)httpStatusCode;
        }
    }

    public class Response<T> : ObjectResult
    {
        public string Message { get; set; }
        public Response(string message) : base(new ResultData<T>(message, HttpStatusCode.OK))
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public Response(T data) : base(new ResultData<T>(data))
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public Response(T data, string message, ResponseCode code) : base(new ResultData<T>(data, message, code))
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public Response(HttpStatusCode httpStatusCode, string message) : base(new ResultData<T>(message, httpStatusCode))
        {
            Message = message;
            StatusCode = (int)httpStatusCode;
        }

        public Response(HttpStatusCode httpStatusCode, T data, ResponseCode code) : base(new ResultData<T>(data))
        {
            StatusCode = (int)httpStatusCode;
        }

        public Response(HttpStatusCode httpStatusCode, T data, string message, ResponseCode code) : base(new ResultData<T>(data, message, code))
        {
            StatusCode = (int)httpStatusCode;
        }
    }

    public class ResultData
    {
        public ResponseCode Code { get; set; }
        public string Message { get; set; }
        public IList<ValidationFailureDto> Errors { get; set; }

        public ResultData()
        {
        }
        public ResultData(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            Code = httpStatusCode == HttpStatusCode.BadRequest ? ResponseCode.ErrorMessage : ResponseCode.Message;
        }
        public ResultData(HttpStatusCode httpStatusCode ,IList<ValidationFailureDto> errors)
        {
            Errors = errors;
            Code = ResponseCode.Errors;
        }
    }

    public class ResultData<T> : ResultData
    {
        public T Data { get; set; }

        public ResultData(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            Code = (int)httpStatusCode > 299 ? ResponseCode.ErrorMessage : ResponseCode.Message;
        }
        public ResultData(T data)
        {
            Data = data;
            Code = ResponseCode.Data;
        }
        public ResultData(T data, string message, ResponseCode code)
        {
            Data = data;
            Message = message;
            Code = code;
        }
    }

    public enum ResponseCode
    {
        ErrorMessage = 98,
        Errors = 100,
        Message = 101,
        Data = 102
    }
}
