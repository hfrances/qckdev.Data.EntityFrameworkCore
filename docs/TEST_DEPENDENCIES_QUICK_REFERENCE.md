# Referencia Rapida: Tests Multi-Framework

## Proyecto
`qckdev.Data.EntityFrameworkCore.Test`

## Checklist

1. No cambiar los target frameworks sin solicitud explicita.
2. Mantener bloque de testing sin `Condition` (mismas versiones para todos los frameworks).
3. Condicionar solo EF Core/Sqlite por framework.
4. Ejecutar `dotnet test` y `--list-tests`.

## Frameworks activos

`netcoreapp3.1;net5.0;net6.0;net8.0;net9.0;net10.0`

## Paquetes de testing

- `Microsoft.NET.Test.Sdk` `17.11.1`
- `MSTest.TestAdapter` `3.2.2`
- `MSTest.TestFramework` `3.2.2`
- `coverlet.msbuild` `6.0.0`
- `coverlet.collector` `6.0.0`

## Regla clave

Si las versiones de testing son iguales en todos los frameworks, no usar `ItemGroup Condition` para testing.
