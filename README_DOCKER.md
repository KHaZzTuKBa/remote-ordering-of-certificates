# Инструкция по запуску приложения с Docker

Это руководство поможет вам запустить все приложение (фронтенд, бэкенд и базу данных) с помощью Docker Compose.

## Требования

- Docker (версия 20.10 или выше)
- Docker Compose (версия 2.0 или выше)

## Быстрый старт

1. **Клонируйте репозиторий** (если еще не сделали)

2. **Запустите все сервисы одной командой:**
```bash
docker-compose up -d
```

Эта команда:
- Соберет образы для фронтенда и бэкенда
- Запустит PostgreSQL базу данных
- Запустит бэкенд API
- Запустит фронтенд на nginx

3. **Проверьте статус контейнеров:**
```bash
docker-compose ps
```

4. **Откройте приложение в браузере:**
   - Фронтенд: http://localhost:4200
   - Бэкенд API: http://localhost:5066
   - Swagger UI: http://localhost:5066/swagger (если включен)

## Остановка приложения

```bash
docker-compose down
```

Для остановки и удаления volumes (включая данные БД):
```bash
docker-compose down -v
```

## Пересборка образов

Если вы внесли изменения в код и нужно пересобрать образы:

```bash
docker-compose build
docker-compose up -d
```

Или одной командой:
```bash
docker-compose up -d --build
```

## Просмотр логов

Просмотр логов всех сервисов:
```bash
docker-compose logs -f
```

Логи конкретного сервиса:
```bash
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f postgres
```

## Структура сервисов

### PostgreSQL
- **Порт:** 5432
- **База данных:** db
- **Пользователь:** postgres
- **Пароль:** pass
- **Volume:** postgres_data (данные сохраняются между перезапусками)

### Backend (ASP.NET Core)
- **Порт:** 5066
- **Внутренний порт:** 80
- **Зависит от:** postgres (ждет готовности БД)

### Frontend (Angular + Nginx)
- **Порт:** 4200
- **Внутренний порт:** 80
- **Зависит от:** backend
- **Прокси:** API запросы проксируются через nginx к бэкенду

## Переменные окружения

Вы можете изменить настройки через переменные окружения в `docker-compose.yml`:

- `POSTGRES_DB` - имя базы данных
- `POSTGRES_USER` - пользователь PostgreSQL
- `POSTGRES_PASSWORD` - пароль PostgreSQL
- `ConnectionStrings__Default` - строка подключения к БД

## Миграции базы данных

Если нужно применить миграции Entity Framework:

```bash
docker-compose exec backend dotnet ef database update --project /src/Infrastructure --startup-project /src/WebAPI
```

Или войдите в контейнер:
```bash
docker-compose exec backend bash
```

## Устранение неполадок

### Проблемы с подключением к БД

Убедитесь, что PostgreSQL контейнер запущен и здоров:
```bash
docker-compose ps
docker-compose logs postgres
```

### Проблемы с CORS

CORS уже настроен в бэкенде для работы с фронтендом. Если возникают проблемы, проверьте настройки в `Back/WebAPI/Program.cs`.

### Очистка и пересборка

Если что-то пошло не так, можно полностью очистить и пересобрать:
```bash
docker-compose down -v
docker-compose build --no-cache
docker-compose up -d
```

## Разработка

Для разработки рекомендуется:
- Запускать только PostgreSQL через Docker: `docker-compose up postgres -d`
- Запускать фронтенд и бэкенд локально для горячей перезагрузки

Или используйте volumes для монтирования исходного кода (требует дополнительной настройки docker-compose.yml).

