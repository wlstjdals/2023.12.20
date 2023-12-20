using UnityEngine;

public class TrackManager1 : MonoBehaviour
{
    public static TrackManager1 instance { get; private set; }
    void Awake()
    {
        // Ensure there is only one instance of TrackManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: If you want the TrackManager to persist between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Additional initialization logic...
    }

    // Other methods...
}