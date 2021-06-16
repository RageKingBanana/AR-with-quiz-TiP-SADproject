//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class LEVELSELECT : MonoBehaviour
{

//    [SerializeField] GameEvents events = null;
    /*public void quizselect()
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
        }
    }*/
    public void selectScene()
    {
        PlayerPrefs.SetInt("ThisAnswer",2);
        switch (this.gameObject.name)
        {
        case "Level1":

                PlayerPrefs.SetInt("loadedscene",1);
                PlayerPrefs.SetInt("levelClicked",1);
                SceneManager.LoadScene(4);
                break; 
        case "Level2":
                PlayerPrefs.SetInt("loadedscene",2);
                PlayerPrefs.SetInt("levelClicked", 2);
                SceneManager.LoadScene(4);
                break;
        case "Level3":
                 PlayerPrefs.SetInt("loadedscene",3);
                PlayerPrefs.SetInt("levelClicked", 3);
                SceneManager.LoadScene(4);
                break;   
        case "AR ADVENTURE":
                PlayerPrefs.SetInt("loadedscene",6);
                SceneManager.LoadScene(4);
                break;
            case "AR1":
                PlayerPrefs.SetInt("loadedscene",6);
                SceneManager.LoadScene(4);
                break;
            case "HOME":
                PlayerPrefs.SetInt("loadedscene", 0);
                SceneManager.LoadScene(4);
                break;
            case "3D":
                PlayerPrefs.SetInt("loadedscene", 5);
                SceneManager.LoadScene(4);
                break; 
            case "Button_[Exit]":
                PlayerPrefs.SetInt("loadedscene", 0);
                SceneManager.LoadScene(4);
                break;
            case "EXITSTAGE":
                PlayerPrefs.SetInt("loadedscene", 0);
                SceneManager.LoadScene(4);
                break;
            case "EXITCRED":
                PlayerPrefs.SetInt("loadedscene", 0);
                SceneManager.LoadScene(4);
                break;
        }
    }

    
}