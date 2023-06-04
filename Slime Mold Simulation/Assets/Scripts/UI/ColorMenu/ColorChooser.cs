using UnityEngine;
using UnityEngine.UI;

namespace UI.ColorMenu
{
    public class ColorChooser : ColorSelectable
    {
        public enum ChoiceNumber
        {
            one,
            two
        }

        [SerializeField] private Image thisImage;

        [SerializeField] private PickerManager picker;
        [SerializeField] public ChoiceNumber choiceNumber;

        public Color Color
        {
            get => thisImage.color;
            set
            {
                thisImage.color = value;
                switch (choiceNumber)
                {
                    case ChoiceNumber.one:
                        Simulation.simulationData.colorChoice1 = value;
                        break;
                    case ChoiceNumber.two:
                        Simulation.simulationData.colorChoice2 = value;
                        break;
                }
            }
        }

        private void Start()
        {
            switch (choiceNumber)
            {
                case ChoiceNumber.one:
                    thisImage.color = Simulation.simulationData.colorChoice1;
                    break;
                case ChoiceNumber.two:
                    thisImage.color = Simulation.simulationData.colorChoice2;
                    break;
            }
        }

        public override void Select()
        {
            picker.Activate(this);
        }
    }
}