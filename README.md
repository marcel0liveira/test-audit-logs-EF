# test-audit-logs-EF
Project is testing to autit-logs in EF core

# Referencia:
 - [Post do Projeto original](https://blog.elmah.io/implementing-audit-logs-in-ef-core-without-polluting-your-entities/)

# Info:
> Como este repo é apenas de teste e aprendizado o .env esta adicionado e os valores de appsettings não foram omitidos, 
> mas em um projeto real esses dados devem ser confidenciais e não podem ser expostos.

# Executar o projeto:
 - na raiz da soluçãp, execute o comando:
 ```bash
   docker compose up -d
 ```


 


## AuditLog Implementation
- O plano para AuditLog seria usar uma interface que seria implantada junto com a BaseEntity nas models desejados.

  - Interface:
    ```csharp
        public interface IAuditableEntity
        {
            public DateTimeOffset CreatedAtUtc { get; set; }
            public DateTimeOffset? UpdatedAtUtc { get; set; }
    
            public string CreatedBy { get; set; }
            public string? UpdatedBy { get; set; }
        }
    ```
  - Model de Exemplo (apenas inclui a interface):
    ```csharp
        public class User : BaseEntity, IAuditableEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
            public DateTimeOffset? UpdatedAtUtc { get; set; }
            public string CreatedBy { get; set; } = null!;
            public string? UpdatedBy { get; set; }
            public virtual ICollection<Applicant> Applicants { get; set; }
            public virtual ICollection<Company> Companies { get; set; }
        }
    ```
 - Ajustar o DBContext
   - [DBContext](./JobApp.Infrastructure/Persistences/JobAppDbContext.cs)
 
 - Configuration do AuditLog
   - [AuditLogConfiguration](./JobApp.Infrastructure/Persistences/Auditing/AuditTrailConfiguration.cs)
   
 - Provider de Auditoria da sessão do usuário
   - [AuditingProvider](./JobApp.Infrastructure/Provider/ICurrentSessionProvider.cs)

### Helps 
- EF migration commands
```bash
   Add-Migration "InitialCreated" -Project JobApp.Infrastructure -StartupProject JobApp.Api -Context JobAppDbContext -OutputDir Persistences\Migrations
```