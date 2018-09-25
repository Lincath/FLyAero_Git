using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour {

    private GameObject plane;
    private Vector3 planePosition;
    private Quaternion planeRotation;

    public GameObject[] prefabFigure;
    public bool alreadyPlace;

    private GameObject newFigure;
    private int futureFigure;

    private FigureChoice scriptArrayFigure;

    private int numberStage;
    private int numberFigure;
    private int[,] figure;

    // Use this for initialization
    void Start () {

        numberStage = 0;
        numberFigure = 0;

        plane = GameObject.FindWithTag("plane");
        alreadyPlace = false;
        futureFigure = 0;
        scriptArrayFigure = gameObject.GetComponent<FigureChoice>();

        Debug.Log("whé");

        scriptArrayFigure.setTravelingVariable();
        figure = scriptArrayFigure.travelingFigureArray;
        //figure[scriptArrayFigure.travelingLevelFigure, i]


    }

    //LoopPrefabFigure()
    //setScript()
    // Update is called once per frame

    void Update () {

        if (!alreadyPlace)
        {
            placePlane();
        }



    }

    void placePlane()
    {
        planePosition = plane.transform.position;
        planeRotation = Quaternion.Euler(new Vector3(0, plane.transform.rotation.eulerAngles.y, 0));
        Debug.Log(planeRotation);
        

        newFigure = Instantiate(prefabFigure[figure[scriptArrayFigure.travelingLevelFigure, futureFigure]], planePosition, planeRotation);
        alreadyPlace = true;

        //Debug.Log(newFigure);
        LoopPrefabFigure scriptLoopPrefabFigure = plane.GetComponent<LoopPrefabFigure>();

        
        scriptLoopPrefabFigure.setScript();
        futureFigure++;



    }

    public void allowToPlace()
    {
        Debug.Log("working");
        StartCoroutine(alreadyPlaceToFalse());
    }

    IEnumerator alreadyPlaceToFalse()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("placer figure");
        alreadyPlace = false;

    }
}
