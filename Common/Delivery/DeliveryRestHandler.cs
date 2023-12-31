using System;
using System.Threading.Tasks;
using RestSharp;
using Task_TMajdan.Common.Driver;
using Task_TMajdan.Src.Exceptions;

namespace TMajdanQATestTask.Src.Delivery
{
    internal class DeliveryRestHandler
    {
        public static RestResponse ExecuteRequest(Func<Task<RestResponse>> request)
        {
            Task<RestResponse> response = request.Invoke();
            response.Wait(TimeSpan.FromMinutes(Timeouts.ShortTimeout));
            RestResponse result = response.Result;

            if (!result.IsSuccessful)
            {
                string errorMessage = $"Response returned with errors:" +
                    $"\n    url: {result.ResponseUri}" +
                    $"\n    status code: {result.StatusCode}" +
                    $"\n    message: {result.ErrorMessage}" +
                    $"\n    body: {result.Content}";
                throw new RestClientException(errorMessage);
            }
            return result;
        }
    }
}