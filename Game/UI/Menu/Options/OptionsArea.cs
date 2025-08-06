using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BirdExpert
{
    public class OptionsArea : UIAreaMenu
    {
        [SerializeField] private OptionsFieldManager optionsFieldManager;
        [SerializeField] private GameModeLoadingPanel loadingPanel;
        public override void Initialize(bool active)
        {
            base.Initialize(active);
            optionsFieldManager.Initialize(true, menuManager);
            loadingPanel.OptionsArea = this;
            loadingPanel.Initialize(true, menuManager);
        }
        public override void OpenArea()
        {
            base.OpenArea();
            optionsFieldManager.ActualizeArea();
        }
        public void LoadGameMode(GameModeSO gameMode) => optionsFieldManager.LoadGameMode(gameMode);
    }
}