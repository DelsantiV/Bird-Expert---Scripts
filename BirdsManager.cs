using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class BirdsManager : MonoBehaviour
{
    private Dictionary<string, BirdInfo> allBirds;
    private List<string> spCodesList;
    public static UnityEvent Ready;

    public BirdInfo GetBirdFromCode(string spCode)
    {
        return allBirds[spCode];
    }

    public BirdInfo GetRandomBird()
    {
        int birdnumber = Names.codeNames.Count;
        string birdCode = Names.codeNames[Random.Range(0, birdnumber)];
        return GetBirdFromCode(birdCode);
    }

    private void Awake()
    {
        Ready = new UnityEvent();
        Ready.AddListener(OnAssetsLoadingFinished);
    }

    public void InitializeBirdManager(List<string> keys)
    {
        allBirds = new Dictionary<string, BirdInfo>();
        StartCoroutine(LoadAssetsFromMemory(keys));
    }
    IEnumerator LoadAssetsFromMemory(IList<string> keys)
    {
        Debug.Log("Start retrieving basic data assets");
        AsyncOperationHandle<IList<IResourceLocation>> baseDataLocations = Addressables.LoadResourceLocationsAsync("BaseData", typeof(TextAsset));
        yield return baseDataLocations;
        Debug.Log(baseDataLocations.Status.ToString());
        Debug.Log(baseDataLocations.Result.Count + " birds found in data");

        var loadOpsBasic = new List<AsyncOperationHandle>(baseDataLocations.Result.Count);
        foreach (IResourceLocation loc in baseDataLocations.Result)
        {
            AsyncOperationHandle<TextAsset> jsonFileHandle = Addressables.LoadAssetAsync<TextAsset>(loc);
            jsonFileHandle.Completed += OnBirdJsonDataLoaded;
            loadOpsBasic.Add(jsonFileHandle);
        }
        yield return Addressables.ResourceManager.CreateGenericGroupOperation(loadOpsBasic, true);
        spCodesList = allBirds.Keys.ToList();
        Names.codeNames = spCodesList;

        Debug.Log("Start retrieving bird images");
        AsyncOperationHandle<IList<IResourceLocation>> imagesLocations = Addressables.LoadResourceLocationsAsync("BaseData", typeof(Sprite));
        yield return imagesLocations;
        Debug.Log(imagesLocations.Status.ToString());
        Debug.Log(imagesLocations.Result.Count + " bird images found in data");

        var loadOpsImages = new List<AsyncOperationHandle>(imagesLocations.Result.Count);
        foreach (IResourceLocation loc in imagesLocations.Result)
        {
            AsyncOperationHandle<Sprite> spriteFileHandle = Addressables.LoadAssetAsync<Sprite>(loc);
            spriteFileHandle.Completed += OnBirdImageLoaded;
            loadOpsImages.Add(spriteFileHandle);
        }
        yield return Addressables.ResourceManager.CreateGenericGroupOperation(loadOpsImages, true);
        //Modifier pour ne charger les images que lorsque nécessaire ?


        Debug.Log("Start retrieving bird sounds");
        AsyncOperationHandle<IList<IResourceLocation>> soundsLocations = Addressables.LoadResourceLocationsAsync("BaseData", typeof(AudioClip));
        yield return soundsLocations;
        Debug.Log(soundsLocations.Status.ToString());
        Debug.Log(soundsLocations.Result.Count + " bird sounds found in data");

        var loadOpsSounds = new List<AsyncOperationHandle>(soundsLocations.Result.Count);
        foreach (IResourceLocation loc in soundsLocations.Result)
        {
            AsyncOperationHandle<AudioClip> soundFileHandle = Addressables.LoadAssetAsync<AudioClip>(loc);
            soundFileHandle.Completed += OnBirdSoundLoaded;
            loadOpsSounds.Add(soundFileHandle);
        }
        yield return Addressables.ResourceManager.CreateGenericGroupOperation(loadOpsSounds, true);
        //Modifier pour ne charger les sons que lorsque nécessaire ?

        Ready.Invoke();
    }

    private void OnBirdJsonDataLoaded(AsyncOperationHandle<TextAsset> jsonFileHandle)
    {
        if (jsonFileHandle.Status == AsyncOperationStatus.Succeeded)
        {
            CreateBirdInfo(jsonFileHandle.Result);
        }
    }

    private void CreateBirdInfo(TextAsset jsonFile)
    {
        BirdBaseData birdData = BirdBaseData.CreateFromJson(jsonFile.text);
        if (birdData != null)
        {
            BirdInfo bird = new BirdInfo(birdData);
            Debug.Log("Loaded " + bird.spCode);
            Names.RegisterBird(bird);
            allBirds.Add(bird.spCode, bird);
        }
        else
        {
            Debug.Log("Bird Initialization failed for bird " + birdData.GetName(Lang.Latin));
        }
    }


    private void OnBirdImageLoaded(AsyncOperationHandle<Sprite> spriteLoadHandle)
    {
        if (spriteLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite loadedSprite = spriteLoadHandle.Result;
            string spCode = loadedSprite.name[..6];
            BirdInfo bird = allBirds[spCode];
            Sex sex = GetSexFromImageName(loadedSprite.name);
            BirdInfo.BirdImage birdImage = new (loadedSprite, sex);
            bird.AddImage(birdImage);
        }
    }
    private void OnBirdSoundLoaded(AsyncOperationHandle<AudioClip> soundLoadHandle)
    {
        if (soundLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            AudioClip loadedSound = soundLoadHandle.Result;
            string spCode = loadedSound.name[..6];
            BirdInfo bird = allBirds[spCode];
            SoundType type = GetSoundTypeFromSoundName(loadedSound.name);
            BirdInfo.BirdSound birdSound = new (loadedSound, type);
            bird.AddSound(birdSound);
        }
    }

    private Sex GetSexFromImageName(string imageName)
    {
        switch (imageName.Substring(imageName.Length - 1))
        {
            case "M": return Sex.Male;
            case "F": return Sex.Female;
            default: return Sex.None;
        }
    }
    private SoundType GetSoundTypeFromSoundName(string soundName)
    {
        switch (soundName.Split("_").Last())
        {
            case "cri": return SoundType.Alarm;
            case "chant": return SoundType.Song;
            default: return SoundType.None;
        }
    }

    private void OnAssetsLoadingFinished()
    {

        Debug.Log(allBirds.Count.ToString() + " birds data loaded successfully");
    }
}
