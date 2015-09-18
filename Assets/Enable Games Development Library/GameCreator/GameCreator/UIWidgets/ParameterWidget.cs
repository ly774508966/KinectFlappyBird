using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ParameterWidget : MonoBehaviour {
	public delegate void ParameterUpdated(string parameterName);
	public static event ParameterUpdated OnParameterChanged;

	[SerializeField]
	private Text displayLabel;

	[SerializeField]
	[TextArea(3,10)]
	private string toolTip;

	public string ToolTip {
		get { return toolTip; }
	}

	private GameParameter parameter;
	public GameParameter Parameter {
		get { return parameter; }
		set { parameter = value; }
	}
	
	public virtual void Setup(GameParameter aParameter) {
		parameter = aParameter;
		Initialize();
	}

	void Awake() {
		GameParameters.GameParameterUpdateCheck += HandleGameParameterUpdateCheck;
	}

	protected abstract void HandleGameParameterUpdateCheck (GameParameter parameter);

	void OnDestroy() {
		GameParameters.GameParameterUpdateCheck -= HandleGameParameterUpdateCheck;
	}

	protected virtual void Initialize () {
		this.displayLabel.text = parameter.ParameterName;
	}

	public virtual void UpdateParameter(object o) {
		if(OnParameterChanged != null) {
			OnParameterChanged(displayLabel.text);
		}
	}
}
