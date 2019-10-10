using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollide : MonoBehaviour
{
    AudioSource cheeseEatSound;

    private void Start()
    {
        cheeseEatSound = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Cheese")
        {
            cheeseEatSound.Play();
            Destroy(other.gameObject, 0.2f);
            Scores.AddScores(Scores.cheesePoints);
        }

    }
}
