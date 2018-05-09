using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour {
	public string scene;

	public void loadScene(string Dir) {
        Level_List.setFolderName(Dir);
        SceneManager.LoadScene("Scenes/LevelsMenu");
    }
}
