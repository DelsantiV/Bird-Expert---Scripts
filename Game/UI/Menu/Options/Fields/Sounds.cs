using System;

namespace BirdExpert
{
    public class Sounds : CustomDropdown
    {
        protected override Type enumType => typeof(GameSettings.DataPresenceSettings);
        public override void Actualize()
        {
            dropdown.value = (int)settings.soundPresenceSetting;
        }

        protected override void SetValueInSettings(int index)
        {
            settings.soundPresenceSetting = (GameSettings.DataPresenceSettings)index;
        }
    }
}
