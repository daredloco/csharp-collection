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

### Usage:
#### Check if User is Admin
```cs
IsUserAdmin(); //Returns true if user is admin
```

#### Convert a string to a hash
```cs
string message = "make a hash out of me!";
Hashtype ht = HashType.SHA256;

GetHash(message,ht);
```

#### Create a string with a currency prefix out of a double value
```cs
double value = 50.25;
string prefix = "£";

DoubleToCurrency(value,prefix);
```
#### Encrypts a string to bytes
```cs
//Not for production!
```

#### Decrypts a string from bytes
```cs
//Not for production!
```

#### Resizes the control to the size of the text in it
```cs
ResizeControl(CONTROL);
```

## RoWa.Debug.cs

### Usage:
#### Set log directory
```cs
logdir = "/directory/from/log";
```

#### Logging
```cs
string message = "this is a log message";
//Standart Log
Log(message);

//Exception Log
ExceptionLog(new Exception(message));

//Warning Log with popup
WarningLog(message, true);

//Warning Log without popup
WarningLog(message, false);

//Empty Log
EmptyLog();
```

## Rowa.Game.GameTime.cs

### Usage:
```cs
RoWa.Game.GameTime gt1 = new RoWa.Game.GameTime(); //Creates a default gametime with 1 second per tick
RoWa.Game.GameTime gt2 = new RoWa.Game.GameTime(500,false); //Creates a default gametime with 1 second per tick, an interval of 500ms and it won't autostart
RoWa.Game.GameTime gt3 = new RoWa.Game.GameTime(0,0,0,1,1,1,500); //Creates a default gametime with 1 second per tick and it starts with 0 hours, minutes seconds and 1 day, month and year and an interval of 500ms
RoWa.Game.GameTime gt4 = new RoWa.Game.GameTime(0,0,0,1,1,1,0,5,0,0,0,0,500); //Creates a  gametime with 5 minutes per tick and it starts with 0 hours, minutes seconds and 1 day, month and year and an interval of 500ms

gt2.Start(); //Starts the gametime if it's not started already
gt2.Stop(); //Stops the gametime if it's not stopped already
gt2.ToString("d.m.yyyy h:m:s"); //Gets the time with the format 'd.m.yyyy h:m:s

gt2.Tick(5); //Manually ticks 5 seconds
```

## RoWa.Game

### Usage:
```cs
//Vector2
RoWa.Game.Vector2 v21 = RoWa.Game.Vector2(); //Creates an empty Vector2 value
RoWa.Game.Vector2 v22 = RoWa.Game.Vector2(5,5); //Creates a Vector2 value with x and y values
RoWa.Game.Vector2.Compare(v21,v22); //Returns a Vector2 value with the difference between v21 and v22
RoWa.game.Vector2.Distance(v21,v22); //Returns a float with the distance between v21 and v22
RoWa.Game.Vector2.Parse("5/5"); //Creates a Vector2 value with x and y values from a string
RoWa.Game.Vector2 vout;
RoWa.Game.Vector2.TryParse("5/5", out vout); //Tries to create a Vector2 value with x and y values from a string to vout and returns true if it could parse it or false if not

//Vector3
RoWa.Game.Vector3 v31 = RoWa.Game.Vector3(); //Creates an empty Vector3 value
RoWa.Game.Vector3 v32 = RoWa.Game.Vector3(5,5,5); //Creates a Vector3 value with x, y and z values
RoWa.Game.Vector3.Compare(v31,v32); //Returns a Vector3 value with the difference between v31 and v32
RoWa.game.Vector3.Distance(v31,v32); //Returns a float with the distance between v31 and v32
RoWa.Game.Vector3.Parse("5/5/5"); //Creates a Vector2 value with x, y and z values from a string
RoWa.Game.Vector3 vout;
RoWa.Game.Vector3.TryParse("5/5/5", out vout); //Tries to create a Vector3 value with x, y and z values from a string to vout and returns true if it could parse it or false if not
```
