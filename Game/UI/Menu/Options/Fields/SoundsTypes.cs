using System;

namespace BirdExpert
{
    public class SoundsTypes : CustomDropdown
    {
        protected override Type enumType => typeof(SoundType);
        public override void Actualize()
        {
            if (isInteractable) dropdown.value = (int)settings.soundSetting;
        }
        protected override bool CheckValid()
        {
            isInteractable = settings.soundPresenceSetting != GameSettings.DataPresenceSettings.Never;
            return base.CheckValid();
        }
        protected override void SetValueInSettings(int index)
        {
            settings.soundSetting = (SoundType)index;
        }
    }
}
