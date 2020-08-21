using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public Animator animator;
    public TextMeshProUGUI winnerText;
    public BackgroundMusicManager music;
    public static int winner = -1;

    private void Start()
    {
        if(winnerText != null)
        {
            switch (winner)
            {
                case -1:
                    winnerText.text = "This should not happen";
                    break;
                case 1:
                    winnerText.text = "Player 1 Wins!";
                    break;
                case 2:
                    winnerText.text = "Player 2 Wins!";
                    break;
            }
        }
    }

    public void loadScene()
    {

        StartCoroutine(loadNextLevel());
    }


    IEnumerator loadNextLevel()
    {
        if(music != null)
        {
            music.startFade();
        }
        animator.SetTrigger("End");

        yield return new WaitForSeconds(1); // wait for 1 sceond

        SceneManager.LoadScene(sceneName);

    }
}
