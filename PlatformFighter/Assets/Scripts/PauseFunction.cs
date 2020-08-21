using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseFunction : MonoBehaviour
{
    bool gameIsPaused = false;
    [SerializeField] GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePause();
            pausePanel.active = gameIsPaused;
        }
    }

    public void togglePause()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public bool isGamePaused()
    {
        return gameIsPaused;
    }
}
