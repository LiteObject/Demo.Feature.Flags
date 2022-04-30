/*************************************************************
 * https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core?tabs=core5x
 * https://docs.microsoft.com/en-us/azure/azure-app-configuration/howto-targetingfilter-aspnet-core
 *************************************************************/

using Demo.Feature.Flags;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.DocumentFilter<FeatureGateDocumentFilter>();
});

/*
 * By default, the feature manager retrieves feature flag configuration from the "FeatureManagement" 
 * section of the .NET Core configuration data. To use the default configuration location, call the 
 * AddFeatureManagement method of the IServiceCollection passed into the ConfigureServices method of 
 * the Startup class.
 * 
 * You can specify that feature management configuration should be retrieved from a different 
 * configuration section by calling Configuration.GetSection and passing in the name of the desired 
 * section. 
 */
builder.Services.AddFeatureManagement();
// .AddFeatureFilter<PercentageFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
