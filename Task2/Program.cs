
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Task2.GenericRepo;
using Task2.IRepo;
using Task2.Models;
using Task2.UnitRepo;

namespace Task2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string test = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<GenericRepo<Student>>();
            builder.Services.AddScoped<GenericRepo<Department>>();
            builder.Services.AddScoped<IGenericRepo<Student>, GenericRepo<Student>>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddDbContext<ITIContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddControllers().AddNewtonsoftJson(options =>
            //               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize);
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(test, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 string keyData = "hello from the other side a7aaaaaaaadsdsdsdd";
                 var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyData));
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = secretKey,
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                 };
             });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
   
            app.UseCors(test);


            app.MapControllers();

            app.Run();
        }
    }
}
