using UnityEngine;
using System.Collections;

public class HelloKinect : MonoBehaviour {

	public bool useKinect = true;
	private static bool use;
	public static bool UseKinect {
		get {
			//return KinectManager.IsKinectInitialized() && use;
			return !(Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) && use;
		}
	}
	// Use this for initialization
	void Awake () {
		use = useKinect;
	}
}
