using UnityEngine;
using System.Collections.Generic;

namespace com.halbach.imageselection.input {

    public class MobileSelectionRectScaleState : MobileInputState
    {
        private Vector2 touchPointConnection;

        public MobileSelectionRectScaleState(RectTransform transformTarget) : base(transformTarget) {
            touchPointConnection = Vector2.zero;
        }

        protected override void UpdateInternal(List<Vector2> touchPositions, Vector2 newTouchPointConnection)
        {
            if(touchPointConnection != Vector2.zero) {

                Vector3 delta = touchPointConnection - newTouchPointConnection;

                ScaleSelectionRect(delta);
            }
            touchPointConnection = newTouchPointConnection;
        }

        protected override Vector2 CalculateDelta(List<Vector2> touchPoints) {
            Vector2 touchPointConnection = Vector2.zero;
            if(touchPoints.Count > 1) {
                Vector2 firstTouchPoint = touchPoints[0];
                Vector2 secondTouchPoint = touchPoints[1];

                touchPointConnection = touchPoints[0] - touchPoints[1];
            }

            return touchPointConnection;
        }

        private void ScaleSelectionRect(Vector3 delta) {
            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, transformTarget.rect.size.x - delta.x);
            transformTarget.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transformTarget.rect.size.y - delta.y);
        }
    }
}