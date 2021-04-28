# Cache Sample

This project is POC (Prove of Concept) for implement cache storage on repositories. The focus is on how to implement a solution for local cache during development and use a Redis for cache on the production environment.

## Requirements

- SQL Server initialized;
- .NET 5.0.2xx version

## Explanation

- There is a generic interface called [ICachedRepository.cs](./Repositories/ICachedRepository.cs) that is responsible for defining the main methods for the service, avoiding a specific technology.
- The default implementation to get data from cache or set it there is defined on [CachedRepository.cs](./Repositories/CachedRepository.cs). To use the methods on your repository, you must implement the interface mentioned previously and inherit this class.
- On [Startup.cs](./Startup.cs) is defined the Dependency Injection with the logic to define when to use local or Redis cache.
- You can see a sample code on [CustomerController.cs](./Controllers/CustomerController.cs) using the repository that implemented cache solution.
- I didn't implement Redis logic because the main goal of this POC is to show how to organize the classes and interfaces and see it working with a basic code.
