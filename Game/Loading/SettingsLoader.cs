using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;
using System.Linq;

namespace BirdExpert
{
    public class GameModesLoader
    {
        private string savedSettingsPath { get => Application.dataPath + "/Data/Saved/Settings/"; }
        private GameModeSO gameMode { get => GameManager.Instance.gameMode; set => GameManager.Instance.gameMode = value; }
        public static Dictionary<string, GameModeSO> gameModesByName { get; private set; }
        public UnityEvent Ready;
        public GameModesLoader()
        {
            Ready = new UnityEvent();
            gameModesByName = new();
            GameManager.Instance.StartCoroutine(LoadAllSettings());
        }

        public void SaveSettings(GameModeSO gameMode)
        {
            if (!File.Exists(savedSettingsPath)) { Directory.CreateDirectory(savedSettingsPath); }
            string filename = gameMode.name;
            string json = JsonConvert.SerializeObject(gameMode, Formatting.Indented);
            File.WriteAllText(savedSettingsPath + filename + ".json", json);
            gameModesByName[gameMode.name] = gameMode;
        }
        public bool TrySaveSettings(GameModeSO gameMode)
        {
            if (!File.Exists(savedSettingsPath +  gameMode.name + ".json")) { return false; }
            else
            {
                SaveSettings(gameMode);
                return true;
            }
        }
        public bool TryRemoveSettings(GameModeSO gameMode)
        {
            if (!File.Exists(savedSettingsPath + gameMode.name + ".json")) { return false; }
            else
            {
                File.Delete(savedSettingsPath + gameMode.name + ".json");
                gameModesByName.Remove(gameMode.name);
                return true;
            }
        }
        private GameModeSO LoadSettings(string filename)
        {
            string json = File.ReadAllText(filename);
            GameModeSO savedSettings = (GameModeSO)ScriptableObject.CreateInstance(typeof(GameModeSO));
            JsonConvert.PopulateObject(json, savedSettings);
            return savedSettings;
        }
        public IEnumerator LoadAllSettings()
        {
            string[] savedSettingsFiles = Directory.GetFiles(savedSettingsPath).Where(file => Path.GetExtension(file) == ".json").ToArray();
            Debug.Log("Found " + savedSettingsFiles.Length + " saved play modes.");
            if (savedSettingsFiles.Length != 0)
            {
                foreach (string file in savedSettingsFiles)
                {
                    string name = (string)JObject.Parse(File.ReadAllText(file))["name"];
                    gameModesByName[name] = LoadSettings(file);
                }
            }
            yield return gameModesByName;
            gameMode = gameModesByName["ImageTest"];
            Ready.Invoke();
        }
    }
}
