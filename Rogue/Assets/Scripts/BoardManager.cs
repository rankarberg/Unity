using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class NewBehaviourScript : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count (int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // Variable declarations

    // Game board dimensions
    public int columns = 8;
    public int rows = 8;

    // Random number of walls per level, between 5 and 9 walls
    public Count wallCount = new Count(5, 9);

    // Same for food items, between 1 and 5
    public Count foodCount = new Count(1, 5);

    // Declare variables to hold prefabs that we will spawn
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] enemyTiles;
    public GameObject[] foodTiles;

    // Private variables
    // This one is so we can child all of the things we spawn so it doesn't muck up the hierarchy
    private Transform boardHolder;
    // Used to track all of the different positions on the game board and whether an object has been spawned in that position
    private List<Vector3> gridPositions = new List<Vector3>();

    // Function to initialize grid positions
    void InitializeList()
    {
        // Clear em all out first
        gridPositions.Clear();

        // Now create a nested loop to populate the grid squares
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // Board setup function. Laying outer wall tiles and floor tiles.
    void BoardSetup ()
    {
        boardHolder = new GameObject ("Board").transform;

        // Building an edge around the board using the outer wall objects
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                // Check to see if we're in an outer wall position, and if so, use an outer wall tile
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                    // Assign the GameObject "instance" to what we are instantiating
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    // Set the parent of our newly instantiated game object to boardHolder
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
