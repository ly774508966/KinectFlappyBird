#define GAME_CREATOR
//#define ABLE_TRACKING // FOR DEBUGGING ONLY, is defined in able tracking package
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;


public enum PlayOrder {
    Horse, // player places all traps until THEY screw up in which case next player gets to place the trap
    PingPong, // players 1 - X place traps then X - 1 run through the level
    Scheme, // Players continue placing traps until they are satisfied--unlimited level building then run through
    Normal // Player 1 places, runs through level. Then player 2. etc.
}

public class GameParameters : EnableSerializableValue {
    [fsProperty]
    private string parameterName;
    [fsIgnore]
    public string Name {
        get { return parameterName; }
        set { parameterName = value; }
    }

	private Dictionary<string, List<GameParameter>> categories;
    [fsIgnore]
	public Dictionary<string, List<GameParameter>> Categories {
		get { return categories; }
	}

    public delegate void GameParameterArgument(GameParameter parameter);
    public static event GameParameterArgument GameParameterCreated;
	public static event GameParameterArgument GameParameterUpdateCheck;
	
    [fsProperty]
    private Dictionary<string, GameParameter> parameters;
	
    [fsIgnore]
    public Dictionary<string, GameParameter> Parameters {
        get { return parameters; }
        set { parameters = value; }
    }

    [fsIgnore]
	public List<GameParameter> ParameterList { // Returns a new list. This is not a modular getter
		get { return new List<GameParameter>(parameters.Values); }
	}

    public GameParameter AddParameter(GameParameter param) {
        parameters.Add(param.ParameterName, param);
        if(GameParameterCreated != null) {
			Debug.Log("Parameter created");
            GameParameterCreated(param);
        }
		return param;
    }

    public GameParameter GetParameter(string name) {
        return parameters[name];
    }

    public GameParameters() {
        Debug.Log("New Game Parameters Initialized.");
        parameters = new Dictionary<string, GameParameter>();
    }

    public GameParameters(bool loadDefault) {
        parameters = new Dictionary<string, GameParameter>();
        if (loadDefault) {
            LoadDefaultParameters();
        }
    }

    public GameParameters(string name, bool loadDefault) {
        parameters = new Dictionary<string, GameParameter>();
        if (loadDefault) {
            LoadDefaultParameters();
        }
        this.parameterName = name;
    }

    public GameParameters(string name, List<GameParameter> inspectorParameters) {
        parameters = new Dictionary<string, GameParameter>();
        foreach(GameParameter p in inspectorParameters) {
            RegisterCategory(p.CategoryName, AddParameter(p));
        }
        this.parameterName = name;
    }

	public void AnnounceUpdate(GameParameter param) {
		if(GameParameterUpdateCheck != null) {
			GameParameterUpdateCheck(param);
		}
	}

	public void RegisterCategory(string categoryName, GameParameter param) {
		if(categories == null) {
			categories = new Dictionary<string, List<GameParameter>>();
		}
		if(categories.ContainsKey(categoryName) == false) {
			categories[categoryName] = new List<GameParameter>();
		}
		categories[categoryName].Add(param);
	}

	protected virtual void LoadDefaultParameters() {
        this.parameterName = "Default Parameters";
        
        RegisterCategory("Audio", AddParameter(new RangeParameter(ParameterStrings.VOL_MUSIC, 0f, 1f, 1f, 0.1f)));
        RegisterCategory("Audio", AddParameter(new RangeParameter(ParameterStrings.VOL_SFX, 0f, 1f, 1f, 0.1f)));
    }

    public void Print() {
        foreach (GameParameter p in parameters.Values) {
            p.Print();
        }
    }

    public string Serialize() {
        return JSONSerializer.Serialize(typeof(GameParameters), this);
    }

    public object Deserialize(string json) {
        return JSONSerializer.Deserialize(typeof(GameParameters), json);
    }
}
