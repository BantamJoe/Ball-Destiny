using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    public static string title;
    public static string message;

    public Text _title;
    public Text _message;

    public Button nextlevelbtn;

    private string currentSceneNum;
    private string folderName;

    void Start (){

        title = "";
        message = "";
        _title.text = "";
        _message.text = "";

        // Get the folder Name of current scene
        folderName = SceneManager.GetActiveScene().path;
        folderName = folderName.Replace("Assets/Scenes/", string.Empty);
        folderName = folderName.Replace("/" + SceneManager.GetActiveScene().name + ".unity", string.Empty);

        // Get the current LV Number
        currentSceneNum = SceneManager.GetActiveScene().name;
        currentSceneNum = currentSceneNum.Replace("LV", string.Empty);

        if(int.Parse(currentSceneNum) >= GetNumOfScene())
            nextlevelbtn.GetComponent<Image>().gameObject.SetActive(false);     // Disable the next level button if current LV is the last one
    }

    void Update ()
    {
        _title.text = title;
        _message.text = message;
    }

    public void nextlevel()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Scenes/" + folderName + "/LV" + (int.Parse(currentSceneNum) + 1));
    }

    public void backtomenu(){
        SceneManager.LoadScene("Scenes/PlayMenu");
    }

    int GetNumOfScene()
    {
        int count = 0;
        Debug.Log(folderName);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++){
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            string path = SceneManager.GetActiveScene().path;
            if (scenePath.Contains(folderName))
                count++;
        }
        return count;
    }
}
