using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInfo
{
    private BirdBaseData baseData;

    private List<BirdImage> imagesList;
    private List<BirdSound> soundsList;
    private bool hasSexualDimorphism;
    public struct BirdImage
    {
        public Sprite image;
        public Sex sex;

        public BirdImage(Sprite image, Sex sex)
        {
            this.image = image;
            this.sex = sex;
        }
    }
    public struct BirdSound
    {
        public AudioClip sound;
        public SoundType type;

        public BirdSound(AudioClip sound, SoundType type)
        {
            this.sound = sound;
            this.type = type;
        }
    }

    public BirdInfo(BirdBaseData baseData) 
    {
        this.baseData = baseData;
        imagesList = new List<BirdImage>();
        soundsList = new List<BirdSound>();
    }


    public bool TryInitializeBirdInfos()
    {
        return false;
    }

    public void AddImage(BirdImage image)
    {
        imagesList.Add(image);
    }

    public void AddSound(BirdSound sound)
    {
        soundsList.Add(sound);
    }

    public void SetSexualDimorphism(bool hasSexualDimorphism)
    {
        this.hasSexualDimorphism = hasSexualDimorphism;
    }

    public string GetName(Lang lang)
    {
        return baseData.GetName(lang);
    }

    public string spCode { get { return baseData.code_sp; } }
    public BirdBaseData.MorphoCaracteristics Morpho
    {
        get
        {
            return baseData.Morpho;
        }
    }
    public BirdBaseData.ClassifInfos Classif 
    {
        get
        {
            return baseData.Classif;
        }
    }

    public string[] AllHabitatsNames { get { return baseData.habitat; } }
    public string HabitatName(int index) { return baseData.HabitatName(index); }
    public string[] AllTrophicNiches { get { return baseData.food; } }
    public string TrophicNiche(int index) { return baseData.TrophicNiche(index); }

    public BirdImage[] GetAllImages() { return imagesList.ToArray(); }
    public BirdImage GetImage(int index) { return imagesList[index]; }
    public BirdImage GetRandomImage(Sex sex = Sex.All) 
    {
        List<BirdImage> selectedSounds = new();
        if (sex == Sex.All) { selectedSounds = imagesList; }
        else { selectedSounds = imagesList.FindAll(image => image.sex == sex); }
        if (selectedSounds.Count == 0) { return new(); }
        else { return selectedSounds[Random.Range(0, selectedSounds.Count)]; }
    }

    public bool HasImageOfSex(Sex sex)
    {
        if (sex == Sex.All) { return imagesList.Count > 0; }
        else { return imagesList.Exists(image => image.sex == sex); }
    }

    public BirdSound[] GetAllAudioClips(SoundType type = SoundType.All) 
    {
        if (type == SoundType.All) return soundsList.ToArray(); 
        else return soundsList.FindAll(sound => sound.type == type).ToArray();
    }
    public BirdSound GetAudioClip(int index) { return soundsList[index]; }

    public BirdSound GetRandomSound(SoundType type = SoundType.All)
    {
        List<BirdSound> selectedSounds = new();
        if (type == SoundType.All) { selectedSounds = soundsList; }
        else { selectedSounds = soundsList.FindAll(sound => sound.type == type); }
        if (selectedSounds.Count == 0) { return new(); }
        else { return selectedSounds[Random.Range(0, selectedSounds.Count)]; }
    }
    public bool HasSoundOfType(SoundType type)
    {
        if (type == SoundType.All) { return soundsList.Count > 0; }
        else { return soundsList.Exists(sound => sound.type == type); }
    }

}
