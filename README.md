<a href="https://www.nuget.org/packages/qckdev.Data.Dapper"><img src="https://img.shields.io/nuget/v/qckdev.Data.Dapper.svg" alt="NuGet Version"/></a>
<a href="https://sonarcloud.io/dashboard?id=qckdev.Data.EntityFrameworkCore"><img src="https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.EntityFrameworkCore&metric=alert_status" alt="Quality Gate"/></a>
<a href="https://sonarcloud.io/dashboard?id=qckdev.Data.EntityFrameworkCore"><img src="https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.EntityFrameworkCore&metric=coverage" alt="Code Coverage"/></a>
<a><img src="https://hfrances.visualstudio.com/Main/_apis/build/status/qckdev.Data.EntityFrameworkCore?branchName=main" alt="Azure Pipelines Status"/></a>

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
