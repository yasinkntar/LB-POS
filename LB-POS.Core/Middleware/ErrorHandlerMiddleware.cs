using FluentValidation;
using LB_POS.Core.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace LB_POS.Core.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                bool isApiRequest = context.Request.Path.StartsWithSegments("/api") ||
                           context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isApiRequest)
                {
                    await HandleApiError(context, error);
                }
                else
                {
                    // في حالة الويب (Razor Pages) نقوم بالتوجيه لصفحة خطأ جميلة
                    HandleWebError(context, error);
                }
            }
        }
        private void HandleWebError(HttpContext context, Exception error)
        {
            // يمكنك تخزين الرسالة في TempData أو تمريرها عبر الـ QueryString
            // يفضل استخدام صفحة خطأ مخصصة مثل /Error
            context.Response.Redirect($"/Error?message={Uri.EscapeDataString(error.Message)}");
        }

        private async Task HandleApiError(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // منطق الـ Switch الخاص بك هنا (نفس الكود القديم)
            var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

            // ... (logic switch) ...
            switch (error)
            {
                case UnauthorizedAccessException e:
                    // custom application error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case ValidationException e:
                    // custom validation error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    responseModel.Message = error.Message; ;
                    responseModel.StatusCode = HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case DbUpdateException e:
                    // can't update error
                    responseModel.Message = e.Message;
                    responseModel.StatusCode = HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case Exception e:
                    if (e.GetType().ToString() == "ApiException")
                    {
                        responseModel.Message += e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "\n" + e.InnerException.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    }
                    responseModel.Message = "حدث خطأ داخلي. يرجى المحاولة لاحقاً.";
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                default:
                    // unhandled error
                    responseModel.Message = error.Message;
                    responseModel.StatusCode = HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };

            var result = JsonSerializer.Serialize(responseModel, options);
            await response.WriteAsync(result);
        }
    }

}
