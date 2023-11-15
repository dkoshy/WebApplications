using App.DvdRental.Data.Models;

namespace App.DvdRental.Data.Extentions
{
    public static  class CommonUtility
    {

        public static void  WrappSuccessResult<T>(this QueryResult<T> Result
            , T? Data)
         
        {
            Result.IsSuccess = true;
            Result.Data = Data;
        }

        public static void WrappFailedResult<T>(this QueryResult<T> Result
           , Exception ex)

        {
            Result.IsSuccess = false;
            Result.Data = default;
            Result.Message = ex.Message;
        }
    }
}
