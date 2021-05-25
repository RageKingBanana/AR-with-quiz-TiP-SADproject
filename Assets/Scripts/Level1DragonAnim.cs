using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level1DragonAnim : MonoBehaviour
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
            if (SceneManager.GetActiveScene().buildIndex == 1)
           {
             BattleSoulEater();
           }
           if (SceneManager.GetActiveScene().buildIndex == 2)
           {
             BattleTerror();
           }
           if (SceneManager.GetActiveScene().buildIndex == 3)
           {
             BattleUsurper();
           }
    }

   private void BattleSoulEater()
    {
        //Answer=2;
        Answer = PlayerPrefs.GetInt("ThisAnswer");
        if (Answer == 1)
        {
        animator.Play("Get Hit");
        }
        else if(Answer == 0)
        {
        animator.Play("Tail Attack");

        }
        else if(Answer==2)
        {
        animator.Play("Idle");

        }
        
    }

    private void BattleTerror()
    {
        //Answer=2;
        Answer = PlayerPrefs.GetInt("ThisAnswer");
        if (Answer == 1)
        {
        animator.Play("Get Hit");
        }
        else if(Answer == 0)
        {
        animator.Play("Claw Attack");
        }
        else if(Answer==2)
        {
        animator.Play("Idle02");
        }
        
    }

    private void BattleUsurper()
    {
        //Answer=2;
        Answer = PlayerPrefs.GetInt("ThisAnswer");
        if (Answer == 1)
        {
        //animator.SetBool("UsHit",true);
        animator.Play("Get Hit");
        }
        else if(Answer == 0)
        {
        animator.Play("Fly Flame Attack");
        }
        else if(Answer==2)
        {
        animator.Play("Fly Float");
        }
        
    }

}