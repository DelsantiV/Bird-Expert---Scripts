using TMPro;
using UnityEngine;

namespace BirdExpert
{
    public class EndArea : UIAreaMainGame
    {
        [SerializeField] TextMeshProUGUI scoreText;
        public void StopQuizz(int score)
        {
            scoreText.text = score.ToString() + "/" + canvasManager.numberOfBirdsInQuizz.ToString();
            gameObject.SetActive(true);
        }
    }
}
