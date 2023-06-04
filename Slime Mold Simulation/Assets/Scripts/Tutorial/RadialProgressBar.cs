using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class RadialProgressBar : MonoBehaviour
    {
        [SerializeField] private Image UIElement;

        public void SetPercent(float percent)
        {
            UIElement.fillAmount = percent;
        }
    }
}