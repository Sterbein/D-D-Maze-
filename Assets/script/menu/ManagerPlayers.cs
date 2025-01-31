using System.Collections.Generic;
using UnityEngine;
// This script is used to manage the players in a game
public class ManagerPlayers : MonoBehaviour
{
    // Static instance of the ManagerPlayers script
    public static ManagerPlayers instance;
    // Maximum number of players allowed in the game
    public int PlayersLimit;
    // List of player characters
    public List<personajes> personajes;
    private void Awake()
    {
        // Set the instance of the ManagerPlayers script
        ManagerPlayers.instance = this;
        // Don't destroy the ManagerPlayers script when the scene changes
        DontDestroyOnLoad(this.gameObject);
        return;

    }
    // Called when the user wants to select 2 players
    public void selct2P()
    {
        // Set the players limit to 2
        PlayerPrefs.SetInt("playersLimit", 2);
        // Set the map size to 19
        PlayerPrefs.SetInt("Tamano del mapa", 19);

    }
    public void selct4P()
    {
        PlayerPrefs.SetInt("playersLimit", 4);
        PlayerPrefs.SetInt("Tamano del mapa", 31);
    }
    public void selct6P()
    {
        PlayerPrefs.SetInt("playersLimit", 6);
        PlayerPrefs.SetInt("Tamano del mapa", 51);
    }

}
