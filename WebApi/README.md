# Baking Bunny Backend

This repository shows the backend (.NET Core 5 Web API) for Baking Bunny Project.

## How-to's

### Update DB connection string

Modify 'appsettings.json' and update DB connection parameters accordingly.

```
"server=<em>IP address</em>;user id=<em>userId</em>;password=<em>password</em>;port=3306;database=<em>databasename</em>;"
```

### API version documentation

Under Startup.cs, edit the following line:

```
c.SwaggerDoc("v1", new OpenApiInfo { Title = "Baking Bunny API", Version = "v1" });
```