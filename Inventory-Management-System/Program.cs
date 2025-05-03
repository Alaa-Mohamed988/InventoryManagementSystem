
using InventoryManagmentSystem.Domain.Interfaces;
using InventoryManagmentSystem.Infrastructure.Data;
using InventoryManagmentSystem.Infrastructure.Identity;
using InventoryManagmentSystem.Repository.Repositories;
using InventoryManagmentSystem.Service.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.OpenApi.Models;
using InventoryManagmentSystem.Service.CQRS.InventoryTransaction.Commands;
using InventoryManagmentSystem.Service.Interfaces;

namespace Inventory_Management_System
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });
        


            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Add Identity with custom user class
            //builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            // Add Identity with ApplicationUser
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            //Add Jwt settings

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme; // check using jwt token
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme; // redirect response in case not found cookie | token
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Iss"], // provider
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
           


            // Add services
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();           
            // Add MediatR service
            builder.Services.AddMediatR(opt =>
            opt.RegisterServicesFromAssembly(typeof(GetProductByIdQuery).Assembly));

            builder.Services.AddMediatR(opt =>
           opt.RegisterServicesFromAssembly(typeof(TransferStockCommand).Assembly));

            //Add AutoMapper service
            builder.Services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
            // Add Auth service
            builder.Services.AddAuthorization();
            // add Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
                );
            });



            #endregion

            var app = builder.Build();


            #region Configure the HTTP request pipeline

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "Admin", "Manager", "Viewer" };

                foreach (var role in roles)
                {
                    var roleExists = await roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers(); 
            #endregion

           await app.RunAsync();
            
        }
    }
}
