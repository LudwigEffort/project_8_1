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
                //? exstract token from request header
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
                Console.WriteLine($"Header of Authorization: {token}");

                if (!string.IsNullOrEmpty(token))
                {
                    AttachUserToContext(context, labUserRepository, token); //? reacall method
                }

                await _next(context); //? pass the HTTP request to the next middleware (in this case to controller)
            }
            catch (AuthenticationException authEx)
            {
                context.Response.StatusCode = 401; //? not authorized error
                await context.Response.WriteAsync(authEx.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Error"); //? generic error
            }
        }

        //? verify if user exists and add user to HttpContext.Items
        private void AttachUserToContext(HttpContext context, ILabUserRepository labUserRepository, string token)
        {
            try
            {
                string email = ExtractEmailFromToken(token);

                if (!string.IsNullOrEmpty(email))
                {
                    var labUser = labUserRepository.GetLabUserByEmail(email); //? checks if the user exists in the db

                    if (labUser != null)
                    {
                        context.Items["User"] = labUser; //? add user to HttpContext
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

        //? exstract email from token
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
