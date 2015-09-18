using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCreatorPanel : MonoBehaviour {
    private GameParameterCreator[] gameCreators;

    private int i = 0;

    void OnEnable() {
        gameCreators = this.GetComponentsInChildren<GameParameterCreator>();
        StartCoroutine(WaitAndActivate());
    }

    IEnumerator WaitAndActivate() {
        yield return new WaitForSeconds(GamePanel.TWEEN_TIME);
        ActivateIndex();
    }

    void ActivateIndex() {
        foreach (GameParameterCreator gc in gameCreators) {
            if (gc == gameCreators[i]) {
                gc.gameObject.SetActive(true);
            } else {
                gc.gameObject.SetActive(false);
            }
        }
        gameCreators[i].SetUp();
    }

    public void Next() {
        i++;
        if (i >= gameCreators.Length) {
            i = 0;
        }
        ActivateIndex();
    }

    public void Previous() {
        i--;
        if (i < 0) {
            i = gameCreators.Length - 1;
        }
        ActivateIndex();
    }
}
