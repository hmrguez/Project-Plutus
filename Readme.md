# Project-Plutus

This repository contains an F# ASP.NET Core Web API that retrieves prices on several cryptocurrencies using the Coingecko API. The project also includes some unit tests using the xUnit framework with Fluent Assertions.

## Roadmap

The purpose of this project is to learn about DevOps and create a full blown CI/CD pipeline for a .NET app and deploying the app to a Kubernetes node. The project roadmap is as follows:

1. Finish building the F# ASP.NET Core Web API with Coingecko API functionality.
2. Implement automated unit testing using the xUnit framework with Fluent Assertions.
3. Create a Jenkins pipeline to automate the process of building, testing, and deploying the app to a Kubernetes node.
4. Deploy the app to a Kubernetes node and test the deployment.

## Getting Started

To get started with this project, follow these steps:

1. Clone this repository to your local machine.
2. Navigate to the root directory of the cloned repository.
3. Restore the solution using your preferred IDE or the .NET CLI: `dotnet restore`.
4. Run the web API using your preferred IDE or the .NET CLI: `dotnet run`.
5. Navigate to `https://localhost:7083/api/crypto/{name}` to test the API. This endpoint retrieves the prices of several cryptocurrencies.

## Contributing

If you would like to contribute to this project, please follow these steps:

1. Fork this repository.
2. Create a new branch for your changes.
3. Make your changes and commit them.
4. Submit a pull request to this repository with a description of your changes.