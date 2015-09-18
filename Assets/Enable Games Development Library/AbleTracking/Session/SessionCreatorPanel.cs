using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI Element. Default for generic/quick setup.
/// </summary>
public class SessionCreatorPanel : MonoBehaviour {
    [SerializeField]
    private InputField textField;

    public void Confirm() {
        SessionCreator.Instance.CreateSession(textField.text);
    }
}
