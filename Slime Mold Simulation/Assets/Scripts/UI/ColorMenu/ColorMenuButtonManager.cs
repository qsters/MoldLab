using UnityEngine;

namespace UI.ColorMenu
{
    public class ColorMenuButtonManager : MonoBehaviour
    {
        [SerializeField] private CustomSchemeDisplay display;
        [SerializeField] private ColorSelectable customDisplay;

        public void OnChoice1Used()
        {
            display.ChangeMode(ColorSchemeMode.Choice1);
            Simulation.simulationData.colorMode = ColorSchemeMode.Choice1;
            customDisplay.UpdateSelected();
            SimulationDataPasser.UpdateAndSetData();
        }

        public void OnChoice2Used()
        {
            display.ChangeMode(ColorSchemeMode.Choice2);
            Simulation.simulationData.colorMode = ColorSchemeMode.Choice2;
            customDisplay.UpdateSelected();
            SimulationDataPasser.UpdateAndSetData();
        }

        public void OnGradientButtonUsed()
        {
            display.ChangeMode(ColorSchemeMode.Gradient);
            Simulation.simulationData.colorMode = ColorSchemeMode.Gradient;
            customDisplay.UpdateSelected();
            SimulationDataPasser.UpdateAndSetData();
        }
    }
}