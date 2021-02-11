![Kaomoji Logo](/webcontent/KaomojiSharpIcon.png)
# kaomojisharp
### Kaomojis in your code!


To begin using KaomojiSharp, simply search for "KaomojiSharp" in the NuGet package manager or, alternatively, you can browse through this repository's packages [here](https://github.com/seylorx1/kaomojisharp/packages/).

#### How do I use KaomojiSharp in my code?

KaomojiSharp is (or at least should be) .NET 2.0 compliant and is an incredibly small library. A code example implementing KaomojiSharp can be found in the [example](https://github.com/seylorx1/kaomojisharp/tree/example) branch.

> The library is documented in Visual Studio's Intellisense as well as down below.

## Contents
- [Loading Kaomoji](#loading-kaomoji)
- [Customising Kaomoji](#customising-kaomoji)
- [Kaomoji Categories](#kaomoji-categories)

## Loading Kaomoji
If `Load();` is not called explicitely prior to doing so, KaomojiSharp will automatically load Kaomojis from a JSON file in your exectuable's folder named [KaomojiData.json](KaomojiData.json) whenever any `Kaomoji.Get...();` function is called. 

>  [KaomojiData](KaomojiData.json) should be added automagically whenever you import KaomojiSharp through NuGet.

In KaomojiSharp, there is the [KaomojiDataHandler](KaomojiDataHandler.cs) class which, as the name implies, handles data. At this point in time, the class handles ***data loading*** from JSON. This offers you complete freedom in customising Kaomoji for your purposes.

If you don't want KaomojiData to point towards the auto-generated [KaomojiData](KaomojiData.json) JSON file in your exectuable, but another file or a file in a different folder, you will need to explicitly load it by passing in a path **prior** to calling any `Kaomoji.Get...();` functions.
```csharp
//Loads Kaomojis from a custom path.
string path = "PATH LOCATION/KaomojiData.json";
KaomojiDataHandler.Load(path);

//Clones a random Kaomoji from the registry.
Kaomoji random = Kaomoji.GetRandom();
```

There is no limit to the amount of times you can call `KaomojiDataHandler.Load();` or `KaomojiDataHandler.Load(path);` directly. The registry doesn't clear between loads which means that you can load from multiple different JSON files using `KaomojiDataHandler.Load(path);`. To avoid duplicates, you should never load from the same file more than once. Keep in mind, as mentioned above, that if either `KaomojiDataHandler.Load();` function is not called prior to calling a `Kaomoji.Get...();` function, the library will automatically call the default `KaomojiDataHandler.Load();` function, which looks for a [KaomojiData](KaomojiData.json) JSON file in the executable folder.

```csharp
/*NEVER DO THIS*/
new Kaomoji.GetRandom();
KaomojiDataHandler.Load();
```

## Customising Kaomoji

### Adding Kaomoji at Runtime

> Keep in mind that runtime operations in KaomojiSharp, as of [v0.1](https://github.com/seylorx1/kaomojisharp/packages/617533), cannot not save to disk. Adding Kaomoji does **not** update the [KaomojiData](KaomojiData.json) JSON file.

Incredibly simple, creating and registering a new Kaomoji can be done in one line!

```csharp
new Kaomoji("=^vv^=", new KaomojiFlags(KaomojiFlags.Category.Positive, KaomojiFlags.Category.Joy)).Register();
```

In order to create a new Kaomoji, [KaomojiFlags](KaomojiFlags.cs) must be used. You can set flags by using a parameter list. These flags help narrow down which Kaomoji you want to use when using a `Kaomoji.Get...();` function.

```csharp
//Creates a new KaomojiFlags object with the flags *Positive* and *Joy*
KaomojiFlags flags = new KaomojiFlags(KaomojiFlags.Category.Positive, KaomojiFlags.Category.Joy);
```

> For categories, refer to the [Kaomoji Categories](#kaomoji-categories) table.

[Kaomoji](Kaomoji.cs) is a very simple class structure which only stores an `Emoticon` string and a `Flags` [KaomojiFlags](KaomojiFlags.cs) object.

```csharp
//Creates a new Kaomoji with the emoticon of "=^vv^=" and the KaomojiFlags object *flags* mentioned previously.
Kaomoji k = new Kaomoji("Emoticon", new KaomojiFlags(flags));
```

Finally, to register the [Kaomoji](Kaomoji.cs), just call the register function.

```csharp
//Register the Kaomoji to the internal registry.
k.Register();
```

### Manipulating the JSON File *(Recommended)*

The [KaomojiData.json](KaomojiData.json) file is a big table of Kaomoji emoticons and categories.

```json
{
      "Emoticon": "=^vv^=",
      "Categories": [ 0, 3 ]
},
```

It's fairly self explanatory. To add a new Kaomoji, create a new table element with a string `Emoticon` and an integer array `Categories`.

> For category IDs, refer to the [Kaomoji Categories](#kaomoji-categories) table.

To remove pre-existing Kaomoji, just remove that Kaomoji's element from the table.

Editing Kaomoji is as simple as changing the JSON text. 

While route doesn't offer the same benefits you would get by manipulating the Registry at runtime, it is a lot easier to work with, especially considering KaomojiDataHandler can only load Kaomoji right now.


## Kaomoji Categories
Category | ID | Is Broad?
--- | --- | ---
Positive | 0 | ✔
Neutral | 1 | ✔
Negative | 2 | ✔
Joy | 3 | ❌
Love | 4 | ❌
Embarrassment | 5 | ❌
Sympathy | 6 | ❌
Dissatisfaction | 7 | ❌
Anger | 8 | ❌
Sadness | 9 | ❌
Pain | 10 | ❌
Fear | 11 | ❌
Indifference | 12 | ❌
Confusion | 13 | ❌
Doubt | 14 | ❌
Surprise | 15 | ❌

### Is Broad?
*Postive, Neutral, and Negative* categories are **broad**. This means that at least one of them feature on **every Kaomoji**. This means that you can search for the `Positive` category which will return any positive - or 'good feeling' - Kaomoji whereas searching for a category which **is not broad** such as `Surprise` will only return kaomojis which indicate surprise.
