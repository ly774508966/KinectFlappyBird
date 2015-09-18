using UnityEngine;
using System.Collections;
using FullSerializer;

[System.Serializable]
public class BoolParameter : GameParameter {
	[fsProperty, SerializeField]
    private bool defaultValue = true;
	[fsIgnore]
    public bool Value {
        get { return defaultValue; }
        set { defaultValue = value; }
    }

    public BoolParameter(string name, bool value) 
        : base(name) {
        defaultValue = value;
    }
	public override void AssignValue (float newVal) {
		if(newVal == 1f) {
			Value = true;
		} else {
			Value = false;
		}
	}

	public override bool AllowNetworkSync () {
		return true;
	}

	public override float NetworkValue () {
		return defaultValue ? 1f : 0f;
	}

    public override void Print() {
        Debug.Log(this.ParameterName + " set to: " + defaultValue.ToString());
    }
}
