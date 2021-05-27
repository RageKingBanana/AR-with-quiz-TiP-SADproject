using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RESETLEVELS : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelSelection levelselection;
    public void reset()
    {
        PlayerPrefs.SetString("levelU", "100");
        PlayerPrefs.SetInt("levelClicked", 0);
        // Debug.Log(PlayerPrefs.GetInt("levelReached", 1));
        // Debug.Log(PlayerPrefs.GetInt("iskor"));
        // Debug.Log(PlayerPrefs.GetString("levelU"));
        // Debug.Log(PlayerPrefs.GetInt("levelClicked"));
        //levelselection.NiceFunc();
    }

}