using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public static bool paused;
    public GameObject pausePanel;
    private Button btn;

    void Start () {
        paused = false;
        pausePanel.SetActive(false);

        btn = GetComponent<Button>();
        btn.onClick.AddListener(pause);
    }
	
	void Update () {
        if (paused)
            Time.timeScale = 0;
        else if (!paused)
            Time.timeScale = 1;

        pausePanel.SetActive(paused);
    }

    public void pause()
    {
        paused = !paused;
    }

    public void resume()
    {
        paused = false;
        pausePanel.SetActive(paused);
    }

    public void backtomenu()
    {
        paused = false;
        SceneManager.LoadScene("Scenes/PlayMenu");
    }
}
