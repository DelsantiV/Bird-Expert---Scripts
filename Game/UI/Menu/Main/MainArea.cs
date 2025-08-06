using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BirdExpert
{
    public class MainArea : UIAreaMenu
    {
        [SerializeField] private GameModesArea gameModeArea;
        [SerializeField] private BottomPanel bottomPanel;
        [SerializeField] private CustomButton settingsButton;
        [SerializeField] private CustomButton birdDiplayerSceneButton;
        [SerializeField] private CustomButton communityButton;
        public override void Initialize(bool active)
        {
            base.Initialize(active);
            gameModeArea.Initialize(true, menuManager);
            bottomPanel.Initialize(true, menuManager);
            settingsButton.SetListener(OpenSettings);
            birdDiplayerSceneButton.SetListener(OpenBirdDisplayer);
            communityButton.SetListener(OpenCommunity);
        }

        private void OpenSettings() => menuManager.OpenSettings();
        private void OpenBirdDisplayer() => menuManager.OpenBirdDisplayer();
        private void OpenCommunity() => menuManager.OpenCommunity();
        public void AddGameMode(GameModeSO gameMode) 
        {
            if (gameMode.isFavorite) gameModeArea.AddPanel(gameMode);
            else bottomPanel.AddGameMode(gameMode);
        }
        public void RemoveGameMode(GameModeSO gameMode)
        {
            if (gameMode.isFavorite) gameModeArea.RemovePanel(gameMode);
            else bottomPanel.RemoveGameMode(gameMode);
        }
    }
}
