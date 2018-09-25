using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccelerometerInputButton : MonoBehaviour
{

    public float speed = 100F;
    public float rotationTurnMax = 45f;

    static bool calibrationCompleted = false;


    public static float inclinMinY = 0;
    private float inclinMinX = 0;

    public float speedUpRhightPositionSet = 200f;
    private float speedUpRhightPosition;
    public float smoothAngleSetUp = 1.0f;
    private float smoothAngle;


    public float setCountDown = 1.0f;
    private float setCountDown2 = 1.0f;

    private float countDown;
    private float countDown2;


    public Text calibrationTxt;
    public Text countDownTxt;

    // public GameObject panelRight;
    public GameObject panelLeft;


    private Vector3 angleValue;

    private int multiplicateurDeltaTime = 60;



    // Use this for initialization
    void Start()
    {

        inclinMinY = Input.acceleration.y;
        inclinMinX = Input.acceleration.x;

        calibrationTxt.enabled = false;
        countDownTxt.enabled = false;
        countDown = setCountDown;



        // panelRight.SetActive(true);
        panelLeft.SetActive(true);

        speedUpRhightPosition = speedUpRhightPositionSet;
        smoothAngle = smoothAngleSetUp;

    }

    // Update is called once per frame
    void Update()
    {
        angleValue = transform.rotation.eulerAngles;

        if (!calibrationCompleted) calibration();



        //panelRight.SetActive(false);
        panelLeft.SetActive(false);


        if (calibrationCompleted)
        {
            if (Input.touchCount > 0) //PARTIE OU ON APPUIE SUR L'ECRAN
            {


                Touch touch = Input.GetTouch(0);
                if (touch.position.x > 600)
                {
                    panelLeft.SetActive(true);


                    transform.Rotate((Input.acceleration.y - inclinMinY)* Time.deltaTime * multiplicateurDeltaTime * 10, 0, 0);
                    transform.Rotate(0, 0, -Input.acceleration.x);

                }


                if (touch.position.x < 200)
                {
                    // panelRight.SetActive(true);


                    transform.Rotate((Input.acceleration.y - inclinMinY) * Time.deltaTime * multiplicateurDeltaTime * 10, 0, 0);
                    transform.Rotate(0, 0, -Input.acceleration.x);

                }
            }


            else    //PARTIE SANS APPUYER SUR L'ECRAN
            {
                //panelRight.SetActive(false);
                panelLeft.SetActive(false);


                ////// REPLACE L'AVION DROIT AVANT DE TOURNER DANS LE SENS INVERSE
                if (Input.acceleration.x > 0.1 && angleValue.z > 5 && angleValue.z < 180)
                {
                    SmoothPlane(true);
                }

                else if (Input.acceleration.x < -0.1 && angleValue.z < 355 && angleValue.z > 180)
                {
                    SmoothPlane(true);
                }

                else
                {
                    transform.Rotate(0, (Input.acceleration.x * 1.5f) * Time.deltaTime * multiplicateurDeltaTime, 0);

                    SmoothPlane(false); ///////////////////////////

                    //Debug.Log("neutre");
                }

                transform.Rotate((Input.acceleration.y - inclinMinY) * Time.deltaTime * multiplicateurDeltaTime, 0, 0);
            }


            //////// UI provisoire/////////

            if (countDown2 >= 0.0f)
            {
                calibrationTxt.text = "Completed !";
                countDownTxt.enabled = false;
                countDown2 -= Time.deltaTime;

            }
            if (countDown2 < 0.0f)
            {
                calibrationTxt.enabled = false;
            }


        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed); // FAIT AVANCER L'AVION EN LIGNE DROITE

    }

    //AJOUTE UN SMOOTH QUAND L'AVION SE REMET DROIT

    void SmoothPlane(bool smoothUpRight)

    {
        // Debug.Log("pink fluffy unicorn dancing on rainbows");
        if (smoothUpRight)
        {
            transform.Rotate(0, 0, (-Input.acceleration.x * Time.deltaTime * speedUpRhightPosition) * Time.deltaTime * multiplicateurDeltaTime);

            if (angleValue.z < 20 || angleValue.z > 340)
            {

                if (speedUpRhightPosition > 50)
                {

                    speedUpRhightPosition -= Time.deltaTime * 500;

                }
            }
            else
            {
                speedUpRhightPosition = speedUpRhightPositionSet;
            }
        }

        else if (!smoothUpRight)
        {

            //// AU DESSUS DE ROTATION TURN MAX (45°) L'AVION ARRETE DE SE RENVERSER

            if (angleValue.z > 360 - rotationTurnMax)

            {

                transform.Rotate(0, 0, (-Input.acceleration.x * smoothAngle) * Time.deltaTime * multiplicateurDeltaTime);
            }

            else if (angleValue.z < rotationTurnMax)

            {

                transform.Rotate(0, 0, (-Input.acceleration.x * smoothAngle) * Time.deltaTime * multiplicateurDeltaTime);
            }


            //// SMOOTH LA FIN DU RENVERSEMENT DE L'AVION

            if (angleValue.z > rotationTurnMax - 10 && angleValue.z < 180)
            {
                if (smoothAngle > 0.5f)
                {
                    smoothAngle -= Time.deltaTime * .05f;
                }
            }

            else if (angleValue.z < 360 - rotationTurnMax + 10 && angleValue.z > 180)
            {
                if (smoothAngle > 0.5f)
                {
                    smoothAngle -= Time.deltaTime * .05f;
                }
            }

            else
            {
                smoothAngle = smoothAngleSetUp;
            }
        }
    }


    public void restartScene()
    {
        Debug.Log("restart Lvl");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }



    void calibration()
    {
        if (countDown >= 0.0f)
        {
            countDown -= Time.deltaTime;

        }
        if (countDown < 0.0f)
        {

            calibrationCompleted = true;
        }

        calibrationTxt.text = "Calibration running, stay still on playing position";
        countDownTxt.text = Mathf.Round(countDown).ToString();
        calibrationTxt.enabled = true;
        countDownTxt.enabled = true;

        if (countDown >= 0.5f)
        {
            if (Input.acceleration.x < inclinMinX)
            {
                inclinMinX = Input.acceleration.x;
            }

            if (Input.acceleration.y < inclinMinY)
            {
                inclinMinY = Input.acceleration.y;
            }
        }


    }
}