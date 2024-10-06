using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBaseData
{
    public string code_sp;
    public string name_lat;
    public string name_fr;
    public string name_en;
    public float length;
    public float weight;
    public string order;
    public string family;
    public string[] habitat;
    public string[] food;
    public string[] similarPhysicalBirds;
    public string[] similarSoundsBirds;
    public bool physically_distinguable;

    public struct MorphoCaracteristics
    {
        public float length;
        public float weight;
    }

    public struct ClassifInfos
    {
        public string order;
        public string family;
    }

    public BirdBaseData() { }

    public BirdBaseData(string code_sp)
    {
        this.code_sp = code_sp;
    }

    public BirdBaseData(string code_sp, string name_lat, string name_fr, string name_en, float length, float weight, string order, string family, string[] habitat, string[] food, string[] similarPhysicalBirds, string[] similarSoundsBirds, bool physically_distinguable)
    {
        this.code_sp = code_sp;
        this.name_lat = name_lat;
        this.name_fr = name_fr;
        this.name_en = name_en;
        this.length = length;
        this.weight = weight;
        this.order = order;
        this.family = family;
        this.habitat = habitat;
        this.food = food;
        this.similarPhysicalBirds = similarPhysicalBirds;
        this.similarSoundsBirds = similarSoundsBirds;
        this.physically_distinguable = physically_distinguable;
    }

    public string GetName(Lang lang)
    {
        switch (lang)
        {
            case Lang.English: return name_en;
            case Lang.French: return name_fr;
            case Lang.Latin: return name_lat;
        }
        return null;
    }

    public MorphoCaracteristics Morpho
    {
        get
        {
            MorphoCaracteristics obj = new MorphoCaracteristics();
            obj.length = length; obj.weight = weight;
            return obj;
        }
    }
    public ClassifInfos Classif
    {
        get
        {
            ClassifInfos obj = new ClassifInfos();
            obj.order = order; obj.family = family;
            return obj;
        }
    }

    public string HabitatName(int index) { return habitat[index]; }
    public string TrophicNiche(int index) { return food[index]; }

    public static BirdBaseData CreateFromJson(string json)
    {
        return JsonUtility.FromJson<BirdBaseData>(json);
    }
}
