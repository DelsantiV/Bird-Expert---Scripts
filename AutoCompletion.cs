using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class AutoCompletion : MonoBehaviour
{
    public TMP_InputField inputField;
    public RectTransform resultsParent;
    public RectTransform prefab;

    public GameObject resultImage;
    public Sprite goodAnswerSprite;
    public Sprite badAnswerSprite;

    private GameObject validationButton;
    private CanvasManager canvasManager;
    private TextMeshProUGUI answerText;
    private string selectedName;
    private bool answerCompleted;
    private int selectedTextBoxIndex;
    private List<AutoFillText> textBoxesList;

    private float sizeForBirdName = 30f;
    private int maxNumerbOfBirdNames;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        canvasManager = transform.parent.GetComponent<CanvasManager>();
        answerText = resultImage.transform.Find("AnswerText").GetComponent<TextMeshProUGUI>();
        resultImage.SetActive(false);
        resultImage.transform.Find("ReplayButton").GetComponent<Button>().onClick.AddListener(Replay);
        answerCompleted = false;
        selectedTextBoxIndex = 0;
        textBoxesList = new();
    }

    private void Start()
    {
        resultsParent.gameObject.SetActive(false);
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
        // Reverse loop since you destroy children
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
            resultsParent.gameObject.SetActive(true);
            while (resultIndex < results.Count)
            {
                string name = results[resultIndex];
                AutoFillText textBox = Instantiate(prefab, resultsParent).GetComponent<AutoFillText>();
                textBoxesList.Add(textBox);
                textBox.SetTextBox(name, inputField);
                if (resultIndex == 0)
                {
                    SelectTextBox(textBox);
                    selectedTextBoxIndex = 0;
                }
                resultIndex++;
            }
            resultsParent.sizeDelta = new Vector2(resultsParent.sizeDelta.x, sizeForBirdName * results.Count + resultsParent.GetComponent<VerticalLayoutGroup>().padding.top + resultsParent.GetComponent<VerticalLayoutGroup>().spacing);
        }
        else
        {
            resultsParent.gameObject.SetActive(false);
        }
    }


    private List<string> GetResults(string input)
    {
        OnAnswerFilled(input);
        if (input == null || input == "" || input == " ")
        {

        }
        else
        {
            return Names.frenchNames.FindAll((str) => str.ToLower().IndexOf(input.ToLower()) >= 0);
        }
        return new List<string>();
    }

    private bool DoesInputExist(string input)
    {
         return Names.frenchNames.Contains(input);  
    }

    private void OnAnswerFilled(string input)
    {
        bool exists = DoesInputExist(input);
        validationButton.SetActive(exists);
        answerCompleted = exists;
    }

    private void InitializeValidationButton()
    {

        validationButton = transform.Find("ValidateButton").gameObject;
        validationButton.GetComponent<Button>().onClick.AddListener(ValidateAnswer);
        validationButton.SetActive(false);
    }

    private void ValidateAnswer()
    {
        string answer = inputField.text;
        resultImage.SetActive(true);
        validationButton.SetActive(false);
        inputField.interactable = false;
        if (answer == canvasManager.GetCurrentBird().GetName(Lang.French))
        {
            resultImage.GetComponent<Image>().sprite = goodAnswerSprite;
            resultImage.GetComponent<Image>().color = Color.green;
            answerText.text = "Bonne réponse !";
        }
        else
        {
            resultImage.GetComponent<Image>().sprite = badAnswerSprite;
            resultImage.GetComponent<Image>().color = Color.red;
            answerText.text = canvasManager.GetCurrentBird().GetName(Lang.French);
        }
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

    private void Replay()
    {
        resultImage.SetActive(false);
        inputField.text = "";
        canvasManager.SetUpRandomBird();
        inputField.interactable = true;
    }
}
