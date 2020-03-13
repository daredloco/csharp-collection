# C Sharp Collection

A collection of scripts for C#

## Index
* [RoWa.LoCa.cs](#rowalocacs) (Simple localization system)

* [RoWa.Xamarin.LoCa.cs](#rowaxamarinlocacs) (Simple localization system for Xamarin)

* [RoWa.Functions.cs](#rowafunctionscs) (Various useful functions)

* [RoWa.Xamarin.Functions.cs](#rowaxamarinfunctionscs) (Various useful functions for Xamarin)

* [RoWa.Debug.cs](#rowadebugcs) (Debugging system)

* [RoWa.Settings.cs](#rowasettingscs) (Settings system)

* [RoWa.Game.cs](#rowagamecs) (Useful game functions)

* [RoWa.Game.GameTime.cs](#rowagamegametimecs) (Gametime system)

* [RoWa.Game.SaveHandler.cs](#rowagamesavehandlercs) (Savegame system)

## Requirements
C# 4.0+

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
## RoWa.Xamarin.LoCa.cs

### Requirements
Xamarin

### Usage:
The Xamarin version of RoWa.LoCa.cs is identical to the main version of the script. The only difference is the way the data is saved and the way it is loaded.

#### Save and Load the Data
The data has the same structure as in the main version, but it is saved as Android Asset. To load it simply write the assets directory to the RoWa.Xamarin.LoCa.Init() function like this:
```cs
RoWa.Xamarin.LoCa.Init("localization"); //Loads all files from the directory 'Project/Assets/localization'
```

#### Handle placeholders
With the newest update I did add a function to add placeholders to the translations which then will be replaced by code.

LanguageFile:
```
test1=This is a {TEST}
test2=You can set different {KEY1} with a {KEY2}
test3=You can also set unlimited {KEY1} with the new {KEY2}
```

Code:
```cs
KeyValuePair<string,string> kvp = new KeyValuePair<string,string>("{TEST}","Test");
RoWa.Xamarin.LoCa.Trans("test1",kvp); //output: This is a Test

Dictionary<string,string> dict = new Dictionary<string,string>();
dict.add("{KEY1}","keys");
dict.add("{KEY2}","dictionary");
RoWa.Xamarin.LoCa.Trans("test2",dict); //output: You can set different keys with a dictionary

KeyValuePair<string,string> vp1 = new KeyValuePair<string,string>("{KEY1}","KeyValuePairs");
KeyValuePair<string,string> vp2 = new KeyValuePair<string,string>("{KEY2}","system");
RoWa.Xamarin.LoCa.Trans("test3"),vp1,vp2); //output: You can slo set unlimited KeyValuePairs with the new system
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

## RoWa.Xamarin.Functions.cs
Same as RoWa.Functions.cs but without some Desktop only functions

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

## RoWa.Settings.cs

### Usage:
```cs
RoWa.Settings.Location = "settings.cfg" //Sets the location of the file (default is "settings.cfg"
RoWa.Settings.Load(); //Loads the settings from the file
RoWa.Settings.SetValue("language","en"); //Adds/Updates the value to the settings and saves the file
RoWa.Settings.RemoveValue("language"); //Removes the value from the settings and saves the file
RoWa.Settings.GetValue<T>("language"); //Returns a variable from type T with the value
```

## RoWa.Game.cs

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

## RoWa.Game.SaveHandler.cs

### Usage:
```cs
RoWa.Game.SaveHandler.SaveDirectory = "Savefiles" //Sets the directory of the savefiles
RoWa.Game.SaveHandler.SaveExtension = ".save" //Sets the extension of the savefiles

RoWa.Game.SaveHandler.SaveFile sf1 = new RoWa.Game.SaveHandler.SaveFile("game1"); //Loads or creates a new savefile
RoWa.Game.SaveHandler.SaveFile sf2 = new RoWa.Game.SaveHandler.SaveFile("game1","savename"); //Loads or creates a new savefile with a custom name 'savename'
RoWa.Game.SaveHandler.SaveFile sf3 = new RoWa.Game.SaveHandler.SaveFile("game1","",false); //Creates a new savefile but doesn't load it

sf3.Load(); //Manually loads the savefile

sf1.Add("player_points",1000); //Adds a value to the Savefile data
sf1.Get("player_points"); //Returns a value from the Savefile data
sf1.Remove("player_points"); //Removes a value from the Savefile data

RoWa.Game.Vector2 v = new RoWa.Game.Vector2(5,5);
sf2.Add("player_position",v); //Adds a special object to the Savefile data. (Vector2 and Vector3 are supported)

sf2.Save(); //Saves the data to the savefile
```
