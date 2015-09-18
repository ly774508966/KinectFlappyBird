using UnityEngine;
using System.Collections;
using FullSerializer;

[System.Serializable]
public abstract class GameParameter {
    [fsProperty, SerializeField]
    private string parameterName = "Parameter Name";

    [fsProperty, SerializeField]
    private string categoryName = "Category";
    
    public string CategoryName {
        get { return categoryName; }
        set { categoryName = value; }
    }

    public string ParameterName {
        get { return parameterName; }
        set { parameterName = value; }
    }

    public GameParameter() {
        parameterName = "";
    }

    public GameParameter (string parameterName) {
        this.parameterName = parameterName;
    }

	public abstract bool AllowNetworkSync();

	public abstract void AssignValue(float newVal);

	public abstract float NetworkValue();

    public abstract void Print();
}
