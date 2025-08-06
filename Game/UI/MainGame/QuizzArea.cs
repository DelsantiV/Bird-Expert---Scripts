using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert { 
    public class QuizzArea : UIAreaMainGame
    {
        [SerializeField] private Image birdDisplayer;
        [SerializeField] private GameObject soundPlayerGO;
        [SerializeField] private InputFieldManager inputFieldManager;
        [SerializeField] private ResultArea resultArea;
        [SerializeField] private TextMeshProUGUI limitText;
        [SerializeField] private TextMeshProUGUI otherLangNameText;
        private AudioSource soundPlayerAudioSource;
        private Button soundPlayerButton;
        private BirdInfo currentBird;
        private BirdsManager birdsManager { get => canvasManager.birdsManager; }
        public GameModeSO gameMode { get => canvasManager.gameMode; }
        private int birdCount;
        private float timeCounter;
        private int pointsCounter;
        private List<BirdInfo> birdList;

        public override void Initialize(bool active)
        {
            base.Initialize(active);
            if (gameMode.answerSetting == GameSettings.AnswerSettings.Direct) resultArea.onNextBirdButtonPressed += GoToNextBird;
            resultArea.ResetArea();
            if (gameMode.soundPresenceSetting == GameSettings.DataPresenceSettings.Never)
            {
                soundPlayerGO.SetActive(false);
            }
            else
            {
                soundPlayerAudioSource = soundPlayerGO.GetComponent<AudioSource>();
                soundPlayerButton = soundPlayerGO.GetComponent<Button>();
                soundPlayerButton.onClick.AddListener(PlayBirdSound);
            }
            birdCount = 0;
            birdList = new List<BirdInfo>();
            otherLangNameText.gameObject.SetActive(gameMode.traductionMode);
            inputFieldManager.Initialize(this);
        }

        private void Update()
        {
            if (canvasManager.timeForQuizz != 0)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter > canvasManager.timeForQuizz)
                {
                    StopQuizz();
                }
                UpdateTimeText();
            }
        }
        private void UpdateTimeText()
        {
            limitText.SetText(Mathf.RoundToInt(timeCounter).FormatMinutesSeconds() + "  (" + Mathf.RoundToInt(canvasManager.timeForQuizz).FormatMinutesSeconds() + ")");
        }
        private void UpdateNumberText()
        {
            limitText.SetText(Language.GetLang("bird")+" n°"+birdCount+"/"+canvasManager.numberOfBirdsInQuizz);
        }

        private void DisplayBird(Sprite birdSprite)
        {
            if (birdSprite != null)
            {
                birdDisplayer.sprite = birdSprite;
            }
            else { Debug.Log("No Image found !"); }
        }
        private void ResetImage() => birdDisplayer.sprite = null;
        private void SetSound(AudioClip birdSound)
        {
            if (birdSound != null)
            {
                soundPlayerGO.SetActive(true);
                soundPlayerAudioSource.clip = birdSound;
            }
            else { Debug.Log("No Sound found !"); }
        }
        private void ResetSound() => soundPlayerAudioSource.clip = null;

        public void SetUpNewBird(BirdInfo bird)
        {
            if (gameMode.soundPresenceSetting == GameSettings.DataPresenceSettings.OnlyWhenNeeded) soundPlayerGO.SetActive(false);
            if (canvasManager.numberOfBirdsInQuizz !=0) UpdateNumberText();
            if (ShouldDisplayImage(bird)) DisplayBird(bird.GetRandomImage(gameMode.imageSetting).image);
            if (ShouldDisplaySound(bird)) SetSound(bird.GetRandomSound(typePriority: gameMode.soundSetting, findAnyway: true).sound);
            if (gameMode.traductionMode) otherLangNameText.SetText(bird.GetName(gameMode.hintLang));
            currentBird = bird;
            birdList.Add(bird);
            Debug.Log(bird.spCode + " is now set up !");
        }
        private bool ShouldDisplayImage(BirdInfo bird)
        {
            return gameMode.imagePresenceSetting switch
            {
                GameSettings.DataPresenceSettings.Always => true,
                GameSettings.DataPresenceSettings.OnlyWhenNeeded => bird.NeedsImage,
                _ => false,
            };
        }
        private bool ShouldDisplaySound(BirdInfo bird)
        {
            return gameMode.soundPresenceSetting switch
            {
                GameSettings.DataPresenceSettings.Always => true,
                GameSettings.DataPresenceSettings.OnlyWhenNeeded => bird.NeedsSound,
                _ => false,
            };
        }
        public void PlayBirdSound() => soundPlayerAudioSource.Play();
        public void StopBirdSound() => soundPlayerAudioSource.Stop();
        public bool IsPlayingBirdSound { get => soundPlayerAudioSource.isPlaying; }

        public void SetUpRandomBird()
        {
            birdCount++;
            BirdInfo bird = birdsManager.GetRandomBird();
            while (bird == currentBird)
            {
                bird = birdsManager.GetRandomBird();
            }
            SetUpNewBird(bird);
        }
        public void StartQuizz()
        {
            OpenArea();
            SetUpRandomBird();
        }

        public void ProcessAnswer(string input)
        {
            if (gameMode.soundPresenceSetting != GameSettings.DataPresenceSettings.Never)
            {
                StopBirdSound();
                ResetSound();
            }
            if (gameMode.imagePresenceSetting != GameSettings.DataPresenceSettings.Never) ResetImage();
            inputFieldManager.Interactable = false;
            if (input == currentBird.GetName(gameMode.lang)) {pointsCounter++;}
            if (gameMode.answerSetting == GameSettings.AnswerSettings.Direct) resultArea.SetResult(input, currentBird.GetName(gameMode.lang));
            else GoToNextBird();
            if (birdCount == canvasManager.numberOfBirdsInQuizz) resultArea.StopQuizz();
        }

        private void GoToNextBird()
        {
            inputFieldManager.ResetInputField();
            if (birdCount == canvasManager.numberOfBirdsInQuizz)
            { 
                StopQuizz();
                return;
            }
            inputFieldManager.Interactable = true;
            SetUpRandomBird();
        }

        private void StopQuizz()
        {
            canvasManager.StopQuizz(pointsCounter);
            CloseArea();
        }
    }
}
