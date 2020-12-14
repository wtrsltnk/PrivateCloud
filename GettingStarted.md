Start with adding the initial create migrations

	add-migration -Project PrivateCloud.Infra.Sqlite -StartupProject PrivateCloud.Api -OutputDir Migrations\SqliteMigrations InitialCreate

Then create the litedb file

	update-database