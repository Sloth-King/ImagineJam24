using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 playerPosition { get; set; }
    public int[] partsCollected { get; set; } = { 0, 0, 0, 0, 0 };
    public int currentPart { get; set; } = 0;

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
}

