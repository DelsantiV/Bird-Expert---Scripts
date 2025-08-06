using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert
{
    public class OptionsFieldManager : UIAreaMenu
    {
        [SerializeField] private List<UIOpenablePanel> panels;
        [SerializeField] private InfoText infoText;
        [SerializeField] private CustomButton saveButton;
        [SerializeField] private CustomButton applyButton;
        [SerializeField] private CustomButton backButton;
        [SerializeField] private ConfirmationBox confirmationBox;
        private GameModeSO gameMode { get => GameManager.Instance.gameMode; set => GameManager.Instance.gameMode = value; }
        public GameModeSO currentModifiedGameMode { get; set; }
        private OptionsField[] optionsFields;
        private ScrollRect scrollRect;
        private UIOpenablePanel activePanel 
        { 
            get 
            {
                return panels.Find(panel => panel.IsOpen);
            } 
        }
        public override void Initialize(bool active)
        {
            base.Initialize(active);
            currentModifiedGameMode = ScriptableObject.CreateInstance<GameModeSO>();
            optionsFields = GetComponentsInChildren<OptionsField>(includeInactive:true);
            scrollRect = GetComponent<ScrollRect>();
            scrollRect.content = activePanel.AreaRect;
            InitializeArea();
        }
        public void ActualizeArea()
        {
            foreach (var option in optionsFields) option.Actualize();
        }
        private void InitializeArea()
        {
            foreach (var option in optionsFields)
            {
                option.FieldManager = this;
                option.OnSettingChanged.AddListener(CheckSettings);
            }
            foreach (var option in optionsFields) option.Initialize();
            foreach (var panel in panels) panel.button.onClick.AddListener(() => OpenPanel(panel));
            OpenPanel(panels[0]);
            infoText.Initialize();
            infoText.transform.SetAsLastSibling();
            saveButton.SetListener(SaveGameModeConfirmation);
            applyButton.SetListener(PlayGameMode);
            backButton.SetListener(CloseGameModesCreationConfirmation);
        }
        private void OpenPanel(UIOpenablePanel panel)
        {
            panel.OpenArea();
            panels.Remove(panel);
            foreach(var pan in  panels) pan.CloseArea();
            panels.Add(panel);
            scrollRect.content = activePanel.AreaRect;
        }
        public void PlayGameMode()
        {
            gameMode = currentModifiedGameMode;
            menuManager.PlayGame(gameMode);
        }
        private void SaveGameModeConfirmation()
        {
            if (GameModesLoader.gameModesByName.ContainsKey(currentModifiedGameMode.name))
            {
                confirmationBox.Open("gamemode-name-exists");
                confirmationBox.AddConfirmationAction(SaveGameMode);
            }
            else
            {
                SaveGameMode();
            }
        }
        private void SaveGameMode()
        {
            GameManager.Instance.gameModesLoader.SaveSettings(currentModifiedGameMode);
            menuManager.AddGameMode(currentModifiedGameMode);
        }
        private void CloseGameModesCreationConfirmation()
        {
            if (!AllSettingsValid())
            {
                confirmationBox.Open("gamemode-notvalid");
                confirmationBox.AddCancelAction(menuManager.CloseGameModesCreation);
                confirmationBox.AddConfirmationAction(confirmationBox.Close);
                return;
            }
            bool saved = GameModesLoader.gameModesByName.TryGetValue(currentModifiedGameMode.name, out GameModeSO savedGameMode);
            if (saved)
            {
                if (!savedGameMode.IsEqualTo(currentModifiedGameMode))
                {
                    confirmationBox.Open("gamemode-notsaved");
                    confirmationBox.AddConfirmationAction(() => { SaveGameMode(); menuManager.CloseGameModesCreation(); });
                    confirmationBox.AddCancelAction(menuManager.CloseGameModesCreation);
                }
            }
            else menuManager.CloseGameModesCreation();
        }
        public void SetInfoText(string text) => infoText.OpenAtMousePosition(text);
        public void CloseInfoText() => infoText.Close();
        private void CheckSettings()
        {
            saveButton.interactable = AllSettingsValid();
        }
        private bool AllSettingsValid()
        {
            foreach (var option in optionsFields)
            {
                if (!option.isValid) return false;
            }
            return true;
        }
        public void LoadGameMode(GameModeSO gameMode)
        {
            currentModifiedGameMode = gameMode;
            foreach (var option in optionsFields) option.Actualize();
        }
    }
}
