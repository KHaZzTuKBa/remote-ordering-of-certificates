# Настройка CORS для бэкенда

Если при работе с фронтендом возникают ошибки CORS, необходимо настроить CORS в бэкенде.

## Инструкция для ASP.NET Core

Добавьте в `Back/WebAPI/Program.cs` следующее:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials();
    });
});

// ... остальной код ...

app.UseCors("AllowAngular");
```

Разместите `app.UseCors("AllowAngular");` после `app.UseHttpsRedirection();` и перед `app.UseAuthorization();`.

