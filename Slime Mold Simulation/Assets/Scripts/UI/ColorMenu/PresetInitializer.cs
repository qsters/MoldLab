using UI.ColorMenu;
using UnityEngine;

namespace UI
{
    public class PresetInitializer : MonoBehaviour
    {
        private void Start()
        {
            var schemeButtons = FindObjectsOfType<PresetColorButton>();

            foreach (var scheme in schemeButtons)
                if (scheme.thisScheme == Simulation.simulationData.colorScheme)
                {
                    scheme.Select();
                    ColorSelectable.selectedPreset = scheme;
                }
        }
    }
}