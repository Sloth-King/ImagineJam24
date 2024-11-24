
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpPart : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            char lastChar = this.gameObject.name[this.gameObject.name.Length - 1];
            int number = (int)char.GetNumericValue(lastChar);
            StaticIntArray.numberOfPartsCollected += 1;

            GameManager.instance.currentPart = number;

            if (StaticIntArray.numberOfPartsCollected == 5)
            {
                SceneManager.LoadScene("EndingCutscene");
                Debug.Log("All parts collected, start ending game");
            }

            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
