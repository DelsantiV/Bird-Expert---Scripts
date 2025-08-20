using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BirdExpert
{
    public class BirdsManager
    {
        public static Dictionary<string, BirdInfo> allBirds;
        public static List<string> spCodesList { get => allBirds.Keys.ToList(); }
        public static List<string> GetAllNamesInLang(Lang lang) => allBirds.Values.Select(bird => bird.GetName(lang)).ToList();
        public BirdInfo GetBirdFromCode(string spCode)
        {
            return allBirds[spCode];
        }
        public BirdInfo GetBirdFromLang(Lang lang, string name)
        {
            return allBirds.Values.ToList().Find(bird => bird.GetName(lang) == name);
        }
        public BirdInfo GetRandomBird()
        {
            int birdnumber = spCodesList.Count;
            string birdCode = spCodesList[Random.Range(0, birdnumber)];
            return GetBirdFromCode(birdCode);
        }
    }
}
