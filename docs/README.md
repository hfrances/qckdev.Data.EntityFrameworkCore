# Documentation

Welcome to the documentation for `qckdev.Data.EntityFrameworkCore`.

## Table of Contents

- [Framework Compatibility](COMPATIBILITY.md) - Supported frameworks, package versions, and security considerations
- [Test Dependencies Update](TEST_DEPENDENCIES_UPDATE.md) - Guidance for maintaining `qckdev.Data.EntityFrameworkCore.Test.csproj`
- [Test Dependencies Quick Reference](TEST_DEPENDENCIES_QUICK_REFERENCE.md) - Short checklist for test project updates

## Quick Links

- [Main README](../README.md) - Project overview and usage examples
- [NuGet Package](https://www.nuget.org/packages/qckdev.Data.EntityFrameworkCore)
- [GitHub Repository](https://github.com/hfrances/qckdev.Data.EntityFrameworkCore)

## Documentation Contents

### [Framework Compatibility](COMPATIBILITY.md)
Detailed information about:
- Supported .NET frameworks and their status
- Entity Framework Core version mapping
- CVE-2024-43483 vulnerability mitigation (hash flooding DoS)
- Security best practices for EF Core
- Migration guides from older frameworks
- Performance considerations per framework

### [Test Dependencies Update](TEST_DEPENDENCIES_UPDATE.md)
Detailed guide for:
- Multi-target test configuration
- Package version policy (MSTest/Coverlet)
- Framework-specific EF Core/Sqlite dependencies
- Validation commands (`dotnet test`, `--list-tests`)

### [Test Dependencies Quick Reference](TEST_DEPENDENCIES_QUICK_REFERENCE.md)
Short operational checklist to:
- Avoid accidental framework changes
- Keep test package versions consistent
- Apply conditional runtime packages only where needed

## About This Package

`qckdev.Data.EntityFrameworkCore` provides a default set of tools and extension methods for Entity Framework Core, making it easier to work with EF Core across different .NET versions.

## License

This project is licensed under the terms specified in the [LICENSE](../LICENSE) file.
