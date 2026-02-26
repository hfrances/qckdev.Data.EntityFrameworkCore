# Compatibilidad de Frameworks

Este documento describe la compatibilidad de frameworks y las consideraciones de seguridad para `qckdev.Data.EntityFrameworkCore`.

## Frameworks Soportados

Este paquete soporta los siguientes frameworks de destino:

| Framework | Estado | Nivel de soporte | Versión de EF Core |
|-----------|--------|------------------|--------------------|
| .NET 10.0 |  Soportado | Actual  soporte hasta noviembre 2027 | 10.0.0 |
| .NET 8.0 |  Soportado | LTS  soporte hasta noviembre 2026 | 8.0.0 |
| .NET 6.0 |  Soportado | LTS  fin de soporte noviembre 2024 | 6.0.0 |
| .NET Standard 2.0 |  Soportado | Compatibilidad multiplataforma | 3.1.25 (EF Core 3.1) |

## Versiones de Paquetes

La librería utiliza distintas versiones de Entity Framework Core según el framework de destino:

| Framework | Paquete EF Core | Versión | Dependencias adicionales |
|-----------|-----------------|---------|--------------------------|
| netstandard2.0 | Microsoft.EntityFrameworkCore | 3.1.25 |  |
| net6.0 | Microsoft.EntityFrameworkCore | 6.0.0 | Microsoft.Extensions.Caching.Memory **6.0.2**  |
| net8.0 | Microsoft.EntityFrameworkCore | 8.0.0 | Microsoft.Extensions.Caching.Memory **8.0.1**  |
| net10.0 | Microsoft.EntityFrameworkCore | 10.0.0 |  |

## Consideraciones de Seguridad

### Mitigaciones de Vulnerabilidades

Este paquete referencia explícitamente versiones parcheadas de dependencias de caché para mitigar vulnerabilidades de seguridad conocidas:

#### CVE-2024-43483 (GHSA-qj66-m88j-hmgj)
- **Severidad**: Alta (CVSS 8.8)
- **Problema**: Vulnerabilidad de denegación de servicio (DoS) mediante ataques de *hash flooding* en mecanismos de caché
- **Componentes afectados**:
  - ``Microsoft.Extensions.Caching.Memory`` 6.0.0  6.0.1
  - ``Microsoft.Extensions.Caching.Memory`` 8.0.0
  - ``System.IO.Packaging``
  - ``System.Security.Cryptography.Cose``
- **Mitigación**:
  - Para .NET 6.0: referencia explícita a ``Microsoft.Extensions.Caching.Memory`` versión **6.0.2** (mínimo parcheado)
  - Para .NET 8.0: referencia explícita a ``Microsoft.Extensions.Caching.Memory`` versión **8.0.1** (mínimo parcheado)
  - .NET 10.0 ya incluye dependencias parcheadas por defecto
  - .NET Standard 2.0 (EF Core 3.1) no está afectado
- **Aviso**: https://github.com/advisories/GHSA-qj66-m88j-hmgj
- **Aviso Microsoft**: https://msrc.microsoft.com/update-guide/vulnerability/CVE-2024-43483

### Estrategia de Selección de Versiones

Las versiones de paquetes se seleccionaron aplicando el enfoque de **Mínima Versión Viable (MVV)**:
-  Usa la **versión mínima** necesaria para parchear la CVE-2024-43483
-  Mantiene compatibilidad con las versiones de Entity Framework Core de cada framework
-  Evita actualizaciones innecesarias que puedan introducir cambios disruptivos
-  Referencias explícitas a dependencias transitivas vulnerables
-  Auditorías de seguridad regulares con ``dotnet list package --vulnerable``

## Análisis de Versiones de Paquetes

Esta sección compara las versiones usadas en este proyecto con las últimas disponibles para cada framework, detallando qué se pierde por no actualizar.

> **Nota**: Cuando las mejoras aplican por igual a varios frameworks de la misma línea principal, se unifican en una sola sección.

---

### .NET Standard 2.0  EF Core 3.1.25  3.1.32

> .NET Core 3.1 alcanzó el **fin de vida (EOL) en diciembre de 2022**. La versión 3.1.32 es la última de esta rama.

| Paquete | Versión actual | Última disponible | Diferencia |
|---------|---------------|-------------------|------------|
| Microsoft.EntityFrameworkCore | 3.1.25 | **3.1.32** | 7 parches |

**Qué incluyen las versiones 3.1.26  3.1.32:**

- Correcciones de errores en la traducción de consultas LINQ (casos límite con ``GroupBy``, ``SelectMany``, ``Contains``)
- Mejoras de estabilidad en la lógica de reintento de resiliencia de conexión
- Correcciones en el mapeo de entidades propias (*owned entities*) en jerarquías de herencia complejas
- Correcciones en el proveedor SQLite para el manejo de fechas y horas
- Correcciones menores en el proveedor Cosmos
- No se incorporan nuevas funcionalidades (sólo parches para un framework EOL)
- No se corrigen vulnerabilidades de seguridad adicionales a las ya cubiertas por la 3.1.25

**Recomendación**:  El framework está en EOL. Prioriza la migración a .NET 6.0+ LTS. Actualizar a 3.1.32 aporta mejoras de estabilidad pero ningún beneficio de seguridad respecto a 3.1.25.

---

### .NET 6.0  EF Core 6.0.0  6.0.36

> .NET 6.0 alcanzó el **fin de vida (EOL) en noviembre de 2024**. La versión 6.0.36 es la última de esta rama.

| Paquete | Versión actual | Última disponible | Diferencia |
|---------|---------------|-------------------|------------|
| Microsoft.EntityFrameworkCore | 6.0.0 | **6.0.36** | 36 parches |

**Qué incluyen las versiones 6.0.1  6.0.36:**

#### Correcciones de Errores (acumuladas en 36 parches)
- **Traducción de consultas**: numerosas correcciones para consultas LINQ complejas, agregados con ``GroupBy``, ``Contains`` con subconsultas, ``Skip``/``Take`` con ordenación
- **Change tracker**: correcciones en el borrado en cascada, ``DetectChanges`` con entidades propias y la resolución de relaciones en grafos complejos
- **Migraciones**: correcciones en el orden de columnas, creación de índices sobre columnas calculadas y la tabla de historial de migraciones en esquemas no predeterminados
- **SQL en bruto**: correcciones en el manejo de parámetros de ``FromSqlRaw`` / ``FromSqlInterpolated`` en ciertos proveedores
- **Proxies de carga diferida**: correcciones en escenarios de eliminación y re-adjuntado de entidades
- **Tokens de concurrencia**: mejor manejo de columnas *row version* en los proveedores de SQL Server y PostgreSQL
- **Entidades propias**: múltiples correcciones en *table splitting*, mapeo de columnas JSON y propiedades sombra (*shadow properties*)
- **Modelos compilados**: correcciones de exactitud con configuraciones de modelos complejas

#### Mejoras de Rendimiento
- Reducción del coste de compilación de consultas para patrones repetidos
- Recorrido de grafo más eficiente en el *change tracker* para grafos grandes de entidades
- Construcción del modelo más rápida en modelos con muchos tipos de entidad
- Reducción de asignaciones de memoria en rutas críticas (materialización de consultas, *change tracking*)

#### Estabilidad
- Lógica de reintento de resiliencia de conexión mejorada ante fallos transitorios
- Mensajes de excepción más claros para relaciones y restricciones mal configuradas
- Correcciones de seguridad de hilos en patrones de uso concurrente de ``DbContext``

#### Sin Vulnerabilidades de Seguridad Adicionales Corregidas
- La referencia explícita a ``Microsoft.Extensions.Caching.Memory 6.0.2`` ya mitiga la CVE-2024-43483 independientemente de la versión de EF Core

**Recomendación**:  **Actualización recomendada** a 6.0.36 por las correcciones acumuladas y la mejora de estabilidad. El framework está en EOL  planifica la migración a .NET 8.0 LTS.

---

### .NET 8.0  EF Core 8.0.0  8.0.17

| Paquete | Versión actual | Última disponible | Diferencia |
|---------|---------------|-------------------|------------|
| Microsoft.EntityFrameworkCore | 8.0.0 | **8.0.17** | 17 parches |

**Qué incluyen las versiones 8.0.1  8.0.17:**

#### Correcciones de Errores
- **Columnas JSON**: correcciones para consultar propiedades JSON anidadas, traducción de ``JSON_VALUE``, colecciones dentro de documentos JSON
- **Colecciones primitivas** (novedad de EF 8): correcciones para ``Contains``, ``Any``, ``Count`` sobre columnas de colección primitiva
- **Tipos complejos** (novedad de EF 8): correcciones de mapeo para objetos de valor usados como tipos propios sin clave
- **HierarchyId** (SQL Server): casos límite en ordenación y filtrado
- **Traducción de consultas**: correcciones para operaciones con ``DateOnly``/``TimeOnly``, funciones ``Math``, traducción de métodos de cadena
- **Operaciones masivas** (``ExecuteUpdate`` / ``ExecuteDelete``): correcciones para expresiones que involucran entidades relacionadas y tipos propios
- **Consultas compiladas**: correcciones de exactitud al usar características nuevas de EF 8 (JSON, colecciones primitivas)
- **Migraciones**: correcciones para ``CreateTable`` con tipos complejos, conflictos en nombres de índices y casos límite en tablas temporales
- **Interceptores**: correcciones en la interacción de ``DbCommandInterceptor`` e ``ISaveChangesInterceptor`` con operaciones masivas
- **Índices referenciados**: corrección al reconfigurar una propiedad como navegación, que eliminaba índices de referencia incorrectamente (8.0.11)
- **Ejecución de lotes vacíos**: se evita ejecutar lotes SQL vacíos innecesariamente (8.0.11)
- **Recursión infinita**: corrección al identificar claves foráneas sombra en modelos complejos (8.0.11)
- **Caché de comandos relacionales**: los valores de parámetros ya no se almacenan en ``IMemoryCache``, evitando potenciales fugas de memoria (8.0.11)
- **LoadExtension en SQLite**: corrección para que funcione correctamente con ``dotnet run`` y librerías con prefijo ``lib*`` (8.0.15)
- **Funcletizador**: transformación de sobrecargas basadas en ``Span`` a ``Enumerable`` para mejor compatibilidad (8.0.15)

#### Mejoras de Rendimiento
- Traducción optimizada de consultas sobre columnas JSON para evitar llamadas redundantes a ``JSON_VALUE``
- Generación SQL más eficiente para comprobaciones ``Contains`` en colecciones primitivas
- Mejor generación de clave de caché de consultas, reduciendo falsos *cache miss*
- Menos ciclos de ida y vuelta en ``ExecuteUpdate`` para conjuntos de datos grandes

#### Estabilidad
- Mensajes de excepción más precisos para errores de configuración de mapeo JSON
- SQL generado más legible para facilitar la depuración
- Mejores eventos de diagnóstico para *logging* basado en interceptores
- Correcciones para casos límite en tablas temporales (SQL Server)

#### Sin Vulnerabilidades de Seguridad Adicionales Corregidas
- La referencia explícita a ``Microsoft.Extensions.Caching.Memory 8.0.1`` ya mitiga la CVE-2024-43483 independientemente de la versión de EF Core

**Recomendación**:  **Actualización recomendada** a 8.0.17, especialmente si se usan características de EF Core 8 como columnas JSON, colecciones primitivas u operaciones masivas.

---

### .NET 10.0  EF Core 10.0.0

| Paquete | Versión actual | Última disponible | Diferencia |
|---------|---------------|-------------------|------------|
| Microsoft.EntityFrameworkCore | 10.0.0 | **10.0.0** |  Al día |

**Recomendación**:  Ya se usa la última versión estable disponible. No se requiere ninguna acción.

---

## Recomendaciones de Estrategia de Actualización

### Prioridad 1  Crítica (Inmediata)
- **Ninguna**  Todos los frameworks utilizan versiones sin vulnerabilidades críticas sin parchear (CVE-2024-43483 ya está mitigada de forma explícita)

### Prioridad 2  Alta (En el próximo mes)
- **.NET 8.0**: Actualizar EF Core a **8.0.17**  correcciones en columnas JSON, tipos complejos y operaciones masivas (características nuevas de EF 8)

### Prioridad 3  Media (En los próximos 3 meses)
- **.NET 6.0**: Actualizar EF Core a **6.0.36**  36 parches acumulados de correcciones; planificar migración a .NET 8.0

### Prioridad 4  Baja (Cuando sea conveniente)
- **.NET Standard 2.0**: Actualizar EF Core a **3.1.32**  sólo mejoras menores de estabilidad; priorizar la migración sobre la actualización

### Ruta de Migración

Para aplicaciones que usen frameworks EOL:

1. **.NET Standard 2.0 (EF Core 3.1)**  Migrar a .NET 8.0 LTS (EF Core 8.0)
2. **.NET 6.0 (EF Core 6.0)**  Migrar a .NET 8.0 LTS o .NET 10.0

## Verificación

Para comprobar que no existen vulnerabilidades conocidas en este paquete:

```powershell
dotnet list package --vulnerable --include-transitive
```

Resultado esperado:
```
The given project has no vulnerable packages given the current sources.
```

## Guía de Migración

### De EF Core 3.1 (.NET Standard 2.0)
Si migras a un framework más moderno:

1. **A .NET 6.0**:
   - Actualiza ``TargetFramework`` del proyecto a ``net6.0``
   - EF Core 6.0 tiene algunos cambios disruptivos respecto a 3.1
   - Consulta los [cambios disruptivos de EF Core 6.0](https://docs.microsoft.com/ef/core/what-is-new/ef-core-6.0/breaking-changes)

2. **A .NET 8.0 (Recomendado)**:
   - Actualiza ``TargetFramework`` a ``net8.0``
   - Consulta los [cambios disruptivos de EF Core 8.0](https://docs.microsoft.com/ef/core/what-is-new/ef-core-8.0/breaking-changes)
   - Considera aprovechar las nuevas funcionalidades de EF Core 8.0

3. **A .NET 10.0**:
   - Actualiza ``TargetFramework`` a ``net10.0``
   - Últimas funcionalidades y mejor rendimiento
   - Consulta las notas de versión de EF Core 10.0

### Buenas Prácticas de Seguridad

1. **Mantén EF Core actualizado**: actualiza regularmente para recibir parches de seguridad
2. **Validación de entrada**: valida siempre los datos de usuario antes de usarlos en consultas
3. **Consultas parametrizadas**: usa consultas parametrizadas para prevenir inyección SQL
4. **Mínimo privilegio**: usa cuentas de base de datos con los permisos mínimos necesarios
5. **Cadenas de conexión**: almacénalas de forma segura (Azure Key Vault, secretos de usuario)
6. **Logging**: activa el registro de datos sensibles sólo en desarrollo

## Recursos Adicionales

- [Documentación de Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [Planificación de versiones de EF Core](https://docs.microsoft.com/ef/core/what-is-new/)
- [Repositorio GitHub de EF Core](https://github.com/dotnet/efcore)
- [Política de soporte de .NET](https://dotnet.microsoft.com/platform/support/policy)
- [Detalles de CVE-2024-43483](https://msrc.microsoft.com/update-guide/vulnerability/CVE-2024-43483)

## Última Actualización

Documento actualizado el: 25 de febrero de 2026

Para información más reciente, consulta el [repositorio en GitHub](https://github.com/hfrances/qckdev.Data.EntityFrameworkCore).
