using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BirdExpert
{
    public class EndArea : UIAreaMainGame
    {
        [SerializeField] TextMeshProUGUI scoreText;
        private List<QuizzAnswer> allAnswers;
        public void StopQuizz(List<QuizzAnswer> allAnswers)
        {
            this.allAnswers = allAnswers;
            int numberOfBirds = allAnswers.Count;
            int score = allAnswers.Where(answer => answer.isCorrect).Count();
            scoreText.text = score.ToString() + "/" + numberOfBirds.ToString();
            gameObject.SetActive(true);
        }
        private void SetUpAllCorrections()
        {
            foreach(QuizzAnswer answer in allAnswers)
            {
                SetUpCorrection(answer);
            }
        }
        private void SetUpCorrection(QuizzAnswer answer)
        {

        }
    }
}
