using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Shared.Utilities.Response
{
    public class CustomResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string>? Errors { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }

        public static CustomResponse<T> Success(T data, int statusCode)
        {
            return new CustomResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }
        //başarılı ama data olmama durumu 
        public static CustomResponse<T> Success(int statusCode)
        {
            return new CustomResponse<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }
        //fail durumu birden çok hata
        public static CustomResponse<T> Fail(List<string> errors, int statusCode)
        {
            return new CustomResponse<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
        //fail durumu tek hata
        public static CustomResponse<T> Fail(string error, int statusCode)
        {
            return new CustomResponse<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
