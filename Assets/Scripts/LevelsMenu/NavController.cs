﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavController : MonoBehaviour {

	public void backToPlayMenu()
    {
        SceneManager.LoadScene("Scenes/PlayMenu");
    }
}
