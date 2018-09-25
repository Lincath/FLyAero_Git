using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weloveaero
{
    public class XboxAirplane_Input : BaseAirplane_Input 
    {
        #region Variables
        #endregion


        #region Custom Methods
        protected override void HandleInput()
        {
            //process Keyboard
            base.HandleInput();

            // edition des inputs dans   Edit -> project settings -> Input -> Axes
            pitch += Input.GetAxis("Vertical");
            roll += Input.GetAxis("Horizontal");
            yaw += Input.GetAxis("X_RH_Stick");
            yaw += Input.GetAxis("X_RH_Stick2");
            throttle += Input.GetAxis("X_RV_Stick");


            //Process Brake inputs
            brake += Input.GetAxis("Fire1");


            //Process Flaps Inputs
            if(Input.GetButtonDown("X_R_Bumper"))
            {
                flaps += 1;
            }

            if(Input.GetButtonDown("X_L_Bumper"))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapIncrements);

            //Camera Switch Button
            cameraSwitch = Input.GetButtonDown("X_Y_Button") || Input.GetKeyDown(cameraKey);

            
            //Restart
            if (Input.GetButtonDown("ReLoad"))
            {
                Application.LoadLevel("Airplane_Setup_Dev") ;
            }
            
        }
        #endregion
    }
}
