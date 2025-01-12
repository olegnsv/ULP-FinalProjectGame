using UnityEngine;
using TMPro;

public class MainManager : MonoBehaviour
{

    [SerializeField] private TextMeshPro player1Nameplate;
    [SerializeField] private TextMeshPro player2Nameplate;

    void Start()
    {

        if (GameManager.Instance == null)
        {
            player1Nameplate.text = "Player1";
            player2Nameplate.text = "Player2";
        }
        else
        {
            player1Nameplate.text = GameManager.Instance.player1Name;
            player2Nameplate.text = GameManager.Instance.player2Name;
        }

        
    }

    void Update()
    {
        //if (!GameManager.Instance.isGameRunning)
        //{
        //    // Show game over UI
        //}
    }
}
