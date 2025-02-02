# ambev-test-project
ambev-test-project


Runs this command to add the Migration from within the database folder.
```bash
dotnet ef migrations add Initial --startup-project ../../Consumers/Api
```

Runs this command to update the database 
```bash
dotnet ef database update Initial --startup-project ../../Consumers/Api
```