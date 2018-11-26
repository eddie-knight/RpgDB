# RPG Database for Unity

This is a simple database builder for use in RPG style games. The goal of this asset is to provide a simple solution for translating information from a human-readable spreadsheet into programmable game data.

While JSON may have "Javascript" in its name, it is an easily accessible and widely recognized format. In thirty seconds a spreadsheet can be converted to JSON and saved in a directory that can be parsed by this asset.

That being said, this asset may require some customization to be useful in specific situations. It was built using a D&D derivative as inspiration, but with a few small tweaks the data format can be massively changed to fit any situation.

The following README contains development notes to make customizations painless.

### Requirements

JSON.net for Unity
https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347


## Overview

### GameDatabase.cs

After attaching this script to a GameObject, this script will initialize at runtime with several components that hold parsed data. These are held as components so that the JSON is parsed only once, at runtime.

The components are:
- Weapons
- Ammunition
- Armor
- Upgrades
- Classes


## Constructors

### Constructors/Database.cs
`public abstract class Database : MonoBehaviour`

This MonoBehaviour is the base class for the components that are built by `RpgDB.GameDatabase`, and provides all common functionality for parsing the JSON data. 

- **RpgDB.Database.JsonHome**

	Location of directory containing JSON files relative to project home.

	Default: `public static string JsonHome = @"Assets/RpgDB/json/"`

- **RpgDB.Database.AddObject(JToken item, List<IRpgDBEntry> list, string category=null)**
	
	Void class that is defined individually within all subsequent _database_ classes. 

	Example from `Constructors/Equipment/WeaponDatabase.cs`:
	```
	public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
	{
	    Weapon weapon = new Weapon();
	    weapon.ConvertObject(item, category);
	    list.Add(weapon);
	}
	```

	This function takes in a list of `RpgDB.IRpgDBEntry` objects and converts them to their appropriate status using `RpgDB.RpgDBObject.ConvertObject`, then appends the newly converted object to the provided list.

- **RpgDB.Database.GetJsonFromFile(string file_path)**

	Takes in a simple file path string, then prepends it with `RpgDB.Database.JsonHome`, appends it with `.json`. Once the file path is complete, this function will use the disposable `System.IO.StreamReader` to convert the file contents into a string variable: `file_output`.

	`Newtonsoft.Json.Linq.JObject.Parse(file_output)` will load a JToken from a string that contains JSON. This JToken is necessary for further datahandling, and is returned by the function upon its creation.

- **RpgDB.Database.LoadDataFromJson(string category, List<IRpgDBEntry> list)**

	Uses the string `category` as the `file_path` for `RpgDB.Database.GetJsonFromFile(string file_path)`. The object that is returned will hold a single "parent" JToken, which will contain a series of "child" JTokens as defined in the JSON file.

	This function will loop through each child item, running `RpgDB.Database.AddObject` to convert the JToken into an `RpgDB.IRpgDBEntry` that may be added to the provided list.

- **RpgDB.Database.LoadData(string[] categories, List<IRpgDBEntry> list)**

	Intermediary between `RpgDB.Database.LoadDataFromJson` and the array of category names that is provided when run by the child classes of `RpgDB.Database`.

- **RpgDB.Database.LoadData(string category, List<IRpgDBEntry> list)**

	Intermediary between `RpgDB.Database.LoadDataFromJson` and the child classes of `RpgDB.Database`.

	This overload allows category arrays and single categories to be handled identically.


### Constructors/IRpgDBEntry.cs

This interface does more than create a contract for it's adherants to follow- and the contract it does require is simply a `name` and `id`. The true value of this interface is that it allows dynamic functionality for helper functions that are not benefitted by static typing.

This is used most frequently by the children of the `Database` class, in order to allow common functionality to be passed up to the parent. Without this interface, all of the `Database` functions would need to be repeated across child classes with almost no change.

The greatest value of this interface, however, comes in the inventory. By using this interface, we are able to create an inventory system that is simply a `List<IRpgDBEntry>`. Without this, the inventory system would require a high degree of specificity and customization to fit every type of object that it holds.


## Constructors/Character



## Constructors/Equipment



### Categories

Categories are defined at the top of the WeaponsDatabase class, and are used on `start()`. The categories are simple strings, which are used for the following:

1. Specify the name of the JSON file. This will be passed as the `file_path` in the method `Database.GetJsonFromFile(file_path)`, which will concatenate it to create the full file path: `JsonHome + file_path + ".json")`
1. Assist in the parsing of JSON. The object created by `Newtonsoft.Json.Linq.JObject.Parse` requires a top-level JSON object to hold the subsequent entries. Each JSON file will have a top level item that shares the name of the JSON file.
1. Provide a category for searching. `WeaponDatabase.AddWeapon` takes a parameter called `title`, which will used to populate the field `Weapon.Category`. This field is subsequently used by `WeaponDatabase.SearchWeaponsByCategory` in order to filter `AllWeaponsList`.

#### JSON

The `/json` directory is used for housing data files. The JSON files are handled by `JSON.net`. Should you choose to modify the location of the JSON files, simply modify `Database.JsonHome` accordingly.

> **Debugging Tip:**
> If an error occurs due to a JSON entry, run the script
> and look at which item is the last entered to the db.
> The _next_ item on the list caused the error.

### Search

There is currently one primary search function that can be used in the code: 

`Database.GetByName()`

This returns an object of the type that is specific to the database, such as `class Weapon`

**Example Search:**
```
 Weapon debugWeapon = FindWeaponByName("Bow");
 PropertyInfo[] properties = typeof(Weapon).GetProperties();
 foreach (PropertyInfo property in properties)
 {
    Debug.Log(property.Name + ": " + property.GetValue(debugWeapon, null));
 }
```


## Optional Modifications

### Categories

	```
	public static string[] meleeCategories = { "1h_melee", "2h_melee" };
	public static string[] rangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };
	```
	If the JSON file names are modified, or if additional files are added, be sure to modify the top-level object name _and_ the appropriate categories list(s) in the associated class object. This enables the data to be properly loaded.

## TODO

### Searching
Search functionality will need dramatic improvement. Currently, the only reliable search function across all categories is `GetByName()`. WeaponDatabase has starters for other search types that should be refined before being implemented elsewhere. 

Also, **the search functions are all currently case-sensitive**.
