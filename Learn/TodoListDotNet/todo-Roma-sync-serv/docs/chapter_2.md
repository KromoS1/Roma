## Chapter 2. Project restructure. Dependency injection
### Motivation
Improve structure of project using SOLID approach.
### Take a look:
#### Dependency injection and services registration
Take a look at `DependencyConfiguration.ConfigureServices`.
At first we create `ServiceCollection` then we register all services we want to use in our application in `ConfigureServices` then we build `IServiceProvider` and after it we can request services with all dependencies built-in.
It's native approach of dependency injection in .NET Core.

More about it you can read [here (RU) (all 6 articles in chapter 3)](https://metanit.com/sharp/aspnet5/6.1.php)