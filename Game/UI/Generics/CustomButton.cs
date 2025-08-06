using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace BirdExpert
{
    public class CustomButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] string titleTextCode;
        private Button button;
        public bool interactable { get => button.interactable; set => button.interactable = value; }
        private void Awake()
        {
            button = gameObject.GetOrAddComponent<Button>();
            SetText(Language.GetLang(titleTextCode));
        }
        public void SetListener(UnityAction call)
        {
            if (button == null) button = gameObject.GetOrAddComponent<Button>();
            button.onClick.AddListener(call);
        }
        public void SetText(string text) => title.text = text;
    }
}
