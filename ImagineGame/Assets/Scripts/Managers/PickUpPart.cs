
using UnityEngine;

public class PickUpPart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            char lastChar = this.gameObject.name[this.gameObject.name.Length - 1];
            int number = (int)char.GetNumericValue(lastChar);

            Debug.Log("pickup object number "+ number);
            Debug.Log("Avant :"+ StaticIntArray.partsCollected[number]);
            // tableau de int [number] == 1
            // StaticIntArray.partsCollected[number] = 1;
            // Debug.Log("pickup object number "+ number + " collected");
            // Debug.Log("Avant :" + StaticIntArray.partsCollected[number]);
            this.gameObject.SetActive(false);
        }
    }
}
