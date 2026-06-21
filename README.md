# Blazor.DynamicAdmin

A metadata-driven, extensible admin panel library for Blazor.

## Architecture
- **Abstractions**: Core contracts
- **Core**: Metadata, registry, configuration, in-memory data provider
- **Blazor**: Non-generic UI (`AutoTable`, `DynamicForm`, `DynamicAdminRouter`)
- Providers: EF Core, MudBlazor, etc. (planned)

## Packages

| Package | Status |
|---------|--------|
| `Blazor.DynamicAdmin.Abstractions` | ✅ |
| `Blazor.DynamicAdmin.Core` | ✅ |
| `Blazor.DynamicAdmin.Blazor` | ✅ MVP |
| `Blazor.DynamicAdmin.MudBlazor` | 🔜 |
| `Blazor.DynamicAdmin.EntityFrameworkCore` | 🔜 |

## Quick start

```csharp
builder.Services.AddDynamicAdmin(options =>
{
    options.Resource<Product>("products", resource =>
    {
        resource.DisplayName("Product")
                .Route("products")
                .Group("Catalog");
    });
});

builder.Services.AddDynamicAdminBlazor();
```

```razor
<link href="_content/Blazor.DynamicAdmin.Blazor/dynamic-admin.css" rel="stylesheet" />

@page "/admin/{ResourceName?}/{Mode?}/{Key?}"
<DynamicAdminRouter BasePath="/admin"
                    ResourceName="@ResourceName"
                    Mode="@Mode"
                    Key="@Key" />
```

See the architecture document for details.