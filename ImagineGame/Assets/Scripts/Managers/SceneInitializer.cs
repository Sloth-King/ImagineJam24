using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject obj in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))        {
            if (GameManager.instance != null && GameManager.instance.IsObjectDeactivated(obj.name))
            {
                obj.SetActive(false);
                Debug.Log("Deactivated object on scene load: " + obj.name);
            }
        }

        // Restore player position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && GameManager.instance != null)
        {
            player.transform.position = GameManager.instance.playerPosition;
        }
    }
}
