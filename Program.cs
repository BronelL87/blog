using System.Text;
using blog.Context;
using blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<BlogServices>();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var secretKey = builder.Configuration["JWT:Key"];
var signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

// We're adding Auth To Your Build to check the JWToken from our Services.

builder.Services.AddAuthentication(options => {
    //This line of code will set the Authentification behaviour of our JWT Bearer
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //Sets the default behaviour for when our Auth Fails
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {

    // Configuring JWT Bearer Options (Checking The Params)
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //Check if the token's issuer is valid
        ValidateAudience = true, //Checks if the Token's audience is valid
        ValidateLifetime = true, //Ensures that our Token has not expired
        ValidateIssuerSigningKey = true, //Checking if the Token's signature is Valid
        
        ValidIssuer = "https://lazarblog-fhbpgqe3fna4g0db.westus-01.azurewebsites.net/",
        ValidAudience = "https://lazarblog-fhbpgqe3fna4g0db.westus-01.azurewebsites.net/",
        IssuerSigningKey = signingCredentials
    };

});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
