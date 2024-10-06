using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour 
{
    private Canvas mainCanvas;
    private Image birdDisplayer;
    private GameObject soundPlayerGO;
    private AudioSource soundPlayerAudioSource;
    private Button soundPlayerButton;
    private BirdInfo currentBird;
    private BirdsManager birdsManager;
    public void InitializeCanvasManager (BirdsManager birdsManager)
    {
        this.birdsManager = birdsManager;
        mainCanvas = GetComponent<Canvas>();
        birdDisplayer = mainCanvas.transform.Find("BirdDisplayer").GetComponent<Image>();
        soundPlayerGO = mainCanvas.transform.Find("SoundPlayer").gameObject;
        soundPlayerAudioSource = soundPlayerGO.GetComponent<AudioSource>();
        soundPlayerButton = soundPlayerGO.GetComponent<Button>();
        soundPlayerButton.onClick.AddListener(PlayBirdSound);
        BirdsManager.Ready.AddListener(SetUpRandomBird);
    }

    private void DisplayBird(Sprite birdSprite)
    {
        if (birdSprite != null)
        {
            birdDisplayer.sprite = birdSprite;
        }
        else { Debug.Log("No Image found !"); }
    } 

    private void SetSound(AudioClip birdSound)
    {
        if (birdSound != null)
        {
            soundPlayerAudioSource.clip = birdSound;
        }
        else { Debug.Log("No Sound found !"); }
    }

    public void SetUpNewBird(Sprite birdSprite, AudioClip birdSound)
    {
        DisplayBird(birdSprite);
        SetSound(birdSound);
    }
    public void SetUpNewBird(BirdInfo bird)
    {
        DisplayBird(bird.GetRandomImage().image);
        SetSound(bird.GetRandomSound(SoundType.Song).sound);
        currentBird = bird;
        Debug.Log(bird.spCode + " is now set up !");
    }

    public void PlayBirdSound()
    {
        soundPlayerAudioSource.Play();
    }

    public void StopBirdSound()
    {
        soundPlayerAudioSource.Stop();
    }

    public bool IsPlayingBirdSound
    {
        get { return soundPlayerAudioSource.isPlaying; }
    }

    public BirdInfo GetCurrentBird() { return currentBird; }

    public void SetUpRandomBird()
    {
        BirdInfo bird = birdsManager.GetRandomBird();
        while (bird == currentBird)
        {
            bird = birdsManager.GetRandomBird();
        }
        SetUpNewBird(bird);
    }
}
