using Fba.Api.Exceptions;
using System.Net;
using Fba.Api.Service;
using Fba.Api.SharedObject;
using Marten;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddMarten(x => { x.Connection(connectionString); });

builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature != null)
        {
            if (contextFeature.Error is HbaBusinessException || contextFeature.Error is Exception)
            {
                await context.Response.WriteAsync(
                    new ReturnState<object>(result: null)
                    {
                        Status = HttpStatusCode.InternalServerError,
                        ErrorMessage = contextFeature.Error.Message

                    }.ToString());
            }
        }
    });
});
app.UseAuthorization();

app.MapControllers();

app.Run();

