using UnityEngine;

public class HealthBarPositioner : MonoBehaviour
{
    public Transform characterTransform; // Reference to the character
    //public Vector3 offset = new Vector3(0,1,0); // Offset for health bar position
    public Vector3 offset;

    private void Update()
    {
        offset = new Vector3(0, 1, 0);
        if (characterTransform != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(characterTransform.position + offset);
            transform.position = screenPosition;
        }
    }
}
