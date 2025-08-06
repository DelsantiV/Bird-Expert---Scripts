using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using static BirdExpert.GameSettings;

namespace BirdExpert
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "GameModeSO", menuName = "ScriptableObjects/GameSettings")]
    public class GameModeSO : ScriptableObject
    {
        public new string name;
        public GameObjective objective;
        public GameObjectiveLimitSettings objectiveLimitSettings;
        public int birdNumberLimit;
        public float timeLimit;
        public DataPresenceSettings soundPresenceSetting;
        public DataPresenceSettings imagePresenceSetting;
        public SoundType soundSetting;
        public ImageSettings imageSetting;
        public AnswerSettings answerSetting;
        public Lang lang;
        public Lang hintLang;
        public bool isFavorite;
        public bool traductionMode;
        public string habitat;
        public int rarityLimit;
        public string family;
        public string order;
        public string food;

        public bool IsEqualTo(GameModeSO other)
        {
            Type type = typeof(GameModeSO);
            foreach (FieldInfo fi in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                if (fi.Name == "name") continue;
                var val = fi.GetValue(this);
                var tval = fi.GetValue(other);
                if (val != tval) 
                {
                    return false; 
                }
            }
            return true;
        }
        public GameModeSO DeepCopy()
        { 
            GameModeSO copy = CreateInstance<GameModeSO>();
            Type type = typeof(GameModeSO);
            foreach (FieldInfo fi in type.GetFields())
            { 
                fi.SetValue(copy, fi.GetValue(this));
                if (fi.GetValue(this) != fi.GetValue(copy))
                {
                    Debug.Log(fi.Name);
                    Debug.Log(fi.GetValue(copy));
                    Debug.Log(fi.GetValue(this));
                }
            }
            return copy;
        }

    }

    public class GameSettings
    {
        public enum DataPresenceSettings
        {
            Never,
            OnlyWhenNeeded,
            Always
        }
        public enum ImageSettings
        {
            Base,
            Realistic
        }
        public enum GameObjective
        {
            NumberedQuizz,
            TimedQuizz
        }
        public enum GameObjectiveLimitSettings
        {
            Custom,
            Fixed
        }
        public enum AnswerSettings
        {
            Direct,
            End
        }
    }
}