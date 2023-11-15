using App.DvdRental.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.DvdRental.API.Extention
{
    public static class Utility
    {
        public static ActionResult<APIResponseBase> AsResponse<T>(this T? data )
        {
            if ( data == null )
            {
                return  NotFound();
            }

           return OK(data);
        }

        #region Private Methods
        private static ActionResult<APIResponseBase> NotFound()
        {
            var response = new APIResponseBase()
            {
                IsSuccess = false,
                Message = "Failed",
            };

            return new NotFoundObjectResult(response);

        }
        private static ActionResult<APIResponseBase> OK<T>(this T data)
        {
            var responseType = typeof(APIResponse<>);
            var resposne = Activator.CreateInstance(responseType.MakeGenericType(data.GetType()));

            resposne?.GetType().GetProperty("IsSuccess")?.SetValue(resposne, true);
            resposne?.GetType().GetProperty("Message")?.SetValue(resposne, "Success");
            resposne?.GetType().GetProperty("Metadata")?.SetValue(resposne, null);
            resposne?.GetType().GetProperty("ResultSet")?.SetValue(resposne, data);

            return new OkObjectResult(resposne);
        }
        #endregion
    }
}
