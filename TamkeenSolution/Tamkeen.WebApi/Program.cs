using Microsoft.Extensions.DependencyInjection;
using Tamkeen.Persistence;
using MediatR;
using System.Reflection;
using Tamkeen.Application.Features.Trainees.Queries;
using Tamkeen.Application.Models.MappingProfile.TraineeMapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// استدعاء ميثود التسجيل من طبقة Persistence
builder.Services.AddPersistenceServices(builder.Configuration);

// ✅ تسجيل AutoMapper بشكل صحيح
builder.Services.AddAutoMapper(cfg =>
{
    // يمكن إضافة تكوينات إضافية هنا إذا لزم الأمر
}, typeof(TraineeProfile).Assembly);

// ✅ تسجيل MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetTraineesListQuery).Assembly);
});

// إعدادات CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7080",
                "https://localhost:5001",
                "https://localhost:7001",
                "https://localhost:7140",
                "http://localhost:5000",
                "http://localhost:5250",
                "http://localhost:5236"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // إضافة صفحة اختبار Swagger UI اختيارياً
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorOrigin");
app.UseAuthorization();
app.MapControllers();

app.Run();