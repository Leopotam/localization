[![license](https://img.shields.io/github/license/Leopotam/localization.svg)](https://github.com/Leopotam/localization/blob/develop/LICENSE.md)
# Localization support
[Github repo of localization support](https://github.com/Leopotam/localization).

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

## As unity module from npm registry (Experimental)
This repository can be installed as unity module from external npm registry with support of different versions. In this way new block should be added to `Packages/manifest.json` right after opening `{` bracket:
```json
  "scopedRegistries": [
    {
      "name": "Leopotam",
      "url": "https://npm.leopotam.com",
      "scopes": [
        "com.leopotam"
      ]
    }
  ],
```
After this operation registry can be installed from list of packages from standard unity module manager.
> **Important!** Url can be changed later, check actual url at `README`.

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

# License
The software released under the terms of the [MIT license](./LICENSE.md). Enjoy.

# Donate
Its free opensource software, but you can buy me a coffee:

<a href="https://www.buymeacoffee.com/leopotam" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/yellow_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>