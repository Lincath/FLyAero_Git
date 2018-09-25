using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopPrefabFigure : MonoBehaviour
{
    
    private GameObject objectOnCollider;
    public int numberOnLoop;
    private string numberOnLoopString;
    private string numberOnLoopStringMoreOne;

    public bool alreadyIncrease;

    public Text CheckpointSuccess;
    public Text scoreTxt;

    public int checkPointPassageSuccess;

    public string figureName;

    public GameObject nextCheckpoint;
    public GameObject CurrentCheckpoint;
    public GameObject CurrentFigure;

    private int nbrPerfect;
    private int nbrGood;
    private int nbrBad;

    private CalculScore scriptScore;
    private FigureManager managerScript;
    private GameObject manager;




    void Start()
    {
        numberOnLoop = 1;
        checkPointPassageSuccess = 0;
        CheckpointSuccess.enabled = false;

        alreadyIncrease = false;

        manager = GameObject.FindWithTag("manager");
        managerScript = manager.GetComponent<FigureManager>();
      




    }


    void Update()
    {

    }

    public void setScript()
    {
       // Debug.Log("set script");
        Getcheckpoint(true);

        CurrentFigure = GameObject.FindWithTag("onGoingFigure");
        scriptScore = CurrentFigure.GetComponent<CalculScore>();
    }


    void Getcheckpoint(bool callOnstart)
    {

        numberOnLoopString = numberOnLoop.ToString();
        CurrentCheckpoint = GameObject.Find(numberOnLoopString);
        

        if (callOnstart)
        {
            
            if (CurrentCheckpoint != null)
            {
               /* if (CurrentCheckpoint.transform.root != transform)
                {
                    CurrentCheckpoint = CurrentCheckpoint.transform.parent.gameObject;
                }*/

                feedBackCheckPoint sn = CurrentCheckpoint.GetComponent<feedBackCheckPoint>();
                sn.setActiveArrows();
                
            }          
        }


        if (!callOnstart)
        {
            //Debug.Log("JE SUIS LA");
            numberOnLoopStringMoreOne = (numberOnLoop + 1).ToString();
            nextCheckpoint = GameObject.Find(numberOnLoopStringMoreOne);

            if (nextCheckpoint == null || nextCheckpoint.name == "1")
            {
                nextCheckpoint = GameObject.Find("endFigure");
            }


            if (nextCheckpoint.transform.root != transform && nextCheckpoint.transform.parent.name == nextCheckpoint.name)
            {
                nextCheckpoint = nextCheckpoint.transform.parent.gameObject;
            }
            
            feedBackCheckPoint snNext = nextCheckpoint.GetComponent<feedBackCheckPoint>();
            snNext.setActiveArrows();


            if (CurrentCheckpoint != null)
            {
                feedBackCheckPoint snCurrent = CurrentCheckpoint.GetComponent<feedBackCheckPoint>();

                if (snCurrent != null)
                {
                    snCurrent.setUnactiveArrows();
                }
            }
           
        }

    }


    void OnTriggerEnter(Collider other)
    {
        Renderer m_Renderer = other.gameObject.GetComponent<Renderer>();
        numberOnLoopString = numberOnLoop.ToString();
       


        //Debug.Log(numberOnLoopString);
        
        if (m_Renderer != null)
        {
            m_Renderer.enabled = false;
        }

        if (other.gameObject.name == numberOnLoopString)
        {
            Getcheckpoint(false);

            if (other.gameObject.tag == "small")
            {
                checkPointPassageSuccess = 1;
            }
            else if (other.gameObject.tag == "medium")
            {
                checkPointPassageSuccess = 2;

            }
            else if(other.gameObject.tag == "large")
            {
                
                checkPointPassageSuccess = 3;
            }

        }

        else if (other.gameObject.name == "endFigure")
        {
            Getcheckpoint(false);

            if (other.gameObject.tag == "small")
            {
                checkPointPassageSuccess = 5;
            }
            else if (other.gameObject.tag == "medium")
            {
                checkPointPassageSuccess = 6;

            }
            else if (other.gameObject.tag == "large")
            {
                checkPointPassageSuccess = 7;
            }

            numberOnLoop = 0;
        }

        else
        {

            checkPointPassageSuccess = 4;
            
        }

        alreadyIncrease = false;
    }

    void OnTriggerExit(Collider other)
    {
     
        if (other.gameObject.tag == "large")
        {
           // CheckpointSuccess.enabled = true;


            if (checkPointPassageSuccess == 1)
            {
                //Debug.Log("Perfect !!");
                nbrPerfect++;



            }

            else if(checkPointPassageSuccess == 2)
            {
                //Debug.Log("Good !");
                nbrGood++;
            }

            else if(checkPointPassageSuccess == 3)
            {
                //Debug.Log("Bad.");
                nbrBad++;
            }

            else if (checkPointPassageSuccess == 4)
            {
                //Debug.Log("Miss...");
                CheckpointSuccess.enabled = true;
                CheckpointSuccess.text = "Miss...";

                //////                  \\\\\\
                /////                    \\\\\       
                ////                      \\\\
                ///                        \\\
                //                          \\

                while (other.gameObject.name != numberOnLoopString)
                {
                   // Debug.Log("nom du checkpoint = " + other.gameObject.name);
                    numberOnLoop++;
                    numberOnLoopString = numberOnLoop.ToString();
                   // Debug.Log("nombre loop = " + numberOnLoop);
                    return;
                }

            }

            else if (checkPointPassageSuccess == 5)
            {
                nbrPerfect++;
                Handheld.Vibrate();

                StartCoroutine(WaitAndDisable());
                scriptScore.scoreTotalFigure(nbrPerfect, nbrGood, nbrBad);
                managerScript.allowToPlace();
                variableReset();
            }

            else if (checkPointPassageSuccess == 6)
            {
                nbrGood++;
                Handheld.Vibrate();

                StartCoroutine(WaitAndDisable());
                scriptScore.scoreTotalFigure(nbrPerfect, nbrGood, nbrBad);
                managerScript.allowToPlace();
                variableReset();
            }

            else if (checkPointPassageSuccess == 7)
            {
                nbrBad++;
                Handheld.Vibrate();

                StartCoroutine(WaitAndDisable());
                scriptScore.scoreTotalFigure(nbrPerfect, nbrGood, nbrBad);
                managerScript.allowToPlace();
                variableReset();
            }

            if (!alreadyIncrease)
            {
                numberOnLoop++;
                alreadyIncrease = true;
            }

            checkPointPassageSuccess = 0;
           


        }

        else if (other.gameObject.tag == "limiteFigure")
        {
            Handheld.Vibrate();

            StartCoroutine(WaitAndDisable());
            scriptScore.scoreTotalFigure(0, 0, 0);
            managerScript.allowToPlace();
            variableReset();
        }
        


    }


    public IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(3);
        CheckpointSuccess.enabled = false;

    }

    private void variableReset()
    {
        nbrPerfect = 0;
        nbrGood = 0;
        nbrBad = 0;
    }
}
