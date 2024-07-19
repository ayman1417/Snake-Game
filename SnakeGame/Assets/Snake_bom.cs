using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakeBom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Player")
        {
            GameObject.FindObjectOfType<Game_O>().Game_Over = true;
        }
        else if (other.tag == "Coins")
        {
            //  GameObject.FindObjectOfType<Game_O>().Eat_Coins(other.gameObject.transform.position);
            GameObject.FindObjectOfType<Game_O>().numberEaten++;
            Destroy(other.gameObject);
        }
    }
}
