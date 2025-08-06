using System;
using UnityEngine;
namespace BirdExpert
{
    public class HintLangField : CustomDropdown
    {
        protected override Type enumType => typeof(Lang);
        public override void Actualize()
        {
            if (isInteractable) dropdown.value = (int)settings.hintLang;
        }
        protected override bool CheckValid()
        {
            isInteractable = settings.traductionMode;
            if (!isInteractable) return true;
            if (dropdown.value == (int)settings.lang) return false;
            return true;
        }
        protected override void SetValueInSettings(int index)
        {
            settings.hintLang = (Lang)index;
        }
        protected override void OnDropdownValueChanged(int index)
        {
            base.OnDropdownValueChanged(index);
        }
    }
}
