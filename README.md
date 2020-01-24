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
```cs
SetDefault(KEY);
SetDefault("en");
SetDefault(LANGUAGE_OBJECT);
```

##### User:
```cs
SetLanguage(KEY);
SetLanguage("en");
SetLanguage(LANGUAGE_OBJECT);
```

## RoWa.Functions.cs
Todo

## RoWa.Debug.cs
Todo

## Rowa.Game.GameTime.cs
Todo