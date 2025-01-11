using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIManager : MonoBehaviour
{

    [SerializeField] TMP_InputField player1NameInputField;
    [SerializeField] TMP_InputField player2NameInputField;


    public void StartGame()
    {
        if (player1NameInputField.text != null && player2NameInputField.text != null)
        {
            GameManager.Instance.player1Name = player1NameInputField.text;
            GameManager.Instance.player2Name = player2NameInputField.text;
        }
        else
        {
            GameManager.Instance.player1Name = "Player1";
            GameManager.Instance.player1Name = "Player2";
        }
        
        SceneManager.LoadScene(1);
        GameManager.Instance.isGameRunning = true;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
