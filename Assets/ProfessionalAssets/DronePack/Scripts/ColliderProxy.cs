using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsPlayground
{
    public class ColliderProxy : MonoBehaviour {

        public GameObject host;
        private Animator animator;

private void Awake()
{
animator = GetComponent<Animator>();
}

        void OnTriggerEnter(Collider col) { animator.SetBool("character_nearby",true); }
        void OnTriggerStay(Collider col) { animator.SetBool("character_nearby",true); }
        void OnTriggerExit(Collider col) { animator.SetBool("character_nearby",false);}

        void OnCollisionEnter(Collision col) { animator.SetBool("character_nearby",true); }
        void OnCollisionStay(Collision col) { animator.SetBool("character_nearby",true);}
        void OnCollisionExit(Collision col) { animator.SetBool("character_nearby",false);}
    }
}
