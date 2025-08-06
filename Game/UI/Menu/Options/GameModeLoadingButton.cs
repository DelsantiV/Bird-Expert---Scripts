using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert
{
    public class GameModeLoadingButton : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button deleteButton;
        private GameModeSO gameMode;
        private GameModeLoadingPanel panel;
        public GameModeLoadingPanel Panel { set =>  panel = value; }
        public  void Initialize(GameModeSO gameMode)
        {
            this.gameMode = gameMode;
            loadButton.onClick.AddListener(LoadGameMode);
            deleteButton.onClick.AddListener(DeleteGameMode);
            nameText.SetText(gameMode.name);
        }
        private void LoadGameMode() => panel.LoadGameMode(gameMode);
        private void DeleteGameMode()
        {
            panel.DeleteGameMode(gameMode);
            Destroy(gameObject);
        }
    }
}
