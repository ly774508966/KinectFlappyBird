using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameParameterCreator : MonoBehaviour {
    [SerializeField]
    private string parameterKey;
    [SerializeField]
    private Text title;
    [SerializeField]
    private GameObject BoolWidgetPrefab;
    [SerializeField]
    private GameObject RangeWidgetPrefab;
    [SerializeField]
    private GameObject StringListPrefab;
    [SerializeField]
    private GameObject CategoryPrefab;
    [SerializeField]
    private GameObject scrollViewRoot;

    private int parameterCount = 0;
    private List<ParameterWidget> Widgets;

    private bool setUpComplete = false;
    private GameParameters paramInst;

    // Use this for initialization
    public void SetUp() {
        if (title != null) {
            title.text = parameterKey;
        }
        if (setUpComplete == true) {
            return;
        }
        print("Adding parameter");
        ParameterHandler.Instance.AddParameters(parameterKey);
        paramInst = ParameterHandler.Instance.GetParameters(parameterKey);
        Widgets = new List<ParameterWidget>();
        if(paramInst.Categories == null) {
            print("Parameters not set up");
            return;
        }
        foreach (string category in paramInst.Categories.Keys) {
            AddCategory(category);
            foreach (GameParameter p in paramInst.Categories[category]) {
                SetupWidget(p);
            }
        }
        setUpComplete = true;
    }

    void AddCategory(string name) {
        GameObject categoryGO = (GameObject)Instantiate(CategoryPrefab);
        Text text = categoryGO.GetComponentInChildren<Text>();
        text.text = name;
        categoryGO.transform.SetParent(scrollViewRoot.transform);
        categoryGO.transform.localScale = Vector3.one;
        parameterCount++;
    }

    void SetupWidget(GameParameter parameter) {
        ParameterWidget widget = null;
        GameObject toAdd = RangeWidgetPrefab;
        if (parameter.GetType() == typeof(BoolParameter)) {
            toAdd = BoolWidgetPrefab;
        }
        if (parameter.GetType() == typeof(StringListParameter)) {
            toAdd = StringListPrefab;
        }
        widget = ((GameObject)Instantiate(toAdd)).GetComponent<ParameterWidget>();
        Widgets.Add(widget);
        widget.Setup(parameter);
        PositionWidget(widget);
    }

    void PositionWidget(ParameterWidget widget) {
        if (widget == null) {
            return;
        }
        widget.transform.SetParent(scrollViewRoot.transform);
        widget.transform.localScale = Vector3.one;
       // widget.GetComponent<LayoutElement>().minWidth = vertWidth * .8f;
    }

    public void LoadGame() {
        paramInst.Print();
        Application.LoadLevel("game");
    }
}
