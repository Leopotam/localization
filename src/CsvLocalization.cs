// ----------------------------------------------------------------------------
// The MIT License
// Localization support https://github.com/Leopotam/localization
// Copyright (c) 2017-2019 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Leopotam.Localization {
    /// <summary>
    /// Localize - helper for localization. Supports dynamic overriding of localization tokens with rollback.
    /// </summary>
    public sealed class CsvLocalization {
        /// <summary>
        /// Current language.
        /// </summary>
        /// <value>The language.</value>
        public string Language {
            get { return _language; }
            set {
                if (!string.IsNullOrEmpty (value) && _language != value) {
                    _language = value;
                    FindLangId ();
                }
            }
        }

        readonly Regex CsvMultilineFixRegex = new Regex ("\"([^\"]|\"\"|\\n)*\"");
        readonly Regex CsvParseRegex = new Regex ("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");
        readonly List<string> _csvBuffer = new List<string> (32);
        readonly Dictionary<string, string[]> _statics = new Dictionary<string, string[]> (256);
        readonly Dictionary<string, string[]> _dynamics = new Dictionary<string, string[]> (256);
        readonly string _headerToken;

        string _language;
        int _langId;

        /// <summary>
        /// Default constructor with "English" as default language and "KEY" as default key-column name.
        /// </summary>
#if ENABLE_IL2CPP
        [UnityEngine.Scripting.Preserve]
#endif
        public CsvLocalization () : this ("English", "KEY") { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="language">Language on start.</param>
        /// <param name="headerToken">Header-line value at first column (key).</param>
        public CsvLocalization (string language, string headerToken) {
            _headerToken = headerToken;
            _langId = -1;
            _language = language;
        }

        /// <summary>
        /// Get localization for token.
        /// </summary>
        /// <param name="token">Localization token.</param>
        /// <param name="returnTokenOnFail">Should token value be returned as result if no data was found or null otherwise.</param>
        public string Get (string token, bool returnTokenOnFail = true) {
            if (_langId >= 0) {
                string[] retVals;
                if (_dynamics.TryGetValue (token, out retVals)) {
                    return retVals[_langId];
                }
                if (_statics.TryGetValue (token, out retVals)) {
                    return retVals[_langId];
                }
            }
            return returnTokenOnFail ? token : null;
        }

        /// <summary>
        /// Get pluralized string of count value.
        /// One / Two / Many pluralized versions are supported through
        /// "{token}-plural-one" / "{token}-plural-two" / "{token}-plural-many" naming schemas.
        /// </summary>
        /// <param name="count">Value to pluralize.</param>
        /// <param name="token">Base token of pluralization.</param>
        /// <param name="returnTokenOnFail">Should token value be returned as result if no data was found or null otherwise.</param>
        public string GetPlural (int count, string token, bool returnTokenOnFail = true) {
            if (count < 0) {
                count = -count;
            }
            string retVal;
            if (count == 1) {
                retVal = Get (string.Format ("{0}-plural-one", token), false);
                return retVal ?? (returnTokenOnFail ? token : null);
            }
            if (count > 1 && count < 5) {
                retVal = Get (string.Format ("{0}-plural-two", token), false);
                return retVal ?? (returnTokenOnFail ? token : null);
            }
            retVal = Get (string.Format ("{0}-plural-many", token), false);
            return retVal ?? (returnTokenOnFail ? token : null);
        }

        /// <summary>
        /// Add non-unloadable localization source.
        /// </summary>
        /// <param name="content">Csv source data.</param>
        public void AddStaticSource (string content) {
            if (!string.IsNullOrEmpty (content)) {
                ParseCsv (content, _statics);
                FindLangId ();
            }
        }

        /// <summary>
        /// Adds unloadable localization source. Can overrides loaded tokens and be removed by UnloadDynamics call.
        /// </summary>
        /// <param name="content">Csv source data.</param>
        public void AddDynamicSource (string content) {
            if (!string.IsNullOrEmpty (content)) {
                ParseCsv (content, _dynamics);
                FindLangId ();
            }
        }

        /// <summary>
        /// Unload all dynamics localization sources.
        /// </summary>
        /// <returns>The dynamics.</returns>
        public void UnloadDynamics () {
            _dynamics.Clear ();
            FindLangId ();
        }

        void ParseCsv (string data, Dictionary<string, string[]> storage) {
            // fix for multiline cells.
            data = CsvMultilineFixRegex.Replace (data, m => m.Value.Replace ("\n", "\\n"));
            var headerLen = -1;
            string key;
            using (var reader = new StringReader (data)) {
                while (reader.Peek () != -1) {
                    var line = reader.ReadLine ();
                    _csvBuffer.Clear ();
                    foreach (Match m in CsvParseRegex.Matches (line)) {
                        var part = m.Value.Trim ();
                        if (part.Length > 0) {
                            // unwrap string from quotes.
                            if (part[0] == '"' && part[part.Length - 1] == '"') {
                                part = part.Substring (1, part.Length - 2);
                            }
                            // remove double quotes.
                            part = part.Replace ("\"\"", "\"");
                            // restore multiline strings back.
                            part = part.Replace ("\\n", "\n");
                        }
                        _csvBuffer.Add (part);
                    }
                    if (_csvBuffer.Count > 0) {
                        if (headerLen == -1) {
                            headerLen = _csvBuffer.Count;
                        }
                        // skip invalid (short) csv lines.
                        if (_csvBuffer.Count < headerLen) {
                            continue;
                        }
                        key = _csvBuffer[0];
                        _csvBuffer.RemoveAt (0);
                        storage[key] = _csvBuffer.ToArray ();
                    }
                }
            }
            if (!storage.TryGetValue (_headerToken, out var header)) {
                storage.Clear ();
                _langId = -1;
                return;
            }
            FindLangId ();
        }

        void FindLangId () {
            string[] retVals = null;
            if (!string.IsNullOrEmpty (_language)) {
                if (!_dynamics.TryGetValue (_headerToken, out retVals)) {
                    _statics.TryGetValue (_headerToken, out retVals);
                }
            }
            _langId = retVals != null ? Array.IndexOf (retVals, _language) : -1;
        }
    }
}