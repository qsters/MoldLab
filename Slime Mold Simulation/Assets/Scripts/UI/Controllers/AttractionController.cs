using UnityEngine;
using UnityEngine.UI;

namespace UI.Controllers
{
    public class AttractionController : FieldController
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite attractedSprite;
        [SerializeField] private Sprite repulsedSprite;

        public override void OnSelect()
        {
            selectedField.gameObject.GetComponent<Button>().Select();
            simulationDataSO.attractedToSpores = !simulationDataSO.attractedToSpores;
        }

        public override void ChangeValue(float amount)
        {
            //
        }

        public override void UpdateUI()
        {
            image.sprite = simulationDataSO.attractedToSpores ? attractedSprite : repulsedSprite;
        }
    }
}