using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BirdExpert
{
    public class InputFieldManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private RectTransform resultsParent;
        [SerializeField] private AutoFillText autoFillText;
        [SerializeField] private GameObject validationButton;
        [SerializeField] private RectTransform scrollViewContainer;
        private QuizzArea quizzArea;
        private string selectedName;
        private bool answerCompleted;
        private int selectedTextBoxIndex;
        private List<AutoFillText> textBoxesList;
        private List<string> birdNames;
        public bool Interactable { set { if (inputField != null) inputField.interactable = value; } }

        private void Awake()
        {
            inputField.onValueChanged.AddListener(OnInputValueChanged);
            answerCompleted = false;
            selectedTextBoxIndex = 0;
            textBoxesList = new();
            scrollViewContainer.gameObject.SetActive(false);
        }
        public void Initialize(QuizzArea quizzArea)
        {
            this.quizzArea = quizzArea;
            Debug.Log(quizzArea.gameMode.lang);
            birdNames = BirdsManager.GetAllNamesInLang(quizzArea.gameMode.lang);
            InitializeValidationButton();
        }

        private void Update()
        {
            GetInputs();
        }

        private void GetInputs()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnEnterPressed();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) { OnArrowPressed(false); Debug.Log(selectedTextBoxIndex); }
            if (Input.GetKeyDown(KeyCode.UpArrow)) { OnArrowPressed(true); }
        }

        private void OnInputValueChanged(string newText)
        {
            ClearResults();
            FillResults(GetResults(newText));
        }

        private void ClearResults()
        {
            selectedName = null;
            selectedTextBoxIndex = 0;
            textBoxesList.Clear();
            for (int childIndex = resultsParent.childCount - 1; childIndex >= 0; --childIndex)
            {
                Transform child = resultsParent.GetChild(childIndex);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }

        private void FillResults(List<string> results)
        {
            int resultIndex = 0;
            if (results.Count > 0)
            {
                scrollViewContainer.gameObject.SetActive(true);
                while (resultIndex < results.Count)
                {
                    string name = results[resultIndex];
                    AutoFillText textBox = Instantiate(autoFillText, resultsParent).GetComponent<AutoFillText>();
                    textBox.Initialize(name, inputField);
                    textBoxesList.Add(textBox);
                    if (resultIndex == 0)
                    {
                        SelectTextBox(textBox);
                        selectedTextBoxIndex = 0;
                    }
                    resultIndex++;
                }
            }
            else
            {
                scrollViewContainer.gameObject.SetActive(false);
            }
        }


        private List<string> GetResults(string input)
        {
            OnAnswerFilled(input);
            if (!(input == null || input == "" || input == " ")) return birdNames.FindAll((str) => str.ToLower().Contains(input.ToLower()));
            return new List<string>();
        }

        private bool DoesInputExist(string input)
        {
            return birdNames.Contains(input);
        }

        private void OnAnswerFilled(string input)
        {
            bool exists = DoesInputExist(input);
            validationButton.SetActive(exists);
            answerCompleted = exists;
        }

        private void InitializeValidationButton()
        {
            validationButton.GetComponent<Button>().onClick.AddListener(ValidateAnswer);
            validationButton.SetActive(false);
        }

        private void ValidateAnswer()
        {
            quizzArea.ProcessAnswer(inputField.text);
            inputField.text = "";
        }

        private void SelectTextBox(AutoFillText textBox, bool select = true)
        {
            textBox.SelectTextBox(select);
            selectedName = textBox.birdName;
        }

        private void OnEnterPressed()
        {
            if (answerCompleted)
            {
                ValidateAnswer();
            }
            else
            {
                if (selectedName != null)
                {
                    Debug.Log("Selecting " + selectedName);
                    inputField.text = selectedName;
                }
                else
                {
                    Debug.Log("No selected name");
                }
            }
        }

        private void OnArrowPressed(bool up)
        {
            if (textBoxesList.Count > 0)
            {
                if (selectedTextBoxIndex != -1)
                {
                    SelectTextBox(textBoxesList[selectedTextBoxIndex], false);
                    if (up && selectedTextBoxIndex == 0) { selectedTextBoxIndex = textBoxesList.Count - 1; }
                    else
                    {
                        selectedTextBoxIndex += up ? -1 : 1;
                        selectedTextBoxIndex %= resultsParent.childCount;
                    }
                    SelectTextBox(textBoxesList[selectedTextBoxIndex]);
                }
            }
        }

        public void ResetInputField()
        {
            inputField.text = "";
        }
    }
}
