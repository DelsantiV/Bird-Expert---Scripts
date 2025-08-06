using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert {
    public class ResultArea : MonoBehaviour
    {
        [SerializeField] private Button nextBirdButton;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI nextBirdText;
        public delegate void OnNextBirdButtonPressed();
        public OnNextBirdButtonPressed onNextBirdButtonPressed;

        private void Awake()
        {
            onNextBirdButtonPressed += ResetArea;
            nextBirdButton.onClick.AddListener(onNextBirdButtonPressed.Invoke);
        }

        public void SetResult(string input, string answer)
        {
            gameObject.SetActive(true);
            bool goodResult = input == answer;
            if (goodResult) SetUpGoodResult();
            else SetUpBadResult(answer);
        }
        private void SetUpGoodResult()
        {
            resultText.text = "Bonne r�ponse !";
        }

        private void SetUpBadResult(string answer)
        {
            resultText.text = "Mauvaise r�ponse, il s'agissait d'un(e) " + answer;
        }
        public void ResetArea()
        {
            gameObject.SetActive(false);
            resultText.text = "";
        }
        public void StopQuizz() => nextBirdText.text = "Acc�der aux r�sultats";
    } 
}
