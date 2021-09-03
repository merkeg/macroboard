using Macroboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Macroboard.Extensions
{
    /// <summary>
    /// Controller extensions class
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Construct an error message model object
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static BaseResponse<MessageResponse> ConstructErrorResponse(this Controller controller, string message)
        {
            return ConstructResponse(controller, StatusType.error, new MessageResponse()
            {
                Message = message
            });
        }
        
        /// <summary>
        /// Construct a response
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static BaseResponse<T> ConstructSuccessResponse<T>(this Controller controller, T content)
        {
            return new()
            {
                Status = StatusType.ok,
                Result = content
            };
        }

        /// <summary>
        /// Construct a response
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="statusType"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static BaseResponse<T> ConstructResponse<T>(this Controller controller, StatusType statusType, T content)
        {
            return new()
            {
                Status = statusType,
                Result = content
            };
        }
        
        
        
        
    }
}