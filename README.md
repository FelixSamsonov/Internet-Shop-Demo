Eng.
InternetShopAspNetCoreMvc

InternetShopAspNetCoreMvc is a demo online store built using ASP.NET Core MVC that demonstrates Clean Architecture principles. The project includes modules for data access, business logic, services, and the user interface.

Project Architecture

The project consists of four main components:

1. InternetShopAspNetCoreMvc.Data

This module handles data access and interaction with the database:

Implementation of the database context using Entity Framework Core.

Migrations for creating and updating the database schema.

2. InternetShopAspNetCoreMvc.Domain

Contains the core business logic and models:

Entities, including models for products, categories, and orders.

Interfaces for repositories and services.

3. InternetShopAspNetCoreMvc.Service

This module implements the business logic and request processing:

Implementation of services for processing orders, managing products, and other functions.

4. InternetShopAspNetCoreMvc.UI

Contains the user interface:

MVC Controllers: controllers to handle user requests.

Views: representations for displaying data to the user.

Models: models for transferring data between controllers and views.

Features

Product Viewing: users can browse available products by category.

Shopping Cart: adding products to the cart and viewing cart contents.

Order Placement: ability to place an order with the choice of delivery and payment methods.

System Requirements

.NET 6.0 SDK or later.

SQL Server for data storage.

Setup and Running Instructions

Clone the repository:

git clone https://github.com/FelixSamsonov/Internet-Shop-Demo.git
cd Internet-Shop-Demo


Restore dependencies:

dotnet restore


Configure the database connection string in the appsettings.json file:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InternetShopDb;Trusted_Connection=True;"
}


Run migrations to create the database:

dotnet ef database update --project InternetShopAspNetCoreMvc.Data


Run the application:

dotnet run --project InternetShopAspNetCoreMvc.UI


Open your browser and go to:

https://localhost:5001

Ukr.
InternetShopAspNetCoreMvc

InternetShopAspNetCoreMvc — це демонстраційний інтернет-магазин, розроблений з використанням ASP.NET Core MVC, який демонструє принципи Clean Architecture. Проєкт включає в себе модулі для доступу до даних, бізнес-логіки, сервісів та користувацького інтерфейсу.

Архітектура проєкту

Проєкт складається з чотирьох основних компонентів:

1. InternetShopAspNetCoreMvc.Data

Цей модуль відповідає за доступ до даних та взаємодію з базою даних:

Реалізація контексту бази даних за допомогою Entity Framework Core.

Міграції для створення та оновлення структури бази даних.

2. InternetShopAspNetCoreMvc.Domain

Містить основну бізнес-логіку та моделі:

Сутності, включаючи моделі для продуктів, категорій та замовлень.

Інтерфейси для репозиторіїв та сервісів.

3. InternetShopAspNetCoreMvc.Service

Цей модуль реалізує бізнес-логіку та обробку запитів:

Реалізація сервісів для обробки замовлень, управління продуктами та іншими функціями.

4. InternetShopAspNetCoreMvc.UI

Містить інтерфейс користувача:

MVC Controllers: контролери для обробки запитів користувачів.

Views: представлення для відображення даних користувачу.

Models: моделі для передачі даних між контролерами та представленнями.

Функціональність

Перегляд продуктів: користувачі можуть переглядати доступні продукти за категоріями.

Кошик: додавання продуктів до кошика та перегляд вмісту кошика.

Оформлення замовлення: можливість оформлення замовлення з вибором способу доставки та оплати.

Системні вимоги

.NET 6.0 SDK або новіша версія.

SQL Server для зберігання даних.

Інструкція з налаштування та запуску

Клонуйте репозиторій:

git clone https://github.com/FelixSamsonov/Internet-Shop-Demo.git
cd Internet-Shop-Demo


Відновіть залежності:

dotnet restore


Налаштуйте рядок підключення до бази даних у файлі appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InternetShopDb;Trusted_Connection=True;"
}


Виконайте міграції для створення бази даних:

dotnet ef database update --project InternetShopAspNetCoreMvc.Data


Запустіть додаток:

dotnet run --project InternetShopAspNetCoreMvc.UI


Відкрийте браузер і перейдіть за адресою:

https://localhost:5001