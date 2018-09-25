using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace weloveaero
{
    public class Airplane_Stage : MonoBehaviour
    {
        #region Variables
        private string etatRace = " ";
        public string EtatRace
        {
            get { return etatRace; }
            set { etatRace = value;  }
        }

        public int age = 30;
        private  Text StateRace;
        #endregion

        // Use this for initialization
        void Start()
        {

            
        StateRace = GameObject.Find("TxtRace").GetComponent<Text>();

           
        }

        // Update is called once per frame
        void Update()
        {
            StateRace.text =  " " + EtatRace;

          
        }
    }
}
