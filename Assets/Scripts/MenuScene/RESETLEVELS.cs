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
        PlayerPrefs.SetString("levelU", "10000");
        levelselection.NiceFunc();
    }

}