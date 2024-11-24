using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartCombat : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            GameManager.instance.playerPosition = other.transform.position;
            SceneManager.LoadScene(sceneName);
        }
    }
}
