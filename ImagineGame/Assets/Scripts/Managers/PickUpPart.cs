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

            // tableau de int [number] == 1

            this.gameObject.SetActive(false);
        }
    }
}
