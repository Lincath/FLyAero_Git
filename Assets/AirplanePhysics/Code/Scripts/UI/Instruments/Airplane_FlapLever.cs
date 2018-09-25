using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace weloveaero
{
    public class Airplane_FlapLever : MonoBehaviour, IAirplaneUI
    {
        #region Variables
        [Header("Flap Lever Properties")]
        public BaseAirplane_Input input;
        public RectTransform parentRect;
        public RectTransform handleRect;
        public float handleSpeed = 2f;
        #endregion


        #region Interface Methods
        public void HandleAirplaneUI()
        {
            if(input && parentRect && handleRect)
            {
                float height = parentRect.rect.height;
                Vector2 wantedHandlePosition = new Vector2(0f, -height * input.NormalizedFlaps);
                handleRect.anchoredPosition = Vector2.Lerp(handleRect.anchoredPosition, wantedHandlePosition, Time.deltaTime * handleSpeed);
            }
        }
        #endregion
    }
}
