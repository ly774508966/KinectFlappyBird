using UnityEngine;
using System.Collections;
using FullSerializer;

[System.Serializable]
public class RangeParameter : GameParameter {
	[fsProperty,SerializeField]
    private float minValue = 0f;
	[fsIgnore]
    public float Min {
        get { return minValue; }
        set { minValue = value; }
    }
	[fsProperty,SerializeField]
    private float maxValue = 1f;
	[fsIgnore]
    public float Max {
        get { return maxValue; }
        set { maxValue = value; }
    }
	[fsProperty, SerializeField]
    private float defaultValue = 0f;
	[fsIgnore]
    public float Value {
        get { return defaultValue; }
        set { 
			defaultValue = Mathf.Round(value * RoundFactor) / RoundFactor;
		}
    }

    [fsProperty, SerializeField]
    private float tick = 1f; // degree to round to
	[fsIgnore]
	public float Tick {
		get { return tick; }
		set { tick = value; }
	}
	[fsIgnore]
	public float RoundFactor {
		get { return 1f/ tick; }
	}

	public void LoadDefaultValue () {
		this.defaultValue  = (this.maxValue - this.minValue) / 2 + this.minValue;
	}

	public RangeParameter(string name, float min, float max, float startValue) : this(name, min, max, startValue, 1f) {}
	
	public RangeParameter(string name, float min, float max, float startValue, float tick) : base(name) {
		minValue = min;
		maxValue = max;
		this.tick = tick;
		Value = startValue;
	}

	public override bool AllowNetworkSync () {
		return true;
	}

	public override float NetworkValue () {
		return Value;
	}

	public override void AssignValue (float newVal) {
		Value = newVal;
	}

    public override void Print() {
        Debug.Log(this.ParameterName + " set to: " + defaultValue.ToString());
    }
}