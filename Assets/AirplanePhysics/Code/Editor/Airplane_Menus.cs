using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace weloveaero
{
    public static class Airplane_Menus 
    {
        [MenuItem("Airplane Tools/Create New Airplane")]
        public static void CreateNewAirplane()
        {
//            IP_Airplane_SetupTools.BuildDefaultAirplane("New Airplane");
            AirplaneSetup_Window.LaunchSetupWindow();
        }
    }
}
