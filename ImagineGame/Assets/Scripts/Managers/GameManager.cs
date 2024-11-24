using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 playerPosition { get; set; }
    public int[] partsCollected { get; set; } = { 0, 0, 0, 0, 0 };
    public int currentPart { get; set; } = 0;   
    private HashSet<string> deactivatedObjects = new HashSet<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkObjectAsDeactivated(string objectName)
    {
        deactivatedObjects.Add(objectName);
    }

    public bool IsObjectDeactivated(string objectName)
    {
        return deactivatedObjects.Contains(objectName);
    }
}

