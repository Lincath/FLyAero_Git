using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace weloveaero
{
    
    public class Airplane_Gate_Point : MonoBehaviour
    {
        public float Showduration = 1f;
        public float FadeSpeed = 0.05f;
        public float MoveStep = 0.05f;

        public GameObject TextGate;
        public GameObject Airplane;

        public void ShowGatepoint()
        {
            //Instantiate(TextGate, new Vector3(0, 42, 280), Quaternion.identity);
            Instantiate(TextGate, new Vector3(Airplane.transform.position.x, Airplane.transform.position.y , Airplane.transform.position.z + 100), Quaternion.identity);

            Debug.Log(" 100 points");
        }

       
    }
}
