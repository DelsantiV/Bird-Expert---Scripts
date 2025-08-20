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
            else SetUpBadResult(input, answer);
        }
        private void SetUpGoodResult()
        {
            resultText.SetCodeText("good-answer");
        }

        private void SetUpBadResult(string input, string answer)
        {
            resultText.SetText(Language.GetLang("wrong-anwser-beginning") + input + Language.GetLang("wrong-answer-end") + answer);
        }
        public void ResetArea()
        {
            gameObject.SetActive(false);
            resultText.text = "";
        }
        public void StopQuizz() => nextBirdText.SetCodeText("gotoresults");
    } 
}
