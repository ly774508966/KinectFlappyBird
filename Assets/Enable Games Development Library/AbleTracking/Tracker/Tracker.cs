using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;

/// <summary>
/// MonoBehaviour that records the game data. Subscribe game behavior to the tracker by defining a message delegate. See TrackerModule.
/// </summary>
public class Tracker : MonoBehaviour {
    /// <summary>
    /// How long between each tracker 'tick'. 100 ticks in a second would have .01 seconds between updates
    /// </summary>
    [SerializeField]
    private float SecondsBetweenUpdates = 0.3f;

    /// <summary>
    /// Singleton. A prefab can be made if a person wishes otherwise the package creates the gameobject for the user and the gameobject can track with singleton calls.
    /// </summary>
    private static Tracker instance;
    public static Tracker Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject();
                Tracker i = go.AddComponent <Tracker>() as Tracker;
                i.name = "_AbleTracker(Singleton)";
                instance = i;
            }
            return instance;
        }
    }

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        tickModules = new List<TrackerModule>();
    }

    private bool track = false;
    private List<TrackerModule> tickModules; // These modules update every SecondsBetweenUpdate

    /// <summary>
    /// Tracking routine. Developer defines when to start and stop with Begin and End tracking
    /// </summary>
    /// <returns></returns>
    IEnumerator Tracking() {
        Session session = SessionCreator.Instance.CurrentSession;
        while (track) {
            yield return new WaitForSeconds(SecondsBetweenUpdates);
            foreach (TrackerModule module in this.tickModules) {
				try{
                	session.AddMessage(module.GetMessage());
				} catch {
					session.AddMessage(new TrackerMessage(module.Key, "Error in tracking module."));
				}
            }
        }
    }

    public void AddTickModule(TrackerModule module) {
        tickModules.Add(module);
    }

    /// <summary>
    /// Start tracking
    /// </summary>
    public void BeginTracking() {
        track = true;
        StartCoroutine(Tracking());
    }

    /// <summary>
    /// Stop tracking
    /// </summary>
    public void StopTracking() {
        if (track == true) {
            track = false;
            SessionCreator.Instance.CurrentSession.Save();
        }
    }

    /// <summary>
    /// Default game message.
    /// </summary>
    /// <param name="messageText">String to display next to the default message key</param>
    public void Message(string messageText) {
        SessionCreator.Instance.CurrentSession.AddMessage(new TrackerMessage("Game Message", messageText));
    }

    /// <summary>
    /// Make a game message with a specified tracker message object
    /// </summary>
    /// <param name="message">Used to specify a specific key and message</param>
    public void Message(TrackerMessage message) {
        SessionCreator.Instance.CurrentSession.AddMessage(message);
    }

    /// <summary>
    /// Make a game message with a specific key and text
    /// </summary>
    /// <param name="key">Key displayed in the tracker data</param>
    /// <param name="messageText">Text displayed in message</param>
    public void Message(string key, string messageText) {
        Message(new TrackerMessage(key, messageText));
    }

    /// <summary>
    /// Gets current timestamp, for use in formatting.
    /// </summary>
    /// <returns></returns>
    public static string CurrentTimeStamp() {
        return System.DateTime.Now.ToString("h:mm:ss");
    }

    public void OnApplicationQuit() {
        if (SessionCreator.Instance.CurrentSession != null) {
            Tracker.Instance.StopTracking();
		}
    }
}
