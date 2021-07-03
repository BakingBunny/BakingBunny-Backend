# Baking Bunny Backend

This repository shows the backend (.NET Core 5 Web API) for Baking Bunny Project.

## How-to's

### Update DB connection string

Open 'appsettings.json' and update DB connection parameters accordingly.

```
"server=localhost;user id=root;password=bakingbunny;port=3306;database=bakingbunny;"
```

### API version documentation

Under Startup.cs, edit the following line:

```
c.SwaggerDoc("v1", new OpenApiInfo { Title = "Baking Bunny API", Version = "v1" });
```