using UnityEngine;
using System.Collections;
using FullSerializer;

public class ParameterTracker : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Tracker.Instance.AddTickModule(new TrackerModule("Game Parameters", ParameterTick));	    
	}

    EnableSerializableValue ParameterTick() {
        return ParameterHandler.Instance.AllParameters[0];
    }
}
