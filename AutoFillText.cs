using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoFillText : MonoBehaviour
{
    private GameObject selectionImage;
    public string birdName
    {
        get
        {
            return textBox.text;
        }
        set 
        { 
            textBox.text = value;
        }
    }
    private TextMeshProUGUI textBox;
    private Button button;
    private TMP_InputField inputField;

    private void Awake()
    {
        selectionImage = transform.Find("Image").gameObject;
        SelectTextBox(false);
        textBox = GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }
    public void SelectTextBox(bool select)
    {
        selectionImage?.SetActive(select);
    }

    public void SetTextBox(string name,  TMP_InputField inputField)
    {
        birdName = name;
        this.inputField = inputField;
        button.onClick.AddListener(delegate { OnClickedName(name); });
    }

    private void OnClickedName(string name)
    {
        Debug.Log(name + " clicked !");
        inputField.text = name;
    }
}
