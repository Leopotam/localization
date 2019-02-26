# Localization support
[Github repo of localization support](https://github.com/Leopotam/localization).

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
The software released under the terms of the [MIT license](./LICENSE). Enjoy.

# Donate
Its free opensource software, but you can buy me a coffee:

<a href="https://www.buymeacoffee.com/leopotam" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/yellow_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>