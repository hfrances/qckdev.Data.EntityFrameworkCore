# Documentation

Welcome to the documentation for `qckdev.Data.EntityFrameworkCore`.

## Table of Contents

- [Framework Compatibility](COMPATIBILITY.md) - Supported frameworks, package versions, and security considerations

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

## About This Package

`qckdev.Data.EntityFrameworkCore` provides a default set of tools and extension methods for Entity Framework Core, making it easier to work with EF Core across different .NET versions.

## Key Features

✨ **Multi-Framework Support**: Works on .NET Standard 2.0, .NET 6.0, .NET 8.0, and .NET 10.0  
🔒 **Security Hardened**: Mitigated CVE-2024-43483 (High severity)  
🎯 **Simplified Configuration**: Extension methods for common EF Core patterns  
⚡ **Version-Appropriate**: Uses the right EF Core version for each framework  
📦 **Minimal Dependencies**: Clean dependency tree

## Security Notice

⚠️ **Important**: This package includes explicit security patches for .NET 6.0 and .NET 8.0 to mitigate CVE-2024-43483, a high-severity denial of service vulnerability in caching mechanisms.

## Related Packages

- **qckdev.Data**: Core data utilities (no EF Core dependency)
- **qckdev.Data.EntityFrameworkCore.Merger**: Entity merging utilities for EF Core
- **qckdev.Data.Dapper**: Dapper extensions and utilities

## Contributing

If you find any issues or have suggestions for improving the documentation, please open an issue or submit a pull request on [GitHub](https://github.com/hfrances/qckdev.Data.EntityFrameworkCore/issues).

## License

This project is licensed under the terms specified in the [LICENSE](../LICENSE) file.

---

Last updated: February 25, 2026
