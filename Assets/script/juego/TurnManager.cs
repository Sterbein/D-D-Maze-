
using UnityEngine;
using UnityEngine.Rendering.Universal;

// This script is used to manage the turns in a game
public class TurnManager : MonoBehaviour
{
    // Index of the current player
    public int index = 0;
    // Reference to the menu game object
    public GameObject menu;
    // Reference to the puntos game object
    public GameObject puntos;



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) ChangeTurn();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Set the time scale to 0, which pauses the game
            Time.timeScale = 0f;
            // Activate the menu game object
            menu.SetActive(true);
            // Deactivate the puntos game object
            puntos.SetActive(false);

        }
    }
    // Called when the turn needs to be changed
    public void ChangeTurn()
    {
        // Get the current player's ability cooldown counter
        FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<PlayerMovement>().abilityCooldownCounter--;
        // Get the current player's light component
        FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<Light2D>().pointLightOuterRadius = 3;
        // Get the current player's ability component
        FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<AbilityOfPlayers>().isImune = false;
        // Get the current player's movement component
        FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<PlayerMovement>().isMyTurn = false;
        index = (index + 1) % (FindAnyObjectByType<LaberintoPrim>().playerTag.Count);
        // Get the next player's movement component
        FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<PlayerMovement>().isMyTurn = true;
        // Get the next player's movement component
        FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<PlayerMovement>().walks = FindAnyObjectByType<LaberintoPrim>().playerTag[index].GetComponent<PlayerMovement>().velocidad;


    }
}
