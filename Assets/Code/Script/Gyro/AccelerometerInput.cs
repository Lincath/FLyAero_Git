using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AccelerometerInput : MonoBehaviour {

    public float speed = 100F;
    static bool calibrationCompleted = false;
    private bool calibrationPreparation = false;

    private float inclinMinY = 0;
    private float inclinMinX = 0;
    private float inclinMinZ = 0;

    private float inclinMaxY = 0;
    private float inclinMaxX = 0;
    private float inclinMaxZ = 0;

    public float setCountDown = 3.0f;
    private float setCountDown2 = 1.0f;

    private float countDown;
    private float countDown2;

    private float smooth = 10.0f;

    public Text calibrationTxt;
    public Text countDownTxt;
    public Text textValeur;
    public Text textValeurAngle;

    private Vector3 angleValue;


    // Use this for initialization
    void Start () {

        calibrationTxt.enabled = false;
        countDownTxt.enabled = false;
        countDown = setCountDown;
        countDown2 = setCountDown2;



    }
	
	// Update is called once per frame
	void Update () {
        angleValue = transform.rotation.eulerAngles;

        textValeur.text ="Y =" + System.Math.Round(inclinMinY,4).ToString() + '\n' +  "X min = " + System.Math.Round(inclinMinX,4).ToString()+'\n' + "X max = " + System.Math.Round(inclinMaxX, 4).ToString();
        textValeurAngle.text = "Angle Z =" + System.Math.Round(angleValue.z, 4).ToString();
        

        //Debug.Log(Input.acceleration.x - inclinMinX);

        if (calibrationCompleted)
        {

            if (countDown2 >= 0.0f)
            {
                calibrationTxt.text = "Completed !";
                countDownTxt.enabled = false;
                countDown2 -= Time.deltaTime;

            }
            if (countDown2 < 0.0f)
            {
                //calibrationTxt.enabled = false;
            }

            transform.Rotate(0, Input.acceleration.x - inclinMinX, 0);
            transform.Rotate(Input.acceleration.y - inclinMinY, 0, 0);


            smoothDetermination();


            ////////////////////////////////
            ////ANGLE AVION EN TOURNANT ////
            ////////////////////////////////

            if (Input.acceleration.x - inclinMinX < inclinMinX)
            {
                calibrationTxt.color = Color.green;
                calibrationTxt.text = "Gauche !";
                transform.Rotate(0, 0, 10 * Time.deltaTime);
            }

            else if (Input.acceleration.x - inclinMinX > -inclinMinX + 0.1)
            {
                calibrationTxt.color = Color.yellow;
                calibrationTxt.text = "Droite !";
                transform.Rotate(0, 0, -10 * Time.deltaTime);
            }

            
            else
            {
                calibrationTxt.color = Color.red;
                calibrationTxt.text = "Neutre";
                


                if (angleValue.z < 355)
                {
                    if (angleValue.z >= 180)
                    {
                        transform.Rotate(0, 0, smooth * Time.deltaTime);
                    }

                    else if (angleValue.z < 180)
                    {
                        transform.Rotate(0, 0, -smooth * Time.deltaTime);
                    }

                }
            }

        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (!calibrationCompleted) calibration();

    }


    void calibration()
    {
        if(!calibrationPreparation)
        {
            inclinMinY = Input.acceleration.y;
            inclinMinX = Input.acceleration.x;
            inclinMinZ = Input.acceleration.z;

            inclinMaxY = Input.acceleration.y;
            inclinMaxX = Input.acceleration.x;
            inclinMaxZ = Input.acceleration.z;

            calibrationPreparation = true;
}


        if (countDown >= 0.0f)
        {
            countDown -= Time.deltaTime;

        }
        if(countDown < 0.0f)
        {

            calibrationCompleted = true;
        }

        calibrationTxt.text = "Calibration running, stay still on playing position";
        countDownTxt.text = Mathf.Round(countDown).ToString();
        calibrationTxt.enabled = true;
        countDownTxt.enabled = true;

        if (countDown <= setCountDown-0.5f && countDown >= 0.5f)
         {
            if(Input.acceleration.x < inclinMinX) inclinMinX = Input.acceleration.x;
            else if(Input.acceleration.x > inclinMaxX) inclinMaxX = Input.acceleration.x;

            if (Input.acceleration.y < inclinMinY) inclinMinY = Input.acceleration.y;
            else if (Input.acceleration.y > inclinMaxY) inclinMaxY = Input.acceleration.y;
         }
        

    }


    void smoothDetermination()
    {


        if (angleValue.z >= 330 || angleValue.z <= 60)
        {
            Debug.Log("between 60 and 330");


        }

        else if (angleValue.z < 330 && angleValue.z >= 270 || angleValue.z >60 && angleValue.z <= 90)
        {
            Debug.Log("reussi");
        }

        else if(angleValue.z < 270 && angleValue.z > 90)
        {
            Debug.Log("50");
        }


    }
}
