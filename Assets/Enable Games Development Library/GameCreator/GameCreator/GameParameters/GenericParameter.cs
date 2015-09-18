using UnityEngine;
using System.Collections;

public class GenericParameter<T> : GameParameter {
    protected T value_;
    public T Value {
        get { return value_; }
        set { value_ = value; }
    }

    public GenericParameter(string name, T value) 
        : base(name) {
        value_ = value;
    }

	public override float NetworkValue () {
		return -1f;
	}

	public override bool AllowNetworkSync () {
		return false;
	}

	public override void AssignValue (float newVal) {

	}

    public override void Print() {
        Debug.Log(this.ParameterName + " set to: " + value_.ToString());
    }
}
