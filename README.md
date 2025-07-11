
Процедура запуску програми MarketPricePlatform

Перед запуском необхідно встановити:
.NET SDK 8.0 або вище
Docker Desktop
Git


git clone https://github.com/boler226/MarketPricePlatform
cd MarketPricePlatform

В appsettings змініть налаштування бази данних на вашу актуальну і робочу
 "ConnectionStrings": {
   "PostgreSQLConnection": "Host=postgres;Port=5432;Database=MarketPrice;Username=username;Password=password"
 }


У консолі Package Manager Console в налаштуванні Defoult projects: встановіть MarketPrice.Infrastructure та введіть команду Update-Database


Переконайтесь, що Docker Desktop увімкнено
Запустіть додаток командою: docker-compose up --build


Це збере та запустить два контейнери:
PostgreSQL база даних
ASP.NET Core бекенд


Додаток буде доступний за адресою:
http://localhost:5000
