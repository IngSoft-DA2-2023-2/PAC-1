// using System.Net;
// using Microsoft.AspNetCore.Mvc;
//
// namespace PAC.Vidly.WebApi.Filters;
//
// public class ExceptionFilter_
// {
//     private readonly Dictionary<Type, IActionResult> _errors = new Dictionary<Type, IActionResult>
//     {
//         {
//             typeof(ArgumentNullException),
//             new ObjectResult(new
//                 {
//                     Code = "Code": string,
//                     Message = "Message": string,
//                     "DeveloperMessage": string
//                 })
//             {
//                 StatusCode = (int)HttpStatusCode.BadRequest
//             }
//         }
//     }; 
// }