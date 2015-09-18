using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StringListParameter : GameParameter {
    [FullSerializer.fsProperty,SerializeField]
    private List<string> strings;
	[FullSerializer.fsIgnore]
	public List<string> Strings {
		get { return strings; }
	}
	[FullSerializer.fsProperty,SerializeField]
    private string defaultValue = "";
    [FullSerializer.fsIgnore]
	public string Value {
		get { return defaultValue; }
		set { defaultValue = value; }
	}

	public override void Print () {
		Debug.Log(this.ParameterName + " set to: " + defaultValue.ToString());
	}

	public override bool AllowNetworkSync () {
		return false;
	}

	public override void AssignValue (float newVal) {

	}

	public override float NetworkValue ()
	{
		return -1f;
	}

	public StringListParameter(string name, params string [] strings) : base(name) {
		this.strings = new List<string>();
		foreach(string s in strings) {
			this.strings.Add(s);
		}
		defaultValue = strings[0];
	}
}
