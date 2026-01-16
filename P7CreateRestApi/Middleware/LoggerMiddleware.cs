using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace P7CreateRestApi.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Vérifie si l’utilisateur est authentifié
            // Si oui → récupère son nom (via JWT / Identity)
            // Sinon → "Anonymous"
            var user = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                ?? "Anonymous";


            // Récupère la methode appellée (GET, POST, PUT, DELETE…)
            var method = context.Request.Method;

            // Récupère la route: /api/users, /api/bidlist, etc.
            var path = context.Request.Path;

            await _next(context);

            // Le log est construit après l’exécution de l’endpoint avec l'utilisateur, méthode HTTP,
            // l'endpoint,code de réponse(200, 201, 401, 403, etc.), date.
            _logger.LogInformation(
                "User: {User} | {Method} {Path} | Status: {StatusCode} | Date: {Date}",
                user,
                method,
                path,
                context.Response.StatusCode,
                DateTime.UtcNow);
        }
    }

}
