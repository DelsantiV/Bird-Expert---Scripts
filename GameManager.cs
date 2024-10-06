using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string birdDataMainAddress;
    [SerializeField] private List<string> assetLabelsToLoad;
    private AsyncOperationHandle<TextAsset> codeNamesLoadOpHandle;
    private BirdsManager birdsManager;
    private CanvasManager canvasManager;
    public Sprite testSprite;
    public AudioClip testAudioClip;
    private void Awake()
    {
        InitializeBirdsManager();
        InitializeCanvasManager();
    }

    private void InitializeBirdsManager()
    {
        birdsManager = gameObject.AddComponent<BirdsManager>();
        birdsManager.InitializeBirdManager(assetLabelsToLoad);
    }

    private void InitializeCanvasManager()
    {
        Canvas mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasManager = mainCanvas.gameObject.AddComponent<CanvasManager>();
        canvasManager.InitializeCanvasManager(birdsManager);
    }

}
