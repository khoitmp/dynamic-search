# Dynamic Search
- A dynamic searching library which built from Linq.Dynamic.Core, main features including:
  - Paging
  - Sort
  - Filter

## 1. Compoments
### 1.1. The request payload
- Search without filter
```json
{
    "pageIndex": 0,     // Page index starts from 0
    "pageSize": 100,    // Page size default is 20
    "sorts": "id=desc,name=asc",
    "fields": ["id", "name", "type"],
    "filter": null
}
```

- Search with single filter
```json
{
    "pageIndex": 0,
    "pageSize": 100,
    "sorts": "id=desc,name=asc",
    "fields": ["id", "name", "type"],
    "filter": {
        "queryKey": "<query_key>",
        "queryType": "<query_type>",
        "operation": "<operation>",
        "queryValue": "<query_value>"
    }
}
```

- Search with multiple filter (and/or)
```json
{
    "pageIndex": 0,
    "pageSize": 100,
    "sorts": "id=desc,name=asc",
    "fields": ["id", "name", "type"],
    "filter": {
        "and": [ // or
            {
                "queryKey": "<query_key>",
                "queryType": "<query_type>",
                "operation": "<operation>",
                "queryValue": "<query_value>"
            },
            {
                "queryKey": "<query_key>",
                "queryType": "<query_type>",
                "operation": "<operation>",
                "queryValue": "<query_value>"
            }
        ]
    }
}
```

### 1.2. The filter
#### query_key
- The key of an object in the response when calling a search API

#### query_value
- The value we want to search

#### query_type

| Value             | Description                 |
| ----------------- | --------------------------- |
| text              | Text data type              |
| number            | Number data type            |
| boolean           | Boolean data type           |
| guid              | Guid data type              |
| nullable_guid     | Nullable Guid data type     |
| date              | Date data type              |
| nullable_date     | Nullable Date data type     |
| datetime          | Datetime data type          |
| nullable_datetime | Nullable datetime data type |

#### operation

| Value     | Description            |
| --------- | ---------------------- |
| eq        | Equals                 |
| neq       | Not equals             |
| in        | In                     |
| nin       | Not in                 |
| lt        | Less than              |
| lte       | Less than or equals    |
| gt        | Greater than           |
| gte       | Greater than or equals |
| contains  | Contains               |
| ncontains | Not contains           |
| ago       | Ago                    |
| between   | Between                |
| nbetween  | Not beetween           |
| sw        | Starts with            |
| nsw       | Not starts with        |
| ew        | Ends with              |
| new       | Not ends with          |

## 2. Sample
- For example you have a reponse from a search API as below. Currently, the response returning all data
```json
{
    "pageIndex": 0,
    "pageSize": 100,
    "totalPage": 1,
    "totalCount": 5,
    "durationInMilisecond": 11,
    "data": [
        {
            "id": "device5",
            "name": "Device 5",
            "createdUtc": "0001-01-01T00:00:00",
            "updatedUtc": "0001-01-01T00:00:00",
            "type": {
                "id": "COMMAND",
                "name": "Command"
            }
        },
        {
            "id": "device4",
            "name": "Device 4",
            "createdUtc": "0001-01-01T00:00:00",
            "updatedUtc": "0001-01-01T00:00:00",
            "type": {
                "id": "ALIAS",
                "name": "Alias"
            }
        },
        {
            "id": "device3",
            "name": "Device 3",
            "createdUtc": "0001-01-01T00:00:00",
            "updatedUtc": "0001-01-01T00:00:00",
            "type": {
                "id": "RUNTIME",
                "name": "Runtime"
            }
        },
        {
            "id": "device2",
            "name": "Device 2",
            "createdUtc": "0001-01-01T00:00:00",
            "updatedUtc": "0001-01-01T00:00:00",
            "type": {
                "id": "DYNAMIC",
                "name": "Dynamic"
            }
        },
        {
            "id": "device1",
            "name": "Device 1",
            "createdUtc": "0001-01-01T00:00:00",
            "updatedUtc": "0001-01-01T00:00:00",
            "type": {
                "id": "STATIC",
                "name": "Static"
            }
        }
    ]
}
```

- If you want to filter only item has name "Device 1", then the request payload would be
```json
{
    "pageIndex": 0,
    "pageSize": 100,
    "filter": {
        "queryKey": "name",
        "queryType": "text",
        "operation": "eq",
        "queryValue": "Device 1"
    }
}
```

- If you want to filter only item has name ends with "1" and type ends with "c", then the request payload would be
```json
{
    "pageIndex": 0,
    "pageSize": 100,
    "filter": {
        "and": [
            {
                "queryKey": "name",
                "queryType": "text",
                "operation": "ew",
                "queryValue": "1"
            },
            {
                "queryKey": "type.name",
                "queryType": "text",
                "operation": "ew",
                "queryValue": "c"
            }
        ]
    }
}
```

- If you want to filter on one-many object
```json
{
    "pageIndex": 0,
    "pageSize": 100,
    "filter": {
        "and": [
            {
                "queryKey": "isVisible",
                "queryType": "boolean",
                "operation": "eq",
                "queryValue": true
            },
            {
                "queryKey": "Templates.Any(x => x.Id.ToString() == \"<template_id>\")",
                "queryType": "boolean",
                "operation": "eq",
                "queryValue": true
            }
        ]
    }
}
```

## 3. Implementation
- Please refer project [DynamicSearch.Sample](./DynamicSearch.Sample/src/Core.Application/Services/DeviceService.cs)

## 4. Unit Testing
### Construct containers prepare for running UT
```yml
docker compose -f docker-compose.yml -f development.yml build
docker compose -f docker-compose.yml -f development.yml up -d
docker compose -f docker-compose.yml -f development.yml down
```

### Run UT to test the Dynamic Search through the containers
```
newman run -k -e ./DynamicSearch.Test/postman/docker.postman_environment.json ./DynamicSearch.Test/postman/core.postman_collection.json
```

## 5. Push
```sh
dotnet build
dotnet pack
dotnet nuget push --source nuget.org --api-key <api_key> <path>/<file_name>.nupkg
```