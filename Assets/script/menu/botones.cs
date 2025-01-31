
using UnityEngine;
using UnityEngine.SceneManagement;
// This script is used to manage the buttons in a game
public class botones : MonoBehaviour
{
    // Called when the user wants to play the game 
    public void jugar()
    {
        // Load the next scene in the build index

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    // Called when the user wants to go back to the previous scene
    public void volver()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

    }

    // Called when the user wants to quit the game
    public void salir()
    {

        Application.Quit();
    }
}
