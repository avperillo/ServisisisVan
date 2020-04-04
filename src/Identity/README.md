Migrations

Add-Migration InitialMigration -Context IdentityContext  -OutputDir Data/Migrations
Add-Migration InitialMigration -Context ConfigurationDbContext  -OutputDir Data/Migrations/ConfigurationDb
Add-Migration InitialMigration -Context PersistedGrantDbContext -OutputDir Data/Migrations/PersistedGrantDb
