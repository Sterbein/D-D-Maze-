using TMPro;
using UnityEngine;

public class puntos : MonoBehaviour
{
    // Reference to the text mesh in the scene
    public TMP_Text textMesh;
     // References to the maze and turn manager
    public LaberintoPrim maze ;
    public TurnManager turnManager;
    
    void Update()
    {
    
       textMesh.text = $" pasos restantes: {maze.playerTag[turnManager.index].GetComponent<PlayerMovement>().walks}           turnos restantes para usar habilidad: {maze.playerTag[turnManager.index].GetComponent<PlayerMovement>().abilityCooldownCounter}  ";
        
    }
}
