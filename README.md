# GamesApi – веб-сервер для списка любимых игр

**ASP.NET Core Web API** с хранением данных в памяти.  
Поддерживает полное CRUD‑управление коллекцией игр, а также дополнительные функции:  
-  отметка «избранное»  
-  защита от пустого названия при создании  

Проект выполнен в рамках **лабораторной работы №28**.

---

## Запуск

1. Перейдите в папку проекта:
   ```bash
   cd GamesApi
   ```

2. Запустите сервер:
    ```bash
    dotnet run
    ```

3. Сервер будет доступен по адресу:
http://localhost:5001 (порт может отличаться — смотрите вывод терминала).
## Таблица маршрутов (Endpoints)
| Метод   | Маршрут                   | Что делает                         | Успешный статус             | Статус ошибки                     |
|---------|---------------------------|------------------------------------|-----------------------------|-----------------------------------|
| GET     | `/api/games`              | Получить все игры                  | 200 OK                      | –                                 |
| GET     | `/api/games/{id}`         | Получить игру по id                | 200 OK                      | 404 Not Found                     |
| GET     | `/api/games/favourites`   | Получить только избранные игры     | 200 OK                      | –                                 |
| POST    | `/api/games`              | Добавить новую игру                | 201 Created                 | 400 Bad Request (пустое название) |
| PUT     | `/api/games/{id}`         | Полностью обновить игру            | 200 OK                      | 404 Not Found                     |
| DELETE  | `/api/games/{id}`         | Удалить игру                       | 204 No Content              | 404 Not Found                     |

## Примеры запросов с `curl`

> **Примечание:** замените `5001` на актуальный порт, указанный при запуске сервера.

### 1. Получить все игры
```bash
curl http://localhost:5001/api/games
```
### 2. Получить игру по ID (например, id=2)
```bash
curl http://localhost:5001/api/games/2
```
### 3. Добавить новую игру
```bash
curl -X POST http://localhost:5001/api/games \
  -H "Content-Type: application/json" \
  -d '{"title": "Hollow Knight", "genre": "Metroidvania", "releaseYear": 2017}'
```
### 4. Добавить игру с пометкой «избранное»
```bash
curl -X POST http://localhost:5001/api/games \
  -H "Content-Type: application/json" \
  -d '{"title": "Portal 2", "genre": "Puzzle", "releaseYear": 2011, "isFavourite": true}'
```
### 5. Получить только избранные игры
```bash
curl http://localhost:5001/api/games/favourites
```
### 6. Обновить игру (например, id=2)
```bash
curl -X PUT http://localhost:5001/api/games/2 \
  -H "Content-Type: application/json" \
  -d '{"title": "The Witcher 3", "genre": "RPG", "releaseYear": 2015}'
```
### 7. Удалить игру (например, id=1)
```bash
curl -X DELETE http://localhost:5001/api/games/1
```
### 8. Проверка ошибки 404 (несуществующий ID)
```bash
curl -i http://localhost:5001/api/games/9999
```
### 9. Проверка ошибки 400 – пустое название
```bash
curl -X POST http://localhost:5001/api/games \
  -H "Content-Type: application/json" \
  -d '{"title": "", "genre": "RPG", "releaseYear": 2020}'
➡️ Ожидаемый ответ: статус 400 Bad Request и сообщение "Название игры не может быть пустым".
```