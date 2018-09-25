using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace weloveaero
{

    public class Airplane_Score : MonoBehaviour
    {

        #region Variables
        public  int Score = 0;
        private Text TextScore;
        #endregion

        // Use this for initialization
        void Start()
        {
            TextScore = GameObject.Find("TxtScore").GetComponent<Text>();

        }

        // Update is called once per frame
        void Update()
        {
            TextScore.text = "Score  " + Score;
        }
    }
}
