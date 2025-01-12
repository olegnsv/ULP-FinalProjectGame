using UnityEngine;

public class GameManager : MonoBehaviour
{

    //public bool debugMode;
    public bool isGameRunning;
    public string player1Name;
    public string player2Name;

    public static GameManager Instance { get; private set; } // ENCAPSULATION

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}