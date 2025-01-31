
using UnityEngine;
// This script is used to manage the traps in a game
public class Trampa : MonoBehaviour
{

    private AbilityOfPlayers abilityOfPlayers;
    // Enum to define the type of trap
    public enum TrapType
    {
        Freeze,
        buff,
        Teleport,
        Change,
        cooldown
    }

    public TrapType trapType;

    // Called when the player activates the trap
    public void activator(GameObject player)
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        // Check if the player is not immune
        if (player.GetComponent<AbilityOfPlayers>().isImune == false)
        {
            // Switch based on the trap type
            switch (trapType)
            {
                case TrapType.Change:
                    // Change the player's turn
                    FindAnyObjectByType<TurnManager>().GetComponent<TurnManager>().ChangeTurn();
                    Debug.Log("¡Trampa de turno activada! El jugador  pasara  este turno.");
                    break;

                case TrapType.buff:
                    // Give the player a buff of walks
                    player.GetComponent<PlayerMovement>().walks += 3;
                    Debug.Log("¡buff activada! El jugador ha recibido 3 pasos");
                    break;

                case TrapType.Teleport:
                    // Return the player for the begginig
                    PlayerMovement p = player.GetComponent<PlayerMovement>();

                    player.transform.position = p.inicialPosition;
                    p.targetPosition = p.inicialPosition;
                    p.piX = p.piX2;
                    p.piY = p.piY2;
                    p.isMoving = false;
                    Debug.Log("¡Trampa de teletransporte activada! El jugador ha sido teletransportado.");
                    break;
                case TrapType.Freeze:
                    // Reduce the player walks to 0
                    player.GetComponent<PlayerMovement>().walks = 0;
                    break;
                case TrapType.cooldown:
                    // Reduce the player's cooldown
                    player.GetComponent<PlayerMovement>().abilityCooldownCounter -= 2;
                    break;

                default:
                    // Log a warning if the trap type is not recognized
                    Debug.LogWarning("Tipo de trampa no reconocido.");
                    break;
            }
            // Destroy the trap game object
            Destroy(gameObject);
        }

    }
}