using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert
{
    public class GamemodeNamesDisplayer : UIAreaMenu
    {
        [SerializeField] private GameModeButton gameModeButtonTemplate;
        private ScrollRect scrollRect;
        private Dictionary<GameModeSO, GameModeButton> buttons;
        public override void Initialize(bool active)
        {
            base.Initialize(active);
            scrollRect = GetComponent<ScrollRect>();
            buttons = new();
            SetButtons();
        }

        private void SetButtons()
        {
            foreach (GameModeSO gameMode in GameModesLoader.gameModesByName.Values) if (!gameMode.isFavorite) AddButton(gameMode);
            gameModeButtonTemplate.gameObject.SetActive(false);
        }
        public void AddButton(GameModeSO gameMode)
        {
            if (buttons.ContainsKey(gameMode)) return;
            GameModeButton gameModeButton = Instantiate(gameModeButtonTemplate, scrollRect.content);
            gameModeButton.gameObject.SetActive(true);
            gameModeButton.MenuManager = menuManager;
            gameModeButton.Initialize(gameMode);
            buttons[gameMode] = gameModeButton;
        }
        public void RemoveButton(GameModeSO gameMode) 
        {
            Destroy(buttons[gameMode].gameObject);
            buttons.Remove(gameMode);
        }
    }
}
