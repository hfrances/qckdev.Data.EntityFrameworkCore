# Actualizacion de Dependencias de Tests Unitarios

## Proyecto
`qckdev.Data.EntityFrameworkCore.Test`

## Estado actual (fuente de verdad)

- Target frameworks: `netcoreapp3.1;net5.0;net6.0;net8.0;net9.0;net10.0`
- Test SDK/MSTest/Coverlet: mismas versiones para todos los frameworks
- Dependencias EF Core y Sqlite: versionadas por framework

## Regla de implementacion para este proyecto

1. Mantener `TargetFrameworks` exactamente como estan, salvo solicitud explicita.
2. Como el stack de testing usa mismas versiones en todos los frameworks, usar un unico `ItemGroup` sin `Condition` para:
   - `Microsoft.NET.Test.Sdk`
   - `MSTest.TestAdapter`
   - `MSTest.TestFramework`
   - `coverlet.msbuild`
   - `coverlet.collector`
3. Condicionar solo paquetes runtime (EF Core / Sqlite) por framework.

## Matriz de versiones

### Testing (todos los frameworks)

- `Microsoft.NET.Test.Sdk`: `17.11.1`
- `MSTest.TestAdapter`: `3.2.2`
- `MSTest.TestFramework`: `3.2.2`
- `coverlet.msbuild`: `6.0.0`
- `coverlet.collector`: `6.0.0`

### Runtime por framework

- `netcoreapp3.1`, `net5.0`:
  - `Microsoft.Data.Sqlite`: `3.1.32`
  - `Microsoft.EntityFrameworkCore`: `3.1.32`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `3.1.32`
- `net6.0`:
  - `Microsoft.Data.Sqlite`: `6.0.36`
  - `Microsoft.EntityFrameworkCore`: `6.0.36`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `6.0.36`
- `net8.0`:
  - `Microsoft.Data.Sqlite`: `8.0.11`
  - `Microsoft.EntityFrameworkCore`: `8.0.11`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `8.0.11`
- `net9.0`:
  - `Microsoft.Data.Sqlite`: `9.0.0`
  - `Microsoft.EntityFrameworkCore`: `9.0.0`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `9.0.0`
- `net10.0`:
  - `Microsoft.Data.Sqlite`: `10.0.0`
  - `Microsoft.EntityFrameworkCore`: `10.0.0`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `10.0.0`

## Verificacion

```powershell
dotnet test qckdev.Data.EntityFrameworkCore.Test\qckdev.Data.EntityFrameworkCore.Test.csproj
dotnet test qckdev.Data.EntityFrameworkCore.Test\qckdev.Data.EntityFrameworkCore.Test.csproj --list-tests
```
