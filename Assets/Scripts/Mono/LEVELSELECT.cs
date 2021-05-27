﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class LEVELSELECT : MonoBehaviour
{

//    [SerializeField] GameEvents events = null;
    public void selectScene()
    {
        PlayerPrefs.SetInt("ThisAnswer",2);
        switch (this.gameObject.name)
        {
            case "Level1":
                PlayerPrefs.SetInt("levelClicked",1);
                SceneManager.LoadScene(4);
                break; 
            case "Level2":
                PlayerPrefs.SetInt("levelClicked", 2);
                SceneManager.LoadScene(4);
                break;
            case "Level3":
                PlayerPrefs.SetInt("levelClicked", 3);
                SceneManager.LoadScene(4);
                break;
            case "AR ADVENTURE":
                PlayerPrefs.SetInt("clicked", 7);
                SceneManager.LoadScene(4);
                break;
            case "AR1":
                PlayerPrefs.SetInt("clicked", 6);
                SceneManager.LoadScene(4);
                break;
            case "HOME":
                PlayerPrefs.SetInt("clicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "3D":
                PlayerPrefs.SetInt("clicked", 7);
                SceneManager.LoadScene(4);
                break; 
            case "Button_[Exit]":
                PlayerPrefs.SetInt("clicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "YES-EXIT":
                PlayerPrefs.SetInt("clicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "EXITCRED":
                PlayerPrefs.SetInt("clicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "EXPLORE":
                PlayerPrefs.SetInt("clicked", 8);
                SceneManager.LoadScene(4);
                break;
           
        }
    }

    
}