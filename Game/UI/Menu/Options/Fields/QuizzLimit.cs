using UnityEngine;
using System;

namespace BirdExpert
{
    public class QuizzLimit : CustomDropdown
    {
        protected override Type enumType => typeof(GameSettings.GameObjective);
        public override void Actualize()
        {
            dropdown.value = (int)settings.objective;
        }

        protected override void SetValueInSettings(int index)
        {
            settings.objective = (GameSettings.GameObjective)index;
        }
    }
}