# dzCS — домашки по C#

1 репозиторий, каждая домашка — отдельная папка.

## Формат папок

```
MM.DD - Название/
```

Примеры:
- `10.14 - Massivy/`
- `10.15 - Cikly/`

## Как добавить новую домашку

```powershell
# 1. Создать консольный проект
dotnet new console -n Primer -o "10.21 - NovayaTema"

# 2. Добавить в solution
dotnet sln add "10.21 - NovayaTema/Primer.csproj"

# 3. Запустить
dotnet run --project "10.21 - NovayaTema"
```

## Структура внутри домашки

```
10.14 - Primer/
  Primer.csproj
  Program.cs
  README.md  # число, тема, задачи
```

Внутреннее имя проекта (`Primer.csproj`) — латиницей без пробелов,
имя папки — как договорились: `MM.DD - Название`.
