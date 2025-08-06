using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BirdExpert
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MainArea mainArea;
        [SerializeField] private OptionsArea optionsArea;
        private UIAreaMenu activeArea;

        private void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            mainArea.Initialize(active: true, this);
            optionsArea.Initialize(active: false, this);
            activeArea = mainArea;
        }
        public void OpenGameModesCreation()
        {
            activeArea.CloseArea();
            optionsArea.OpenArea();
            activeArea = optionsArea;
        }
        public void CloseGameModesCreation()
        {
            optionsArea.CloseArea();
            mainArea.OpenArea();
            activeArea = mainArea;
        }
        public void PlayGame(GameModeSO gameMode)
        {
            GameManager.Instance.gameMode = gameMode;
            SceneManager.LoadSceneAsync("Game");
        }
        public void OpenBirdDisplayer()
        {
            SceneManager.LoadSceneAsync("Birdrairy");
        }
        public void OpenSettings()
        {
            Debug.Log("Should Open Settings !");
        }
        public void OpenCommunity()
        {
            Debug.Log("Should Open Community !");
        }
        public void AddGameMode(GameModeSO gameMode) => mainArea.AddGameMode(gameMode);
        public void RemoveGameMode(GameModeSO gameMode) => mainArea.RemoveGameMode(gameMode);
    }
}
