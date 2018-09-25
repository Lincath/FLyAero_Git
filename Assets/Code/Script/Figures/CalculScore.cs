using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculScore : MonoBehaviour {

    private float perfectScore;
    private float goodScore;
    private float badScore;

    private float actualScore;
    private static float scoreTotal;

    private float pourcentGood;
    private float pourcentBad;

    private float nbrCheckpoint;

    private float scoreFigure;

    private GameObject plane;
    private LoopPrefabFigure scriptPrefab;


    // Use this for initialization
    void Start () {

        scoreFigure = 100;
        pourcentGood = 50;
        pourcentBad = 25;


        plane =  GameObject.FindWithTag("plane");
        scriptPrefab = plane.GetComponent<LoopPrefabFigure>();
        


        if (gameObject.transform.name == "loopCollider(Clone)")
        {
            nbrCheckpoint = 12;
           
        }

        else if (gameObject.transform.name == "slopeCollider(Clone)")
        {
            nbrCheckpoint = 5;

        }
        else
        {
            Debug.Log("error");
        }
    
        perfectScore = scoreFigure/ nbrCheckpoint;
        goodScore = perfectScore * (pourcentGood / 100);
        badScore = perfectScore * (pourcentBad / 100);

       /* Debug.Log(perfectScore);
        Debug.Log(goodScore);
        Debug.Log(badScore);*/
    }



	
    public void scoreTotalFigure (int nbrPerfect, int nbrGood, int nbrBad)
    {
       /* Debug.Log("Perfect = " + nbrPerfect);
        Debug.Log("Good = " + nbrGood);
        Debug.Log("Bad = " + nbrBad);*/

        if (nbrPerfect + nbrGood + nbrBad <= nbrCheckpoint)
        {
            //Debug.Log("not an error ! ");

            actualScore = (perfectScore * nbrPerfect) + (goodScore * nbrGood) + (badScore * nbrBad);
            //scriptPrefab.scoreTxt.enabled = true;
            scriptPrefab.CheckpointSuccess.enabled = true;

            
            if (actualScore == 100)
            {
                scriptPrefab.CheckpointSuccess.color = Color.yellow;
                scriptPrefab.CheckpointSuccess.text = "PERFECT Figure !";
            }

            else if (actualScore == 0)
            {
                scriptPrefab.CheckpointSuccess.color = Color.red;
                scriptPrefab.CheckpointSuccess.text = "FIGURE FAILED !";
            }
            else
            {
                scriptPrefab.CheckpointSuccess.color = Color.black;
                scriptPrefab.CheckpointSuccess.text = "Figure finished";
            }

            scoreTotal += actualScore;
            scriptPrefab.scoreTxt.text = scoreTotal.ToString();

            //Debug.Log(actualScore);
            Destroy(gameObject);
            
        }

        else
        {
            Debug.Log("error");
            actualScore = scoreFigure / 2;

        }

        Destroy(gameObject);
    }
    
}
