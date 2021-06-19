using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject host;
    private Animator animator;
    private int Answer;
  

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //Battle();
        //stop();
    }
    private void Start()
    {
        
        //Battle();
    }
    private void Update()
    {
        Battle();
    }
    
   private void Battle()
    {
        
        //Answer=2;
        Answer = PlayerPrefs.GetInt("ThisAnswer");
        if (Answer == 1)
        {
        Debug.Log("1");
        animator.Play("Attack02");
        }
        else if(Answer == 0)
        {
        Debug.Log("0");
        animator.SetBool("DogDamage",true);
        }
        else if(Answer==2)
        {
        animator.SetBool("DogDamage",false);
        Debug.Log("2");
        animator.Play("Idle_Battle");
        }
        else if(Answer==3)
        {

        }
        
        
    }


}