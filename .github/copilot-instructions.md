# Copilot Instructions

## Project Guidelines
- Para adicionar/remover migrations neste projeto, usar o comando completo com -Project, -StartupProject, -Context e -OutputDir: Add-Migration "MigrationName" -Project JobApp.Infrastructure -StartupProject JobApp.Api -Context JobAppDbContext -OutputDir Persistence\Migrations
- Sempre verificar a versão exata dos NuGet packages instalados ANTES de sugerir código. Não assumir que APIs obsoletas funcionam. No .NET 10 com Scalar.AspNetCore 2.12.47 e Microsoft.AspNetCore.OpenApi 10.0.3, o Bearer é detectado automaticamente - não precisa de transformadores manualmente.