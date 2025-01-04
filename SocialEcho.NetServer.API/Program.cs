
using SocialEcho.NetServer.Domain.ModelBinders;
using SocialEcho.NetServer.Services;
using Tahyour.Base.Common.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBaseServices();
builder.Services.AddServices();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new SetDefaultAvatarAttribute());
    options.ModelBinderProviders.Insert(1, new SetRoleAttribute());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
