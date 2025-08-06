
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert {
    public class GameModeLoadingPanel : UIAreaMenu
    {
        [SerializeField] CustomButton gameModesButton;
        [SerializeField] GameModeLoadingButton buttonTemplate;
        private ScrollRect scrollRect;
        private Dictionary<GameModeSO, GameModeLoadingButton> buttons;
        private OptionsArea optionsArea;
        public OptionsArea OptionsArea { set =>  optionsArea = value; }
        public override void Initialize(bool active)
        {
            base.Initialize(active);
            buttons = new();
            scrollRect = GetComponentInChildren<ScrollRect>(includeInactive: true);
            scrollRect.gameObject.SetActive(false);
            gameModesButton.SetListener(ToggleViewport);
            SetButtons();
        }
        private void ToggleViewport() => scrollRect.gameObject.SetActive(!scrollRect.gameObject.activeSelf);
        private void SetButtons()
        {
            foreach (GameModeSO gameMode in GameModesLoader.gameModesByName.Values) AddButton(gameMode);
            Destroy(buttonTemplate.gameObject);
        }
        private void AddButton(GameModeSO gameMode)
        {
            GameModeLoadingButton gameModeButton = Instantiate(buttonTemplate, scrollRect.content);
            gameModeButton.Panel = this;
            gameModeButton.Initialize(gameMode);
            buttons[gameMode] = gameModeButton;
        }
        public void LoadGameMode(GameModeSO gameMode) => optionsArea.LoadGameMode(gameMode);
        public void DeleteGameMode(GameModeSO gameMode)
        {
            buttons.Remove(gameMode);
            menuManager.RemoveGameMode(gameMode);
            GameManager.Instance.gameModesLoader.TryRemoveSettings(gameMode);
        }
    } 
}
