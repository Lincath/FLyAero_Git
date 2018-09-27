using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class apprentissageControl : MonoBehaviour {

    public Text instructionTxt;

    public GameObject panelInstruction;
    public GameObject panelHiding;

    public Image arrow;
    public Image altimeter;
    public Image airSpeed;


    private int stepLearning = 0;
    private bool stop;


    // Use this for initialization
    void Start () {
        stepLearning = 0;
        stop = true;


        instructionTxt.text = "Press <color=#ffa500ff>R</color> to move forward";
        arrow.enabled = false;
        airSpeed.enabled = false;
        altimeter.enabled = false;

        panelHiding.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {

        // Debug.Log(stepLearning);
        if(stepLearning == 0)
        {
            if (Input.GetAxis("Throttle") < 0 && stop)
            {
                instructionTxt.text = "Well done";
                StartCoroutine(waitBeforeStep(2f));
                
            }

            else if (Input.GetAxis("Throttle") > 0 && stop)
            {
                instructionTxt.text = "Wrong side";
            }

            else if (stop)
            {
                instructionTxt.text = "Press <color=#ffa500ff>R</color> to move forward";
            }
        }

        if(stepLearning == 1 && stop)
        {
            airSpeed.enabled = true;
            panelHiding.SetActive(true);
            arrow.enabled = true;
            instructionTxt.text = "use <color=#ffa500ff>R</color> and<color=#ffa500ff> L </color> to change the <color=#ffa500ff>speed </color>";
            StartCoroutine(waitBeforeStep(5f));

        }


        if (stepLearning == 2 && stop)
        {
            panelHiding.SetActive(false);
            arrow.enabled = false;
            instructionTxt.enabled = false;
            StartCoroutine(waitBeforeStep(2f));
        }

        if (stepLearning == 3 && stop)
        {
            //Debug.Log(stepLearning);
            instructionTxt.enabled = true;
            altimeter.enabled = true;
            instructionTxt.text = "<size=40>Change <color=#ffa500ff>altitude</color> by moving up and down the<color=#ffa500ff> left joystick </color></size>";
            StartCoroutine(waitBeforeStep(3f));
        }

        if (stepLearning == 4 && stop)
        {
            instructionTxt.enabled = false;
        }

        if (stepLearning == 5 && stop)
        {
            instructionTxt.enabled = false;
        }

        if (stepLearning == 6 && stop)
        {
            instructionTxt.enabled = true;
            instructionTxt.text = "Well Done !";
            StartCoroutine(waitBeforeStep(3f));
        }

        if (stepLearning == 7 && stop)
        {
            instructionTxt.enabled = true;
            instructionTxt.text = "Now we will learn to <color=#ffa500ff>turn easly</color> !";
            StartCoroutine(waitBeforeStep(3f));
        }

        if (stepLearning == 8 && stop)
        {
            instructionTxt.text = "Use <color=#ffa500ff>L2</color> and <color=#ffa500ff>R2</color> to turn left and right !";
            StartCoroutine(waitBeforeStep(3f));
        }

        if (stepLearning == 9 && stop)
        {
            instructionTxt.enabled = false;
        }

        if (stepLearning == 10 && stop)
        {
            
            instructionTxt.enabled = true;
            instructionTxt.text = "Perfect";
            StartCoroutine(waitBeforeStep(3f));
        }

        if (stepLearning == 11 && stop)
        {
            instructionTxt.text = "<size=40>you can also <color=#ffa500ff>turn</color> by moving left and right the the<color=#ffa500ff> left joystick </color></size>";
            StartCoroutine(waitBeforeStep(3f));
        }

        if (stepLearning == 12 && stop)
        {
            instructionTxt.text = "Stage complete";
            StartCoroutine(waitBeforeStep(3f));
        }






        if (instructionTxt.enabled == false)
        {
            Debug.Log("Text et boite de text desactivé");
            panelInstruction.SetActive(false);
        }
        else
        {
            panelInstruction.SetActive(true);
        }
    }


    IEnumerator waitBeforeStep(float timer)
    {
        Debug.Log("go");
        stop = false;
        yield return new WaitForSeconds(timer);
        Debug.Log("stop");
        stepLearning +=1;
        stop = true;
    }


    void OnTriggerEnter(Collider other)
    {
      if (other.name == "Gate_001")
        {
            stepLearning = 5;
        }

      else if (other.name == "Gate_003")
        {
            stepLearning = 6;
        }

        else if (other.name == "Gate_006")
        {
            stepLearning = 10;
        }
    }

}
