using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{

    public void Get1()
    {
        FindObjectOfType<Move>().cherrycount();
        Destroy(gameObject);
    }

    public void Get2()
    {
        FindObjectOfType<Move>().gemcount();
        Destroy(gameObject);
    }
}
