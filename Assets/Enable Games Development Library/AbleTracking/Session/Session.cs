using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;

public class Session {
    public static string FileExtension = ".json";
    public static string ResultFolder = "AbleSessions";
    [fsProperty]
    private string name;

    public string Name {
        get { return name; }
        set { name = value; }
    }

    [fsProperty]
    private List<TrackerMessage> messages;
	[fsIgnore]
	public List<TrackerMessage> Messages {
		get {
			return messages;
		}
	}

    private StreamWriter fs;

    [fsProperty]
    private string fn = "";

    private string Filename {
        get {
            if (fn != "") {
                return fn;
            }
            int fileNumber = 0;
            string n = this.name;
            if (n == "") {
                n = "DefaultSession";
            }
            DirectoryInfo info = new DirectoryInfo(ResultFolder);
            if (info.Exists == false) {
                info.Create();
            }
            string filename = Path.Combine(ResultFolder, n + FileExtension);
            if (File.Exists(filename)) {
                do {
                    filename = Path.Combine(ResultFolder, n + "_" + fileNumber.ToString() + FileExtension);
                    fileNumber++;
                } while (File.Exists(filename));
            }
            return filename;
        }
    }

    public Session(string sessionName) {
        name = sessionName;
        fs = new StreamWriter(Filename);
        messages = new List<TrackerMessage>();
        messages.Add(new TrackerMessage("Session Event", "Session Created"));
    }

    public Session() {
        name = "Default AbleGames Session";
        fs = new StreamWriter(Filename);
        messages = new List<TrackerMessage>();
        messages.Add(new TrackerMessage("Session Event", "Session Created"));
    }

    public void AddMessage(TrackerMessage message) {
        if (!streamAlive) {
            return;
        }
        messages.Add(message);
        fs.WriteLine(message.SerializedValue);
        fs.Flush();
    }

    private bool streamAlive = true;
    public void Save() {
        //Debug.Log(JSONString());
#if !UNITY_WEBPLAYER
        AddMessage(new TrackerMessage("Session Event", "Session Saved"));
        fs.Close();
        streamAlive = false;
        //JSONSerializer.Serialize(typeof(Session), this, Filename);
        //File.WriteAllText(filename, this.JSONString());
#endif
    }

    /// <summary>
    /// Serialize the session into JSON. A weird fullserializer issue adds backslashes when serializing nested JSON objects. The backslash gets removed here.
    /// DO NOT USE BACKSLASHES IN MESSAGES!
    /// </summary>
    /// <returns></returns>
    public string JSONString() {
       // return (JSONSerializer.Serialize(typeof(Session), this)).Replace(@"\", "");
        return JSONSerializer.Serialize(typeof(Session), this);
    }
}
