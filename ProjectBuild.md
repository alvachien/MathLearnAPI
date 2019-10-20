
# Database operation
## Material
Refer to official [doc](https://docs.microsoft.com/en-us/ef/core/managing-schemas/)

## Use Database as SoT
EF provide tools: Scaffold-DbContext.
Command used Package Manager Console: 
```
Scaffold-DbContext 'Data Source=.\SQLExpress;Initial Catalog=acquiz;Integrated Security=True;' Microsoft.EntityFrameworkCore.SqlServer -Tables knowledge, questionbank, qbklink -ContextDir Data -OutputDir Models -Force
```

## Use Model (classes) as SoT
EF provide tools: Migration.

Command used PowerShell:
```
Add-Migration InitialCreate
```

After that, run command 'Update-Database'.

