using UnityEngine;
using System.Collections;

public class GamePanel : MonoBehaviour {
	public static float TWEEN_TIME = .2f;

	private Vector3 upScale;


	void OnEnable () {
		this.transform.localScale = Vector3.one;
//		this.transform.DOScale(1f,TWEEN_TIME);
	}

	public void Hide () {
//		this.transform.DOScale(0f,TWEEN_TIME);
		StartCoroutine(Disable());
	}

	IEnumerator Disable () {
		yield return new WaitForSeconds(TWEEN_TIME);
		this.gameObject.SetActive(false);
	}

	public void Show () {
		this.gameObject.SetActive(true);
	}
}
