using UnityEngine;
using UnityEngine.SceneManagement;

namespace BirdExpert
{
    public class BirdDisplayerManager : UIArea
    {
        [SerializeField] private BirdSpeciesDisplayer birdSpeciesDisplayer;
        [SerializeField] private CustomButton homeButton;
        [SerializeField] private CustomButton settingsButton;
        [SerializeField] private CustomButton communityButton;

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            birdSpeciesDisplayer.Initialize();
            homeButton.SetListener(GoBackToMenu);
        }
        private void GoBackToMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
