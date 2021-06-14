using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level1DragonAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject host;
    private Animator animator;
    private int Answer,Anims;
    public GameObject flameatk;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //flameatk.SetActive(false);
        //Battle();
        //stop();
    }
    private void Start()
    {
       flameatk.SetActive(false); //Battle();
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
             Anims=PlayerPrefs.GetInt("ThisAnswer");
              if (Anims == 2)
                   {
                    flameatk.SetActive(false);
                   }
              else 
                   {
                    ///
                   }    
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
        flameatk.SetActive(false);
        //animator.SetBool("UsHit",true);
        animator.Play("Get Hit");
        }
        else if(Answer == 0)
        {
        flameatk.SetActive(true);
        animator.Play("Fly Flame Attack");

        }
        else if(Answer==2)
        {
        animator.Play("Fly Float");
        }
        //flameatk.SetActive(false);
    }
    

}