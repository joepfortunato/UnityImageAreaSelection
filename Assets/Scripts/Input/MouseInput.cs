﻿using UnityEngine;


namespace com.halbach.imageselection.input {

    public delegate void SelectionChanged(object sender);

    public class MouseInput : PointerInput
    {
        private IMouseInputState mouseInputState;

        protected override void InitializeInternal(RectTransform transformTarget, BoxCollider2D selectionRectCollider, Camera renderCamera) {
            
            MouseInputState newMouseInputState = new MouseInputState(propertyContainer, transformTarget);
            mouseInputState = newMouseInputState;
            pointerState = newMouseInputState;
            pointerState.AddOnSelectionChangedEvent(GetOnSelectionChangedEvent());
        }

        public override void UpdateInternal()
        {
            UpdateMousePos(); 
        }

        private void UpdateMousePos()
        {
            mouseInputState = mouseInputState.UpdateMousePostion(ConvertCurrentMousePostionToWorldSpace());
        } 

        void OnMouseDown() 
        {
            mouseInputState = mouseInputState.MouseDown(ConvertCurrentMousePostionToWorldSpace());
        }

        void OnMouseUp() {
            mouseInputState = mouseInputState.MouseUp(ConvertCurrentMousePostionToWorldSpace());
        }

        private Vector3 ConvertCurrentMousePostionToWorldSpace() {
            return ConvertToWorldSpace(Input.mousePosition);
        }

        private Vector3 ConvertToWorldSpace(Vector3 vector) {
            return Camera.main.ScreenToWorldPoint(vector);
        }
    }
}