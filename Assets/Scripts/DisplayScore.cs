using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
   
    // Update is called once per frame
   public void Update()
    {
        this.GetComponent<Text>().text = ("Score: " + Scores.points);
    }
}
