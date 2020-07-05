using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
protected Animator animator;
   protected virtual void Start(){
       audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
     public  void death()
    {
        GetComponent<Collider2D>().enabled=false;
        Destroy(gameObject);
    }
    public void jumpOn()
    {
                audioSource.Play();
        animator.SetTrigger("death");

    }
}