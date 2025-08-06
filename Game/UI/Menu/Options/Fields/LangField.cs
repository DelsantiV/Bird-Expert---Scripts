using System;
using UnityEngine;

namespace BirdExpert
{
    public class LangField : CustomDropdown
    {
        protected override Type enumType => typeof(Lang);
        public override void Actualize()
        {
            dropdown.value = (int)settings.lang;
        }
        protected override void SetValueInSettings(int index)
        {
            settings.lang = (Lang)index;
        }
    }
}
