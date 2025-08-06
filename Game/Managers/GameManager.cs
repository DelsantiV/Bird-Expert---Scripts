using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BirdExpert
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Languages language;
        public GameModeSO gameMode;
        public GameModesLoader gameModesLoader;
        private BirdsLoader birdsLoader;
        private LoadingSceneCanvas loadingSceneCanvas;
        public UnityEvent dataReady = new();

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            Load();
        }

        private void Load()
        {
            loadingSceneCanvas = FindFirstObjectByType<LoadingSceneCanvas>();
            Language.LoadAllLanguages();
            Language.SetLanguage(language);
            loadingSceneCanvas.SetLoadingStage("loading-gamemodes", 0.1f);
            gameModesLoader = new();
            gameModesLoader.Ready.AddListener(LoadBirds);
        }

        private void LoadBirds()
        {
            birdsLoader = gameObject.AddComponent<BirdsLoader>();
            birdsLoader.LoadingSceneCanvas = loadingSceneCanvas;
            birdsLoader.StartLoading();
            birdsLoader.Ready.AddListener(OnDataReady);
        }
        private void OnDataReady()
        {
            loadingSceneCanvas.SetLoadingStage("loading-completed", 1f);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
