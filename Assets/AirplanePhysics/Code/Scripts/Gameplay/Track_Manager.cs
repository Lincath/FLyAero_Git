using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;


namespace weloveaero
{
    public class Track_Manager : MonoBehaviour
    {
        #region Variables
        [Header("Manager Properties")]
        public List<Track> tracks = new List<Track>();
        public Airplane_Controller airplaneController;

        [Header("Manager Events")]
        public UnityEvent OnCompletedRace = new UnityEvent();
        #endregion



        #region Builtin Methods
        // Use this for initialization
        private void Start()
        {
            FindTracks();
            InitializeTracks();

            StartTrack(0);
        }

        private void Update()
        {

        }
        #endregion



        #region Custom Methods
        public void StartTrack(int trackID)
        {
            if (trackID >= 0 && trackID < tracks.Count)
            {
                for(int i = 0; i < tracks.Count; i++)
                {
                    if(i != trackID)
                    {
                        tracks[i].gameObject.SetActive(false);
                    }

                    tracks[trackID].gameObject.SetActive(true);
                    tracks[trackID].StartTrack();
                }
            }
        }

        void FindTracks()
        {
            tracks.Clear();
            tracks = GetComponentsInChildren<Track>(true).ToList<Track>();
        }

        void InitializeTracks()
        {
            if(tracks.Count > 0)
            {
                foreach(Track track in tracks)
                {
                    track.OnCompletedTrack.AddListener(CompletedTrack);
                }
            }
        }

        void CompletedTrack()
        {
            Debug.Log("Completed Track!");

            if(airplaneController)
            {
                StartCoroutine("WaitForLanding");
            }
        }

        IEnumerator WaitForLanding()
        {
            while(airplaneController.State != AirplaneState.LANDED)
            {
                yield return null;
            }

            Debug.Log("Completed Race!");

            //Tutorial terminé
            GameObject.Find("StatutRace").GetComponent<Airplane_Stage>().EtatRace = " Bravo Stage   Réussit ";


            if (OnCompletedRace != null)
            {
                OnCompletedRace.Invoke();
            }
        }
        #endregion
    }
}
