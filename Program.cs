using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Api versioning
builder.Services.AddApiVersioning(config =>
{
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.ReportApiVersions = true;
    //config.ApiVersionReader = new HeaderApiVersionReader("api-version");
    config.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("api-version"),
        new QueryStringApiVersionReader("version"),
        new MediaTypeApiVersionReader("v"));
});

//builder.Services.AddVersionedApiExplorer(config =>
//{
//    config.GroupNameFormat = "'v'VVV";
//    config.SubstituteApiVersionInUrl = true;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen( c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

