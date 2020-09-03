using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StorageService.API.Models
{
    public class ErrorDetails
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<ValidationFailure> Errors { get; set; }

        public override string ToString()
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(this, settings);
        }
    }
}