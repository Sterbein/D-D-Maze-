
using UnityEngine;
using UnityEngine.SceneManagement;
// This script is used to manage the pause menu in a game
public class menuPausa : MonoBehaviour
{
    // Called when the user wants to resume the game
    public void resumen()
    {
        Time.timeScale = 1f;


    }
    // Called when the user wants to restart the game
    public void reiniciar()
    {
        // Load the current scene again, which will restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Called when the user wants to go back to the main menu
    public void salirAlMenu()
    {
        // Load the previous scene in the build index, which should be the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    // Called when the user wants to quit the game
    public void salir()
    {
        Application.Quit();
    }
}
