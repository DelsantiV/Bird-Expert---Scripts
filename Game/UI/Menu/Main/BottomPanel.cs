using UnityEngine;

namespace BirdExpert
{
    public class BottomPanel : UIAreaMenu
    {
        [SerializeField] private CustomButton otherGamemodesButton;
        [SerializeField] private GamemodeNamesDisplayer namesDisplayer;

        public override void Initialize(bool active)
        {
            base.Initialize(active);
            otherGamemodesButton.SetListener(namesDisplayer.ToggleArea);
            namesDisplayer.Initialize(false, menuManager);
        }
        public void AddGameMode(GameModeSO gameMode) => namesDisplayer.AddButton(gameMode);
        public void RemoveGameMode(GameModeSO gameMode) => namesDisplayer.RemoveButton(gameMode);
    }
}
