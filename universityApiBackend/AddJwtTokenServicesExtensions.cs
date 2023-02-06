﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using universityApiBackend.Models.Jwt;

namespace universityApiBackend
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            //Add JWT Settings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

            //Add Singleton of JWT Settings
            Services.AddSingleton(bindJwtSettings);
            Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata= false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey=bindJwtSettings.ValidateIssuerSiningKey,
                        IssuerSigningKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSiningKey)),
                        ValidateIssuer=bindJwtSettings.ValidateIssuer,
                        ValidIssuer=bindJwtSettings.ValidIssuer,
                        ValidateAudience=bindJwtSettings.ValidateAudience,
                        ValidAudience=bindJwtSettings.ValidAudience,
                        RequireExpirationTime=bindJwtSettings.RequireExpirationTime,
                        ValidateLifetime=bindJwtSettings.ValidateLifeTime,
                        ClockSkew= TimeSpan.FromDays(1)

                    };
                });
        }
    }
}
