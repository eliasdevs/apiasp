// 1. Usings to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using universityApiBackend;
using universityApiBackend.DataAccess ;
using universityApiBackend.Models.DataModels;
using universityApiBackend.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// 2. Connection with SQL Server Express 
const string CONNECTIONNAME = "UniversityDB";
var connectionString= builder.Configuration.GetConnectionString(CONNECTIONNAME);

// 3. Add Context 
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// 7. Add services of jwt autorization 
builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// 4.Add Custom Services(Folder Services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
//TODO: Add th rest of services
//8. Add Authorization 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy =>policy.RequireClaim("UserOnly","User1"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//TODO 8. confing swagger to take care of Autorization of JWT
builder.Services.AddSwaggerGen(options =>
    {
    //we define the security for authorization
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using Bearer Scheme"

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer",
            }
        },new string[]{}
        }
    });
    }
);
//5. CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//6. Tell app to use CORS
app.UseCors("CorsPolicy");

app.Run();
