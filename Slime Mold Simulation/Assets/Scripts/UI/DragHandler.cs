using UnityEngine;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UI
{
    public class DragHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        private enum Direction
        {
            Vertical,
            Horizontal,
        }

        private Direction direction;

        private int bufferFrames = 10;
        private Vector2 bufferDelta = new Vector2();


        public void OnDrag(PointerEventData eventData)
        {
            if (bufferFrames > 0)
            {
                bufferDelta += eventData.delta;

                float x = Mathf.Abs(bufferDelta.x);
                float y = Mathf.Abs(bufferDelta.y);
                if (x > y)
                {
                    direction = Direction.Horizontal;
                }
                else if (y > x)
                {
                    direction = Direction.Vertical;
                }
                else
                {
                    direction = Direction.Horizontal;
                }

                bufferFrames--;
            }

            
            

            float deltaRatio = 0f;
            if (direction == Direction.Horizontal)
            {
                deltaRatio = eventData.delta.x / Screen.width;
            }
            else
            {
                deltaRatio = eventData.delta.y / Screen.height;
            }

            var sensitivityModifier = 0.75f;
            if (Touch.activeFingers.Count == 2)
            {
                sensitivityModifier = 0.1f;
            }


            var scaledDelta = deltaRatio * sensitivityModifier;
            FieldController.selectedField.DragDetected(scaledDelta);
            UIHandler.ResetFading();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UIHandler.ResetFading();
        }

        public void ResetDirection()
        {
            bufferFrames = 10;
            bufferDelta = Vector2.zero;
        }
    }
}