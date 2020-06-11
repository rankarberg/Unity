using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 3;


    void Awake()
    {
        // make sure we only have one instance of game running
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // to be sure things persist between scenes, we do this
        DontDestroyOnLoad(gameObject);

        // get and store a component reference to our BoardManager script
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        // call the SetupScene function of boardScript
        boardScript.SetupScene(level);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
