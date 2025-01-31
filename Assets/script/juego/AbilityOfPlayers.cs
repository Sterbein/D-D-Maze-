
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AbilityOfPlayers : MonoBehaviour
{
    public List<GameObject> player;
    public LaberintoPrim laberinto;
    public TurnManager turnManagers;
    public PlayerMovement p;
    public int indes;
    // Enum for player abilities
    public enum PlayerAbility

    {

        swich,
        clarity,
        inmune,
        moreWalks,
        random,
        MoreCooldown

    }

    public PlayerAbility playerAbility;
    public LayerMask obstacleLayer;
    public bool isImune;



    void Start()
    {
        laberinto = FindAnyObjectByType<LaberintoPrim>();
        turnManagers = FindAnyObjectByType<TurnManager>();
        p = FindAnyObjectByType<PlayerMovement>();
        player = laberinto.playerTag;
        indes = turnManagers.index;


    }


    // Method to use the player's ability
    public void UseAbility()
    {
        gameObject.GetComponent<PlayerMovement>().abilityCooldownCounter = gameObject.GetComponent<PlayerMovement>().abilityCooldownTurns;
        // Switch statement to handle different player abilities
        switch (playerAbility)
        {
            // Call the swich method to switch with another player
            case PlayerAbility.swich:
                swich();
                Debug.Log($"entro a swich player{indes} ");
                break;
            // Call the Clarity method to increase vision range
            case PlayerAbility.clarity:
                Clarity();
                Debug.Log($"entro a clarity player {indes} ");
                break;
            // Call the Inmune method to become immune to traps
            case PlayerAbility.inmune:
                Inmune();
                Debug.Log($"entro a inmune el player{indes} ");
                break;
            // Call the moreWalks method to increase number of walks
            case PlayerAbility.moreWalks:
                moreWalks();
                Debug.Log($"entro a moreWalks player{indes} ");
                break;
            // Call the random method to randomize diferents status of players 
            case PlayerAbility.random:
                Debug.Log($"entro a random player{indes} ");
                random();
                break;
            // Call the MoreCooldown method to increase cooldown time of a random player 
            case PlayerAbility.MoreCooldown:
                MoreCooldown();
                Debug.Log($"entro a morecooldown player{indes} ");
                break;

            default:
                Debug.Log("No ability assigned.");
                break;

        }

    }

    void swich()
    {
        // Randomly select another player
        System.Random raand = new System.Random();
        int m = raand.Next(1, player.Count);
        // Swap positions with the selected player
        Vector2 position = new Vector2(player[indes].transform.position.x, player[indes].transform.position.y);
        player[indes].transform.position = player[m].transform.position;
        player[m].transform.position = position;
        // Swap target positions with the selected player
        (player[m].GetComponent<PlayerMovement>().targetX, player[indes].GetComponent<PlayerMovement>().targetX) = (player[indes].GetComponent<PlayerMovement>().targetX, player[m].GetComponent<PlayerMovement>().targetX);
        (player[m].GetComponent<PlayerMovement>().targetY, player[indes].GetComponent<PlayerMovement>().targetY) = (player[indes].GetComponent<PlayerMovement>().targetY, player[m].GetComponent<PlayerMovement>().targetY);
        (player[m].GetComponent<PlayerMovement>().piX, player[indes].GetComponent<PlayerMovement>().piX) = (player[indes].GetComponent<PlayerMovement>().piX, player[m].GetComponent<PlayerMovement>().piX);
        (player[m].GetComponent<PlayerMovement>().piY, player[indes].GetComponent<PlayerMovement>().piY) = (player[indes].GetComponent<PlayerMovement>().piY, player[m].GetComponent<PlayerMovement>().piY);






    }

    void Clarity()
    {
        gameObject.GetComponent<Light2D>().pointLightOuterRadius = 4;


    }

    public void Inmune()
    {

        isImune = true;



    }
    void moreWalks()
    {
        gameObject.GetComponent<PlayerMovement>().walks += 4;

    }
    void random()
    {
        // Randomly select a new position for the player
        int limitPlayers = PlayerPrefs.GetInt("playersLimit");
        System.Random rand = new System.Random();
        int index = rand.Next(1, 3);
        int m = rand.Next(0, limitPlayers);
        // Check if the selected position is not the current player's position
        while (true)
        {
            m = rand.Next(0, limitPlayers);
            if (m != FindAnyObjectByType<TurnManager>().index)
            {
                // If the index is 1, increase the ability cooldown counter of the selected player
                if (index == 1)
                {
                    FindAnyObjectByType<LaberintoPrim>().playerTag[m].GetComponent<PlayerMovement>().abilityCooldownCounter += 2;

                    break;
                }
                // If the index is 2, decrease the vision range of the selected player
                else if (index == 2)
                {
                    FindAnyObjectByType<LaberintoPrim>().playerTag[m].GetComponent<Light2D>().pointLightOuterRadius = 1;
                    break;
                }
                break;
            }
        }

    }
    void MoreCooldown()
    {

        int limitPlayers = PlayerPrefs.GetInt("playersLimit");
        System.Random rand = new System.Random();
        int m = rand.Next(0, limitPlayers);
        // Check if the selected position is not the current player's position
        while (true)
        {
            m = rand.Next(0, limitPlayers );
            if (m != FindAnyObjectByType<TurnManager>().index)
            {
                // Increase the ability cooldown counter of the selected player
                FindAnyObjectByType<LaberintoPrim>().playerTag[m].GetComponent<PlayerMovement>().abilityCooldownCounter += 2;
                break;
            }
        }
    }
}



