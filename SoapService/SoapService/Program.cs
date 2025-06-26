using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using SoapService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();

var metadata = app.Services.GetRequiredService<ServiceMetadataBehavior>();
metadata.HttpGetEnabled = true;

app.UseServiceModel(builder =>
{
    builder.AddService<LongCreationTimeService>(options =>
    {
        options.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });
    builder.AddServiceEndpoint<LongCreationTimeService, ILongCreationTimeService>(
        new BasicHttpBinding(), "/LCTService.svc");
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
