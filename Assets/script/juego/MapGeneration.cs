using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;


public class LaberintoPrim : MonoBehaviour
{
    public int n;
    public GameObject paredPrefab;
    public GameObject caminoPrefab;
    public GameObject cofrePrefab;
    public GameObject llavePrefab;
    public GameObject[] Trampas;
    public List<GameObject> trampas;
    public string[,] laberintoInf;
    public float celdaSize = 1f;
    public int totalturn = 0;

    public GameObject[,] matrixLlave;

    public int filas, columnas;

    public GameObject[,] laberinto;
    private bool[,] paredes;
    private System.Random rand = new System.Random();
    private List<(int, int, int, int)> paredesPrim = new List<(int, int, int, int)>();

    public List<GameObject> playerTag = new List<GameObject>();

    private ManagerPlayers gameManager;







    void Start()
    {
        // Get the size of the maze from the PlayerPrefs
        n = PlayerPrefs.GetInt("Tamano del mapa");
        gameManager = FindAnyObjectByType<ManagerPlayers>();
        InstanciarMaxPlayer();
        matrixLlave = new GameObject[n, n];




        columnas = n;
        filas = n;
        // Check if the maze size is odd
        if (filas % 2 == 0 || columnas % 2 == 0)
        {
            Debug.LogError("Las dimensiones deben ser impares.");
            return;
        }

        GenerarLaberinto();
        InstanciarTrampas();
        instanciarLlaves();




    }
    // Method to instantiate the players
    public void InstanciarMaxPlayer()
    {
        // Define the positions for the players
        (int x, int y)[] seeds =
        {
            (1,1),
            (1,n-2),
            (n-2,1),
            (n-2,n-2),
            (n/2,1),
            (n/2,n-2)


        };
        // Get the limit of players from the PlayerPrefs
        int limitPlayers = PlayerPrefs.GetInt("playersLimit");
        // Instantiate the players
        for (int i = 0; i < limitPlayers; i++)
        {
            // Get the player prefab from the game manager
            int player = PlayerPrefs.GetInt($"role-{i}");
            personajes playerPrefab = FindAnyObjectByType<ManagerPlayers>().personajes[player];

            GameObject player_ = Instantiate(playerPrefab.players, new Vector2(seeds[i].x, seeds[i].y), quaternion.identity);
            player_.transform.parent = transform;
            // Add the player object to the playerTag list
            playerTag.Add(player_);


        }
    }

    // Method to generate the maze

    public void GenerarLaberinto()
    {
        laberinto = new GameObject[filas, columnas];
        laberintoInf = new string[filas, columnas];
        paredes = new bool[filas, columnas];
        // Define the starting position of the maze
        int x = n / 2;
        int y = n / 2;
        // Create the starting cell of the maze
        CrearCelda(x, y, caminoPrefab);
        // Set the laberintoInf value for the starting cell
        laberintoInf[x, y] = "camino";
        // Add the starting cell to the paredesPrim list
        AgregarParedesPrim(x, y);
        // loop until the paredes list is empty
        while (paredesPrim.Count > 0)
        {
            int indice = rand.Next(paredesPrim.Count);
            // Get the wall position from the paredesPrim list
            (int px, int py, int cx, int cy) = paredesPrim[indice];
            // Remove the wall position from the paredesPrim list
            paredesPrim.RemoveAt(indice);
            // Check if the cell at the wall position is not a wall

            if (laberinto[cx, cy] == null)
            {
                CrearCelda(px, py, caminoPrefab);
                laberintoInf[px, py] = "camino";
                CrearCelda(cx, cy, caminoPrefab);
                laberintoInf[cx, cy] = "camino";
                AgregarParedesPrim(cx, cy);


            }



        }
        // Create the walls of the maze
        CrearParedes();

    }
    // Method to add a wall position to the paredes list
    void AgregarParedesPrim(int x, int y)
    {
        // Check if the cell at the position is not a wall and Add the wall position to the paredes list
        if (x > 1 && laberinto[x - 2, y] == null) paredesPrim.Add((x - 1, y, x - 2, y));
        if (x < filas - 2 && laberinto[x + 2, y] == null) paredesPrim.Add((x + 1, y, x + 2, y));
        if (y > 1 && laberinto[x, y - 2] == null) paredesPrim.Add((x, y - 1, x, y - 2));
        if (y < columnas - 2 && laberinto[x, y + 2] == null) paredesPrim.Add((x, y + 1, x, y + 2));
    }
    // Method to create a cell 
    void CrearCelda(int x, int y, GameObject prefab)
    {
        Vector2 posicion = new Vector2(x, y);
        GameObject celda = Instantiate(prefab, posicion, Quaternion.identity);
        celda.transform.parent = transform;
        laberinto[x, y] = celda;
    }

    // Method to create the walls of the maze
    void CrearParedes()
    {
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                // Check if the cell at the position (i, j) is not a wall
                if (laberinto[i, j] == null)
                {
                    Vector2 posicion = new Vector2(i, j);
                    Instantiate(paredPrefab, posicion, Quaternion.identity, transform);
                    // Set the laberintoInf value for the wall
                    laberintoInf[i, j] = "paredes";
                }
            }
        }
    }
    // Method to instantiate the traps and chest
    public void InstanciarTrampas()
    {
        // Define the position of the treasure chest
        Vector2 win = new Vector2(n / 2, n / 2);
        Instantiate(cofrePrefab, win, quaternion.identity);
        laberintoInf[n - 2, n / 2] = "cofre";
        System.Random rand = new System.Random();
        System.Random raand = new System.Random();
        int m = raand.Next(0, n / 2);
        int b = rand.Next(0, Trampas.Length);
        // Define the positions of the players
        List<(int, int)> posicionesJugadores = new List<(int, int)>
        {
            (1,1),
            (1,n-2),
            (n-2,1),
            (n-2,n-2),
            (n/2,1),
            (n/2,n-2)
        };
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {

                m = raand.Next(0, 30);
                b = rand.Next(0, Trampas.Length);

                Vector2 posicion = new Vector2(i, j);
                // check if the positions is occupied by players
                if (posicionesJugadores.Contains((i, j))) continue;
                // Check if the current cell is not a wall

                if (laberintoInf[i, j] != "paredes")
                {
                    // Randomly decide to place a trap
                    if (m == 4)
                    {
                        trampas.Add(Instantiate(Trampas[b], posicion, Quaternion.identity));
                        laberintoInf[i, j] = $"trampas-{b}";
                    }
                }
            }
        }
    }
    // Method to instantiate keys in the maze
    public void instanciarLlaves()
    {
        // Get the limit of players from PlayerPrefs
        int limitPlayers = PlayerPrefs.GetInt("playersLimit");
        System.Random rand = new System.Random();
        int x;
        int y;
        for (int i = 0; i < limitPlayers; i++)
        {
            // Randomly select a position for the key
            x = rand.Next(0, n - 2);
            y = rand.Next(0, n - 2);
            // Ensure the position is valid not a wall, trap, or already occupied by a key
            while (laberintoInf[x, y] == "paredes" || laberintoInf[x, y].Split('-')[0] == "trampas" || laberintoInf[x, y] == "cofre" || laberintoInf[x, y] == "llave")
            {
                x = rand.Next(0, n - 2);
                y = rand.Next(0, n - 2);

            }
            Vector2 win = new Vector2(x, y);
            // Instantiate the key at the selected position
            matrixLlave[x, y] = Instantiate(llavePrefab, win, quaternion.identity);
            laberintoInf[x, y] = "llave";
        }


    }

}