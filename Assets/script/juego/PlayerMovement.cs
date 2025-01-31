using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float celdaSize = 1f;
    public int velocidad;
    public Vector2 targetPosition;
    public bool isMoving;
    private LaberintoPrim laberinto;
    public int piX, piY;
    public int targetX, targetY;
    public int piX2, piY2;
    public int walks;
    public bool isMyTurn = false;
    public static PlayerMovement currentPlayer;
    public List<GameObject> trampa;

    public Vector3 inicialPosition;
    public GameObject ability;

    public Dictionary<int, GameObject> playerInf = new Dictionary<int, GameObject>();
    public int abilityCooldownTurns;
    public int abilityCooldownCounter;
    public bool ConLlave = false;
    public bool win = false;
    public int vision;







    void Start()
    {
        laberinto = FindAnyObjectByType<LaberintoPrim>();
        abilityCooldownCounter = abilityCooldownTurns;
        vision = (int)gameObject.GetComponent<Light2D>().pointLightOuterRadius;

        walks = velocidad;
        int id = laberinto.totalturn % 4;

        if (laberinto != null)
        {
            // Initialize the player's position
            piX = Mathf.RoundToInt(transform.position.x);
            piY = Mathf.RoundToInt(transform.position.y);
            targetPosition = transform.position;
            inicialPosition = transform.position;
        }
        if (currentPlayer == null)
        {
            currentPlayer = this;
            isMyTurn = true;
        }
        piX2 = piX;
        piY2 = piY;



    }

    void Update()
    {


        if (isMyTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (abilityCooldownCounter <= 0) gameObject.GetComponent<AbilityOfPlayers>().UseAbility();
            }
        }
        if (abilityCooldownCounter <= 0) abilityCooldownCounter = 0;



        // Check if the maze has traps
        if (laberinto.trampas.Count > 0)
        {
            trampa = laberinto.trampas;
        }
        // Check if it's the player's turn
        if (isMyTurn)
        {
            // Check if the player is moving

            if (isMoving)
            {
                // Move the player towards the target position
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                // Check if the player has reached the target position
                if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
                {
                    // Set the player's position to the target position
                    transform.position = targetPosition;
                    isMoving = false;
                    walks--;
                }
            }
            if (walks > 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                    Move(0, 1);
                if (Input.GetKeyDown(KeyCode.S))
                    Move(0, -1);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                    Move(-1, 0);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    Move(1, 0);
                }
            }



        }
        // Check if the player has reached the key
        if (laberinto.laberintoInf[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y] == "llave")
        {
            ConLlave = true;
            Destroy(laberinto.matrixLlave[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y]);
        }
        // Check if the player has the key and is at the chest
        if (ConLlave == true && laberinto.n / 2 == (int)gameObject.transform.position.x && laberinto.n / 2 == (int)gameObject.transform.position.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
    // Method to move the player
    public void Move(int xDir, int yDir)
    {
        // Check if the player is moving or if it's not their turn
        if (isMoving || !isMyTurn) return;

        targetX = piX + xDir;
        targetY = piY + yDir;
        // Check if the target position is within the maze borders
        if (targetX >= 0 && targetX < laberinto.filas && targetY >= 0 && targetY < laberinto.columnas)
        {
            // Check if the target position is not a wall
            if (laberinto.laberintoInf[targetX, targetY] != "paredes")
            {
                float xPos = targetX;
                float yPos = targetY;


                targetPosition = new Vector2(xPos, yPos);
                isMoving = true;
                piX = targetX;
                piY = targetY;
            }
        }
        // Check if the target position is a trap
        foreach (GameObject t in trampa)
        {
            if (t != null)
            {
                // Check if the target position is the same as the trap position
                Vector3 x = targetPosition;
                if (t.transform.position == x)
                {
                    // Activate the trap
                    t.GetComponent<Trampa>().activator(gameObject);
                }
            }
        }



    }




}



