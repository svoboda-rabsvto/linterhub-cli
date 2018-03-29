# Engine
This document describes the engine definition
## Structure
The meta information of engine

|Key|Type|Required|Description|
|-|:-:|:-:|-|
|name|string|+|The engine name|
|customName|string|-|Custom engine name. Use this property when need to change default engine name on execute|
|description|string|+|The engine description|
|url|string|-|The engine url or homepage|
|version|[version](#version)|-|The engine version (expected)|
|languages|string[]|+|The list of supported languages. Possible values: `coffeescript`, `css`, `html`, `javascript`, `json`, `jsx`, `sass`, `typescript`|
|extensions|string[]|-|Common file extensions parsed by engine|
|license|string|+|The engine license. Possible values: `Unknown`, `AGPL-3.0`, `Apache-2.0`, `MIT`|
|requirements|[requirement](#requirement)[]|-|The engine requirements|
|areas|string[]|+|The engine areas. Possible values: `code simplification`, `commented code`, `complexity`, `documentation`, `duplication`, `formatting`, `grammar`, `memory leak`, `security`, `simplification`, `smell`, `spelling`, `syntax`, `undefined element`, `unreachable code`, `unused code`|
|acceptMask|boolean|-|Can use masks for multiple files analyze. Default is `true`|
|postfix|string|-|Posstfix in terminal (normaly post processor)|
|optionsDelimiter|string|-|Delimiter for options (space by default). Default is ` `|
|successCode|integer|-|Success exit code|
|active|boolean|-|A value indicating whether engine is active. Default is `true`|
|defaults|object|-|The default configuration of engine. This property is specific for each engine|
|stdin|[stdin](#stdin)|+|Support of stdin analyze|
### version
The engine version (expected)

|Key|Type|Required|Description|
|-|:-:|:-:|-|
|package|string|-|Package version|
|local|string|-|Local version|
### requirement
The engine dependency

|Key|Type|Required|Description|
|-|:-:|:-:|-|
|manager|string|-|The manager for dependency. Possible values: `system`, `url`, `composer`, `gem`, `npm`, `pip`|
|package|string|-|The package name|
### stdin
Support of stdin analyze

|Key|Type|Required|Description|
|-|:-:|:-:|-|
|available|boolean|-|Supports stdin or not|
|arguments|object|-|The stdin configuration of engine. This property is specific for each engine|
## Example
```
{
    "name": "string",
    "customName": "string",
    "description": "string",
    "url": "string",
    "version": {
        "package": "string",
        "local": "string"
    },
    "languages": [],
    "extensions": [],
    "license": "Unknown",
    "requirements": [
        {
            "manager": "system",
            "package": "string"
        }
    ],
    "areas": [],
    "acceptMask": false,
    "postfix": "string",
    "optionsDelimiter": "string",
    "successCode": 0,
    "active": false,
    "defaults": {},
    "stdin": {
        "available": false,
        "arguments": {}
    }
}
```
