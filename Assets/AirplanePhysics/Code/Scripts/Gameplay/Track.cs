using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


namespace weloveaero
{
    public class Track : MonoBehaviour
    {
        #region Variables
        [Header("Track Properties")]
        public List<Gate> gates = new List<Gate>();

        [Header("Track Events")]
        public UnityEvent OnCompletedTrack = new UnityEvent();

        private int currentGateID = 0;
        #endregion



        #region Builtin Methods
        // Use this for initialization
        void Start()
        {
            FindGates();
            InitializeGates();

            currentGateID = 0;
            StartTrack();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            FindGates();

            if(gates.Count > 0)
            {
                for(int i = 0; i < gates.Count; i++)
                {
                    if((i+1) == gates.Count)
                    {
                        break;
                    }

                    Gizmos.color = new Color(1f, 1f, 0, 0.5f);
                    Gizmos.DrawLine(gates[i].transform.position, gates[i + 1].transform.position);
                }
            }
        }
        #endregion



        #region Custom Methods
        public void StartTrack()
        {
            if(gates.Count > 0)
            {
                gates[currentGateID].ActivateGate();
            }
        }

        void SelectNextGate()
        {
            currentGateID++;
            if(currentGateID == gates.Count)
            {
                //Debug.Log("Completed Track!");
                if(OnCompletedTrack != null)
                {
                    OnCompletedTrack.Invoke();
                }
                return;
            }

            gates[currentGateID].ActivateGate();
        }

        void FindGates()
        {
            gates.Clear();
            gates = GetComponentsInChildren<Gate>().ToList<Gate>();
        }

        void InitializeGates()
        {
            if(gates.Count > 0)
            {
                foreach(Gate gate in gates)
                {
                    gate.DeactivateGate();
                    gate.OnClearedGate.AddListener(SelectNextGate);
                }
            }
        }
        #endregion
    }
}
