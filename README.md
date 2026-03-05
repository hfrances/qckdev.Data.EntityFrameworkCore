[![NuGet Version](https://img.shields.io/nuget/v/qckdev.Data.EntityFrameworkCore.svg)](https://www.nuget.org/packages/qckdev.Data.EntityFrameworkCore)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.EntityFrameworkCore&metric=alert_status)](https://sonarcloud.io/dashboard?id=qckdev.Data.EntityFrameworkCore)
[![Code Coverage](https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.EntityFrameworkCore&metric=coverage)](https://sonarcloud.io/dashboard?id=qckdev.Data.EntityFrameworkCore)
![Azure Pipelines Status](https://hfrances.visualstudio.com/Main/_apis/build/status/qckdev.Data.EntityFrameworkCore?branchName=master)

# qckdev.Data.EntityFrameworkCore

Provides a default set of tools for EntityFramework.

```cs
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    sealed class Test
    {

        public Guid TestId { get; set; }
        public string Name { get; set; }
        public int Factor { get; set; }
		public string Spaced { get; set; }

    }
}

```


```cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace qckdev.Data.EntityFrameworkCore.Test.Configuration
{
    
    sealed class TestConfiguration : IEntityTypeConfiguration<Entities.Test>
    {
        public void Configure(EntityTypeBuilder<Entities.Test> builder)
        {
            builder.HasKey(x => x.TestId);
            builder.Property(x => x.Spaced).HasMaxLength(20).IsFixedLength().TrimEnd();
        }
    }
}

```

## 🤝 Contributing
Issues and pull requests are welcome! See the contribution guidelines (coming soon).

## 📜 License
This project is licensed under the terms of the [MIT License](LICENSE).