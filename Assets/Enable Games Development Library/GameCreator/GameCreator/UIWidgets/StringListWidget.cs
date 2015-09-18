using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StringListWidget : ParameterWidget {

	[SerializeField]
	private GameObject choice;
	[SerializeField]
	private ToggleGroup toggleGroup;
	[SerializeField]
	private Transform scrollRoot;

	private StringListParameter listParameter;

	void InitalizeFromStrings(List<string> strings) {
		RectTransform rootRect = (RectTransform)scrollRoot.transform;
		int i = 0;
		float xDist = 0f;
		for(i = 0; i < strings.Count; i++) {
			GameObject instantiatedChoice = (GameObject)Instantiate(choice);
			RectTransform instantiatedRect = instantiatedChoice.GetComponent<RectTransform>();
			Text t = instantiatedChoice.GetComponentInChildren<Text>();
			Toggle toggle = instantiatedChoice.GetComponentInChildren<Toggle>();

            t.text = strings[i];
            xDist += instantiatedRect.rect.width;
            instantiatedChoice.transform.SetParent(scrollRoot);
            
            // set all toggles to false initially so that onValueChanged fires for the defaults when we turn them on
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(ButtonToggled);
            // must add to group after, since "allow switch off" disabled in toggle group will force first toggle to stay on
            toggle.group = toggleGroup;
            
            // set our default
            toggle.isOn = i == 0 ? true : false;
        }
		rootRect.sizeDelta = new Vector2(xDist,rootRect.sizeDelta.y);
//		rootRect.localPosition += new Vector3(xDist / 2f, 0f);
	}

	public void ButtonToggled (bool t) {
		if(t) {
			Toggle toggle = null;
			foreach(Toggle tg in toggleGroup.ActiveToggles()) {
				toggle = tg;
			}
			UpdateParameter(toggle.GetComponentInChildren<Text>().text);
		}
	}

	protected override void HandleGameParameterUpdateCheck (GameParameter parameter) {
		if(parameter == listParameter) {
			throw new System.NotImplementedException ();
		}
	}

	protected override void Initialize () {
		base.Initialize ();
		if(this.Parameter.GetType() != typeof(StringListParameter)) {
			throw new System.ApplicationException("Mismatch Widget and Parameter Type");
		}
		this.listParameter = (StringListParameter) this.Parameter;
		this.InitalizeFromStrings(this.listParameter.Strings);
	}
	
	public override void UpdateParameter (object o) {
		if(o.GetType() != typeof(string)) {
			throw new System.ApplicationException("Mismatch Widget and Parameter Type");
		}
		listParameter.Value = (string)o;
		base.UpdateParameter(o);
	}
}
