using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Purpose of this script is to load the GameManager when the game starts.  Checks if GameManager has been instantiated,
/// and if not, will do so from our prefab
/// </summary>
/// 
public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    void Awake()
    {
        // check if game manager instance is null
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
