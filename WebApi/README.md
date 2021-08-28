# Baking Bunny Backend

This repository shows the backend (.NET Core 5 Web API) for Baking Bunny Project.

## Basic setup

### Update DB connection string

Add or modify 'appsettings.json' and update DB connection parameters under 'ConnectionStrings'. The ConnectionString 'prodBakingBunnyDB' is for production and 'localBakingBunnyDB' is for local test.

```
"ConnectionStrings": {
    "ConnectionString Name": "server=ip_address; user id=your_user_id; password=your_password; port=db_port_number; database=database_name;"
}
```

### Update MailSetting

Add or modify 'appsettings.json' and update parameters accordingly. One ConnectionString is for production and another is for local test.

```
"MailSettings": {
    "Mail": "Your Email Address",
    "DisplayName": "Your Display Name",
    "Password": "Your Password",
    "Host": "SMTP Address",
    "Port": Port Number (No double quote)
}
```

### API version documentation

Under Startup.cs, edit the following line for title and version:

```
c.SwaggerDoc("v1", new OpenApiInfo { Title = "Baking Bunny API", Version = "v1" });
```