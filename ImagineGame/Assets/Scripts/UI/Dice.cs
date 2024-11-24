using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour { // https://www.youtube.com/watch?v=JgbJZdXDNtg code from here

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;
    private int finalSide;

    public int GetFinalSide()
    {
        return finalSide;
    }

	private void Awake () {

        rend = GetComponent<SpriteRenderer>();
        if(rend == null)
        {
            Debug.LogError("Sprite Renderer component not found attached to the dice.");
        }

        diceSides = Resources.LoadAll<Sprite>("DiceSides/");

        if (diceSides.Length == 0)
        {
            Debug.LogError("diceSides array is empty! Assign the sprites in the Inspector.");
        }

	}
	
    // If you left click over the dice then RollTheDice coroutine is started
    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    public IEnumerator RollTheDice()
    {
        if (rend == null || diceSides == null || diceSides.Length == 0)
        {
            Debug.LogError("Dice setup is incomplete. Ensure rend and diceSides are assigned.");
            yield break;
        }
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Final side or value that dice reads in the end of coroutine
        finalSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.07f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        finalSide = randomDiceSide + 1;

        // Show final dice value in Console
        //Debug.Log(finalSide);
    }
}
