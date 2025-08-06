using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace BirdExpert
{
    public abstract class OptionsField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected string titleTextCode;
        [SerializeField] protected Image nonValidImage;
        [SerializeField] protected List<OptionsField> dependingFields;
        protected OptionsFieldManager optionsArea;
        public OptionsFieldManager FieldManager 
        { set 
            { 
                optionsArea = value;
                OnSettingChanged = new();
            } 
        }
        protected GameModeSO settings { get => optionsArea.currentModifiedGameMode ; set => optionsArea.currentModifiedGameMode = value; }
        public bool isValid;
        protected virtual string messageCode
        {
            get
            {
                if (isValid) return titleTextCode + "-info";
                else return titleTextCode + "-error";
            }
        }
        public virtual bool isInteractable { get; set; }
        public UnityEvent OnSettingChanged { get; protected set; }
        public virtual void Initialize()
        {
            if (title != null) title.text = Language.GetLang(titleTextCode);
            if (isInteractable) Actualize();
            OnValueChanged();
            foreach (var dependingField in dependingFields) dependingField.OnSettingChanged.AddListener(OnValueChanged);
        }
        protected virtual bool CheckValid() => true;
        public abstract void Actualize();
        protected virtual void OnValueChanged()
        {
            isValid = CheckValid();
            nonValidImage.gameObject.SetActive(!isValid);
        }
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            optionsArea.SetInfoText(Language.GetLang(messageCode));
        }
        public virtual void OnPointerExit(PointerEventData eventData) => optionsArea.CloseInfoText();
    }
}
