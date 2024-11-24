using UnityEngine;

public class SaveObject : MonoBehaviour
{
    void Start() {
        DontDestroyOnLoad(this.gameObject);

        SaveObject[] saveObjects = Object.FindObjectsByType<SaveObject>(FindObjectsSortMode.None);

        if (saveObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }

    }
}
