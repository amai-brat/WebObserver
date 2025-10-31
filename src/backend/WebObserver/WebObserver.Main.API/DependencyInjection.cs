using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebObserver.Main.Application.Options;

namespace WebObserver.Main.API;

public static class DependencyInjection
{
    public static IServiceCollection AddSwaggerGenWithBearer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.UseOneOfForPolymorphism();
            options.UseAllOfForInheritance();
            options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Description = """
                              Authorization using JWT by adding header
                              Authorization: Bearer [token]
                              """,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    Array.Empty<string>()
                }
            });
        });

        return serviceCollection;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        JwtOptions jwtOptions)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = mrCtx =>
                    {
                        var path = mrCtx.Request.Path.HasValue ? mrCtx.Request.Path.Value : "";
                        var pathBase = mrCtx.Request.PathBase.HasValue ? mrCtx.Request.PathBase.Value : path;
                        var isFromHangFire = path.StartsWith("/hangfire") || pathBase.StartsWith("/hangfire");
                        
                        if (isFromHangFire)
                        {
                            if (mrCtx.Request.Query.ContainsKey("jwt"))
                            {
                                //If we find token add it to the response cookies
                                mrCtx.Token = mrCtx.Request.Query["jwt"];
                                mrCtx.HttpContext.Response.Cookies
                                    .Append("HangFireCookie",
                                        mrCtx.Token!,
                                        new CookieOptions
                                        {
                                            Expires = DateTime.Now.AddMinutes(10),
                                            SameSite = SameSiteMode.None,
                                            HttpOnly = true,
                                            Secure = true,
                                        });
                            }
                            else
                            {
                                //Check if we have a cookie from the previous request.
                                var cookies = mrCtx.Request.Cookies;
                                if (cookies.ContainsKey("HangFireCookie"))
                                    mrCtx.Token = cookies["HangFireCookie"];                
                            }
                        }
                        return Task.CompletedTask;
                    },
                };
            });
        
        return services;
    }
}