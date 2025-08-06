using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert {
    public class GameModesArea : UIAreaMenu
    {
        [SerializeField] private GameModePanel gameModePanelTemplate;
        [SerializeField] private RectTransform gameModeCreationPanel;
        [SerializeField] private RectTransform gameModesContent;
        private ScrollRect scrollRect;
        private Dictionary<GameModeSO, GameModePanel> gameModesPanelList;

        public override void Initialize(bool active)
        {
            scrollRect = GetComponent<ScrollRect>();
            SetGameModes();
        }

        private void SetGameModes()
        {
            gameModesPanelList = new();
            foreach (GameModeSO gameMode in GameModesLoader.gameModesByName.Values) if(gameMode.isFavorite) AddPanel(gameMode);
            gameModePanelTemplate.gameObject.SetActive(false);
            gameModeCreationPanel.GetComponentInChildren<Button>().onClick.AddListener(menuManager.OpenGameModesCreation);
            Canvas.ForceUpdateCanvases();
            scrollRect.horizontalScrollbar.value = 0;
        }
        public void AddPanel(GameModeSO gameMode)
        {
            if (gameModesPanelList.ContainsKey(gameMode))
            {
                Debug.Log("Already Existing Game Mode");
                return;
            }
            GameModePanel panel = Instantiate(gameModePanelTemplate, gameModesContent);
            panel.gameObject.SetActive(true);
            panel.MenuManager = menuManager;
            panel.Initialize(gameMode);
            gameModesPanelList[gameMode] = panel;
            gameModeCreationPanel.transform.SetAsLastSibling();
        }
        public void RemovePanel(GameModeSO gameMode)
        {
            Destroy(gameModesPanelList[gameMode].gameObject);
            gameModesPanelList.Remove(gameMode);
        }
    }
}