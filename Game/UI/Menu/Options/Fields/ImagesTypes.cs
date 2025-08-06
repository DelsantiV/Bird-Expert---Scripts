using System;

namespace BirdExpert
{
    public class ImagesTypes : CustomDropdown
    {
        protected override Type enumType => typeof(GameSettings.ImageSettings);
        public override void Actualize()
        {
            if (isInteractable) dropdown.value = (int)settings.imageSetting;
        }

        protected override bool CheckValid()
        {
            isInteractable = settings.imagePresenceSetting != GameSettings.DataPresenceSettings.Never;
            return base.CheckValid();
        }
        protected override void SetValueInSettings(int index)
        {
            settings.imageSetting = (GameSettings.ImageSettings)index;
        }
    }
}
