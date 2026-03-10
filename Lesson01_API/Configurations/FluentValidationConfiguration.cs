using FluentValidation;
using Lesson01_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lesson01_API.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            // Đăng ký tất cả các Validator có trong Assembly (tự động quét và nạp vào DI)
            services.AddValidatorsFromAssemblyContaining<Program>();

            // Can thiệp vào cách ASP.NET Core trả về lỗi Validation tự động
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    // 1. Trích xuất lỗi từ ModelState của hệ thống ra thành Dictionary<string, string[]>
                    var errors = context.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    // 2. Nhét vào cái phong bì APIResponse xịn xò của chúng ta
                    var response = APIResponse<object>.Builder()
                        .WithSuccess(false)
                        .WithStatusCode(System.Net.HttpStatusCode.BadRequest)
                        .WithErrors(errors)
                        .Build();

                    // 3. Trả về cho FE
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
