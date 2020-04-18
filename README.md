[![license](https://img.shields.io/github/license/Leopotam/localization.svg)](https://github.com/Leopotam/localization/blob/develop/LICENSE.md)
# Localization support
[Github repo of localization support](https://github.com/Leopotam/localization).

> C#7.3 or above required for this library.

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.localization": "https://github.com/Leopotam/localization.git",
```
By default last released version will be used. If you need trunk / developing version then `develop` name of branch should be added after hash:
```
"com.leopotam.localization": "https://github.com/Leopotam/localization.git#develop",
```

## As source
If you can't / don't want to use unity modules, code can be downloaded as sources archive of required release from [Releases page](`https://github.com/Leopotam/localization/releases`).

# Classes

## CsvLocalization
Csv-base localization support.

### Static sources
All "static" sources can be added to current localization storage with `AddStaticSource` method and `overwrite data` with same tokens. Cant be unloaded from `CsvLocalization` instance.

### Dynamic sources
All "dynamic" sources can be added to current localization storage with `AddDynamicSource` method and `override data` with same tokens. Can be unloaded with `UnloadDynamics` method - all overrides will be reset to static sources if presents.

### Request data
Localized data can be requested with `Get` method.

### Plurals support
Localized data with plurals support can be requested with `GetPlural` method. Tokens (first column) should be properly named:
* 1 item - `xxx-plural-one` where `xxx` - normal token.
* 2-4 items - `xxx-plural-two` where `xxx` - normal token.
* 0 or > 4 items - `xxx-plural-many` where `xxx` - normal token.
```csharp
// loc - instance of CsvLocalization.
for (var i = 0; i < 25; i++) {
    loc.Language = "English";
    var enHour = $"{loc.GetPlural (i, "hour")}";
    var enDay = $"{loc.GetPlural (i, "day")}";
    loc.Language = "Russian";
    var ruHour = $"{loc.GetPlural (i, "hour")}";
    var ruDay = $"{loc.GetPlural (i, "day")}";
    Debug.Log ($"{i}: {enDay},{enHour} / {ruDay},{ruHour}");
}
```
Output:
```
0: days,hours / дней,часов
1: day,hour / день,час
2: days,hours / дня,часа
3: days,hours / дня,часа
4: days,hours / дня,часа
5: days,hours / дней,часов
6: days,hours / дней,часов
7: days,hours / дней,часов
8: days,hours / дней,часов
9: days,hours / дней,часов
10: days,hours / дней,часов
11: days,hours / дней,часов
12: days,hours / дней,часов
13: days,hours / дней,часов
14: days,hours / дней,часов
15: days,hours / дней,часов
16: days,hours / дней,часов
17: days,hours / дней,часов
18: days,hours / дней,часов
19: days,hours / дней,часов
20: days,hours / дней,часов
21: day,hour / день,час
22: days,hours / дня,часа
23: days,hours / дня,часа
24: days,hours / дня,часа
```

# License
The software released under the terms of the [MIT license](./LICENSE.md). Enjoy.

# Donate
Its free opensource software, but you can buy me a coffee:

<a href="https://www.buymeacoffee.com/leopotam" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/yellow_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>