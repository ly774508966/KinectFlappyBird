using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoolWidget : ParameterWidget {
	[SerializeField]
	private Toggle on;
	[SerializeField]
	private Toggle off;

	private BoolParameter boolParameter;

	public void OnChange () {
		if (on.isOn) {
			UpdateParameter(true);
		}
	}
	
	public void OffChange () {
		if(off.isOn) {
			UpdateParameter(false);
		}
	}

	protected override void HandleGameParameterUpdateCheck (GameParameter parameter) {
		if(boolParameter.Value) {
			on.isOn = true;
			off.isOn = false;
		} else {
			on.isOn = false;
			off.isOn = true;
		}
	}

	protected override void Initialize () {
		base.Initialize ();
		if(this.Parameter.GetType() != typeof(BoolParameter)) {
			throw new System.ApplicationException("Mismatch Widget and Parameter Type");
		}
		boolParameter = (BoolParameter) this.Parameter;
	}

	public override void UpdateParameter (object o) {
		if(o.GetType() != typeof(bool)) {
			throw new System.ApplicationException("Mismatch Widget and Parameter Type");
		}
		boolParameter.Value = (bool)o;
		base.UpdateParameter(o);
	}
}
