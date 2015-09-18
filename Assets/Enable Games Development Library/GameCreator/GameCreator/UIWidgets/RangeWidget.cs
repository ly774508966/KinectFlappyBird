using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RangeWidget : ParameterWidget {
	[SerializeField]
	private Slider rangeSlider;

	[SerializeField]
	private float tick;

	[SerializeField]
	private Text valueField;

	private RangeParameter rangeParameter;

	protected override void Initialize () {
		base.Initialize ();
		if(this.Parameter.GetType() != typeof(RangeParameter)) {
			throw new System.ApplicationException("Mismatch Widget and Parameter Type");
		}
		rangeParameter = (RangeParameter) this.Parameter;
		if(rangeParameter.Tick != 1f) {
			rangeSlider.wholeNumbers = false;
		}
		rangeSlider.minValue = rangeParameter.Min;
		rangeSlider.maxValue = rangeParameter.Max;
		StartCoroutine(WaitAndUpdate(rangeParameter.Value));
	}

	IEnumerator WaitAndUpdate(float newVal) { // Sketchy
		yield return null; // Wait a frame for new min and maxes before updating value
		rangeSlider.value = newVal;
		valueField.text = rangeParameter.Value.ToString();
	}

	public void SliderUpdate() {
		UpdateParameter((float)rangeSlider.value);
	}

	protected override void HandleGameParameterUpdateCheck (GameParameter parameter) {
		rangeSlider.value = rangeParameter.Value;
		valueField.text = rangeParameter.Value.ToString();
	}

	public override void UpdateParameter (object o) {
		if(o.GetType() != typeof(float)) {
			throw new System.ApplicationException("Mismatch Widget and Parameter Type");
		}
		rangeParameter.Value = (float)o;
		valueField.text = rangeParameter.Value.ToString();
		base.UpdateParameter(o);
	}
}
