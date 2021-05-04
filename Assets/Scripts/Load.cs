using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{

//    [SerializeField] GameEvents events = null;
 public void selectScene()
    {
        switch (this.gameObject.name)
        {
            case "HOME":
                SceneManager.LoadScene(0);
                break; 
            case "AR1":
                SceneManager.LoadScene(6);
                break;       
            case "AR ADVENTURE":
                SceneManager.LoadScene(7);
                break;    
            case "3D":
                SceneManager.LoadScene(7);
                break; 
        }
        

    }



}