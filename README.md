# LifetimeToolManage

# LifetimeToolManage

## Opis
Aplikacja desktopowa Windows (C# WPF) do śledzenia przebiegu narzędzia.  
Dane mają być odbierane z zewnętrznego źródła po TCP i zapisywane w bazie SQLite.  
Moduł odbierania danych nie jest jeszcze ukończony.  
Zapisywanie do bazy danych działa.

## Funkcjonalności
- Dodawanie i edycja przebiegu narzędzia.
- Moduł serwisowy do testowania i sprawdzania przebiegu.
- Wyświetlanie aktywnego narzędzia i jego przebiegu.
- Baza danych SQLite.

## Wymagania
- .NET 6/7
- Visual Studio 2022 lub nowsze
- SQLite

## Uruchomienie
1. Sklonuj repozytorium.
2. Otwórz projekt w Visual Studio.
3. Wykonaj migracje bazy danych:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
  ```

