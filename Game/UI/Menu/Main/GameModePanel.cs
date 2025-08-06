using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

namespace BirdExpert
{
    public class GameModePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image imagesIcon;
        [SerializeField] private Image soundsIcon;
        [SerializeField] private Image limitIconTime;
        [SerializeField] private Image limitIconNumber;
        [SerializeField] private TextMeshProUGUI limitText;
        [SerializeField] private CustomButton playButton;
        [SerializeField] private Button favoriteButton;
        public GameModeSO gameMode { get; private set; }
        private MenuManager menuManager;
        public MenuManager MenuManager { set =>  menuManager = value; }

        public void Initialize(GameModeSO gameMode)
        {
            this.gameMode = gameMode;
            titleText.SetText(gameMode.name);
            imagesIcon.gameObject.SetActive(gameMode.imagePresenceSetting == GameSettings.DataPresenceSettings.Always);
            soundsIcon.gameObject.SetActive(gameMode.soundPresenceSetting == GameSettings.DataPresenceSettings.Always);
            limitIconNumber.gameObject.SetActive(gameMode.objective == GameSettings.GameObjective.NumberedQuizz);
            limitIconTime.gameObject.SetActive(gameMode.objective == GameSettings.GameObjective.TimedQuizz);
            playButton.SetListener(() => menuManager.PlayGame(gameMode));
            favoriteButton.onClick.AddListener(UnfavoriteGameMode);
        }
        private void UnfavoriteGameMode()
        {
            menuManager.RemoveGameMode(gameMode);
            gameMode.isFavorite = false;
            GameManager.Instance.gameModesLoader.SaveSettings(gameMode);
            menuManager.AddGameMode(gameMode);
        }
    }
}
