using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace BirdExpert
{
    public enum Languages
    {
        English,
        French
    }
    public static class Language
    {
        private static Languages language;
        private static Dictionary<Languages,Dictionary<string, string>> allLanguages = new();
        private static Dictionary<string, string> currentLanguage;
        private static string languagePath = Application.dataPath + "/Data/Languages/";

        public static void ChangeLanguage(Languages language)
        {
            if (Language.language != language)
            {
                if (allLanguages.TryGetValue(language, out currentLanguage)) Language.language = language;
                else Debug.Log("Language " + language + " not found !");
            }
        }
        public static void SetLanguage(Languages language)
        {
            Language.language = language;
            currentLanguage = allLanguages[language];
        }
        public static string GetLang(string code)
        {
            string word = "";
            if(!currentLanguage.TryGetValue(code, out word)) Debug.Log("Code "+code+" not found in languages.");
            return word;
        }
        private static void LoadLanguage(Languages lang)
        {
            string filename = languagePath + lang.ToString() + ".json";
            if (!File.Exists(filename)) return;
            string json = File.ReadAllText(filename);
            allLanguages[lang] = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);
        } 
        public static void LoadAllLanguages()
        {
            foreach(var lang in Enum.GetValues(typeof(Languages)))
            {
                LoadLanguage((Languages) lang);
            }
        }
    }
}
