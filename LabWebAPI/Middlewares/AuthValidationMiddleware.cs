using LabWebAPI.Interfaces;
using System.Security.Authentication;

namespace LabWebAPI.Middlewares
{
    public class AuthValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILabUserRepository labUserRepository)
        {
            try
            {
                //? token: ludwig@gmail.com-PJTDO5QE5O
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
                Console.WriteLine($"Header of Authorization: {token}");

                if (token != null)
                {
                    AttachUserToContext(context, labUserRepository, token);
                }

                await _next(context);
            }
            catch (AuthenticationException authEx)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(authEx.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Error");
            }
        }

        private void AttachUserToContext(HttpContext context, ILabUserRepository labUserRepository, string token)
        {
            try
            {
                string email = ExtractEmailFromToken(token);

                if (!string.IsNullOrEmpty(email))
                {
                    var labUser = labUserRepository.GetLabUserByEmail(email);

                    if (labUser != null)
                    {
                        context.Items["User"] = labUser;
                    }
                    else
                    {
                        throw new AuthenticationException("User not found.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ExtractEmailFromToken(string token)
        {
            var chunks = token.Split('-');

            if (chunks.Length > 0)
            {
                return chunks[0];
            }

            return null;
        }
    }
}
