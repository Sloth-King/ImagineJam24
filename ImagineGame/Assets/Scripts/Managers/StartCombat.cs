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
            string objectName = this.gameObject.transform.parent.gameObject.name;
            this.gameObject.transform.parent.gameObject.SetActive(false);
            GameManager.instance.MarkObjectAsDeactivated(objectName);
            GameManager.instance.playerPosition = other.transform.position;
            Debug.Log("Back in battle : " + this.gameObject.transform.parent.gameObject.name);
            SceneManager.LoadScene(sceneName);
        }
    }
}
