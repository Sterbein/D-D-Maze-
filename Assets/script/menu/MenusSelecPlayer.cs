using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// This script is used to manage the selection of players in a game
public class MenusSelecPlayer : MonoBehaviour
{
    // Index of the current playe
    private int index;
    // Reference to the wallpaper image
    [SerializeField] private Image wallpaper;
    // Reference to the player name text
    [SerializeField] private TextMeshProUGUI names;
    // Reference to the ManagerPlayers script
    private ManagerPlayers managerPlayers;
    // List of selected players
    List<int> selectPlayers;
    // Current player ID
    int id = 0;

    private void Start()
    {
        selectPlayers = new List<int>();
        managerPlayers = ManagerPlayers.instance;
        index = 0;
        // Update the player display
        changePlayer();
    }
    // Update the player display based on the current index
    private void changePlayer()
    {

        PlayerPrefs.SetInt("PlayerIndex", index);
        wallpaper.sprite = managerPlayers.personajes[index].wallpaper;
        names.text = managerPlayers.personajes[index].names;
    }
    // Called when the user wants to select the next player
    public void nextPlayer()
    {

        index++;
        // Check if the index exceeds the number of players
        if (index > managerPlayers.personajes.Count - 1) index = 0;

        // Check if the current player is already selected
        bool finded = false;

        foreach (int item in selectPlayers)
        {
            if (item == index) finded = true;
        }
        // If the player is already selected, call this method recursively
        if (finded)
        {

            nextPlayer();
            return;
        }
        // Update the player display
        changePlayer();



    }
    // Called when the user wants to select the previous player
    public void PreviusPlayer()
    {
        index--;
        if (index < 0) index = managerPlayers.personajes.Count - 1;
        bool finded = false;

        foreach (int item in selectPlayers)
        {
            if (item == index) finded = true;
        }
        if (finded)
        {

            PreviusPlayer();
            return;
        }
        changePlayer();


    }
    // Called when the user wants to start the game
    public void startGame()
    {
        // Load the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void seleccionar()
    {
        // Get the maximum number of players from the player preferences
        int maxplayer = PlayerPrefs.GetInt("playersLimit");
        // Set the role of the current player to the current index
        PlayerPrefs.SetInt($"role-{id}", index);
        id++;
        selectPlayers.Add(index);
        // Check if the number of selected players reaches the maximum

        if (id >= maxplayer)
        {
            startGame();
            return;
        }
        changePlayer();
        nextPlayer();
    }


}
