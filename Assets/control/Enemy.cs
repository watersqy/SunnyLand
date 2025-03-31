using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    protected AudioSource Deathaudio;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        Deathaudio = GetComponent<AudioSource>();
    }
    public void Death()
    {
        anim.GetComponent<Animator>().enabled = false;
        Destroy(gameObject);
    }

    public void Jumpon()
    {
        anim.SetTrigger("death");
        Deathaudio.Play();
    }
}
