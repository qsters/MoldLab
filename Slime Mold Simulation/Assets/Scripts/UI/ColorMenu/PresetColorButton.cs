using UnityEngine;

namespace UI.ColorMenu
{
    public class PresetColorButton : ColorSelectable
    {
        [SerializeField] public ColorSchemes thisScheme;

        public override void Select()
        {
            Simulation.simulationData.colorScheme = thisScheme;
            Simulation.simulationData.colorMode = ColorSchemeMode.Preset;
        }
    }
}