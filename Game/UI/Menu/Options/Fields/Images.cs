using System;

namespace BirdExpert
{
    public class Images : CustomDropdown
    {
        protected override Type enumType => typeof(GameSettings.DataPresenceSettings);
        public override void Actualize()
        {
            dropdown.value = (int)settings.imagePresenceSetting;
        }

        protected override void SetValueInSettings(int index)
        {
            settings.imagePresenceSetting = (GameSettings.DataPresenceSettings)index;
        }
    }
}
