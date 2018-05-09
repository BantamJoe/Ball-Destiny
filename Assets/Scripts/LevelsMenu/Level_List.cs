using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_List : MonoBehaviour {
    public int numberOfLevels;
    public GameObject Level_Btn;
    public Transform Panel;
    public static string FolderName;

    // Use this for initialization
    void Start ()
    {
        for (int i = 1; i < GetNumOfScene()+ 1; i++){
            GameObject LevelBtn = GameObject.Instantiate(Level_Btn) as GameObject;

            // Set the parent of the button is Canvas GameObject
            LevelBtn.transform.SetParent(Panel);

            // Set the Position
            LevelBtn.transform.position = new Vector3(Panel.position.x * i, Panel.position.y, Panel.position.z);

            // Set the button Text as Level
            Text ButtonText = LevelBtn.GetComponentInChildren<Text>();
            string level = "LV" + i;
            ButtonText.text = level;

            // Add Event Listener to the button
            Button btn = LevelBtn.GetComponent<Button>();
            btn.onClick.AddListener(()=> { LoadScene(level); });
        }
	}

    public static void setFolderName(string _FolderName){
        FolderName = _FolderName;
    }

    int GetNumOfScene(){
        int count = 0;
        Debug.Log(FolderName);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            
            string path = "Scenes/" + FolderName + "/LV";
            if (scenePath.Contains(FolderName))
                count++;
        }
        return count;
    }

    void LoadScene(string Dir){
        SceneManager.LoadScene("Scenes/" + FolderName + "/" + Dir);
    }
}
