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
                PlayerPrefs.SetInt("levelClicked", 7);
                SceneManager.LoadScene(4);
                break;
            case "AR1":
                PlayerPrefs.SetInt("levelClicked", 6);
                SceneManager.LoadScene(4);
                break;
            case "HOME":
                PlayerPrefs.SetInt("levelClicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "3D":
                PlayerPrefs.SetInt("levelClicked", 7);
                SceneManager.LoadScene(4);
                break; 
            case "Button_[Exit]":
                PlayerPrefs.SetInt("levelClicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "YES-EXIT":
                PlayerPrefs.SetInt("levelClicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "EXITCRED":
                PlayerPrefs.SetInt("levelClicked", 0);
                SceneManager.LoadScene(4);
                break;
            case "EXPLORE":
                PlayerPrefs.SetInt("levelClicked", 8);
                SceneManager.LoadScene(4);
                break;
        }
        

    }


}