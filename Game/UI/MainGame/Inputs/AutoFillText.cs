using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BirdExpert
{
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
        public float height { get => GetComponent<RectTransform>().sizeDelta.y; }

        public void Initialize(string birdName, TMP_InputField inputField)
        {
            selectionImage = transform.Find("Image").gameObject;
            SelectTextBox(false);
            textBox = GetComponent<TextMeshProUGUI>();
            button = GetComponent<Button>();
            SetTextBox(birdName, inputField);
        }
        public void SelectTextBox(bool select)
        {
            if (selectionImage != null) selectionImage.SetActive(select);
        }

        private void SetTextBox(string name, TMP_InputField inputField)
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
}
