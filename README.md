# C Sharp Collection

A collection of scripts for C#


## RoWa.LoCa.cs

### Usage:

#### Initialize files
##### Simple:
```cs
RoWa.LoCa.Init(LOCALIZATION_FILES_DIRECTORY);

RoWa.LoCa.Init("/localization");
```

##### With default language:
```cs
RoWa.LoCa.Init(LOCALIZATION_FILES_DIRECTORY,DEFAULT_LANGUAGE);

RoWa.LoCa.Init("/localization","en");
```

##### With extension:
```cs
RoWa.LoCa.Init(LOCALIZATION_FILES_DIRECTORY,DEFAULT_LANGUAGE,EXTENSION);
RoWa.LoCa.Init("/localization","en",".txt");
```

#### Translate
```cs
RoWa.LoCa.Trans(KEY);

RoWa.LoCa.Trans("word");

RoWa.LoCa.Translate("word");
```

#### Set languages
##### Default:
```
Must be set at the RoWa.LoCa.Init() function!
```

##### User:
```cs
UserLanguage = Languages[KEY];

UserLanguage = Languages["en"];
```

### Roadmap
- [x] Loading the languages
- [x] Translate the keys
- [ ] Set the default language (Can only be set on Init() at the moment)
- [ ] Set the user language (Will be set same as the default language)

## RoWa.Functions.cs
Todo

## RoWa.Debug.cs
Todo
