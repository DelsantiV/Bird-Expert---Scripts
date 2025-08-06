using System.Collections.Generic;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BirdExpert
{
    public abstract class CustomDropdown : OptionsField
    {
        [SerializeField] protected TMP_Dropdown dropdown;
        protected int numberOfOptions;
        public override bool isInteractable { get => dropdown.interactable; 
            set { 
                dropdown.interactable = value;
                labelText.gameObject.SetActive(value);
            } 
        }
        private TextMeshProUGUI labelText;
        protected abstract Type enumType { get; }

        public override void Initialize()
        {
            labelText = dropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>();
            SetOptions(enumType);
            base.Initialize();
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        protected abstract void SetValueInSettings(int index);
        protected virtual void OnDropdownValueChanged(int index)
        {
            OnValueChanged();
            if (isValid) SetValueInSettings(index);
            OnSettingChanged.Invoke();
        }
        protected virtual void SetOptions(Type enumType)
        {
            dropdown.ClearOptions();
            List<string> options = Enum.GetNames(enumType).Select(el => Language.GetLang(el)).ToList();
            numberOfOptions = options.Count;
            dropdown.AddOptions(options);
        }
    }
    public abstract class CustomDropdownForBirdsOptions : OptionsField
    {
        [SerializeField] protected TMP_Dropdown dropdown;
        protected int numberOfOptions;
        public override void Initialize()
        {
            if (title != null) title.text = Language.GetLang(titleTextCode);
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        protected abstract void SetValueInSettings(int index);
        protected virtual void OnDropdownValueChanged(int index)
        {
            SetValueInSettings(index);
        }
    }
}
