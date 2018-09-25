using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace weloveaero
{
    public class Airplane_Horizon : MonoBehaviour, IAirplaneUI
    {

        #region Variables
        [Header("HorizonProperties")]
        public Airplane_Controller airplane;
        public Airplane_Characteristics rollCharac;
        public RectTransform horizonArtificial;
        #endregion


        #region Interface Methods
        public void HandleAirplaneUI()
        {
            float horizonRotation = - airplane.transform.eulerAngles.z;
            //Debug.Log("Rotation euler :" + horizonRotation);

            if (airplane)
            {
                if (horizonArtificial)
                {
                        horizonArtificial.rotation =  Quaternion.Euler(0f, 0f, horizonRotation);
                }
            }
        }
        #endregion
    }
}
