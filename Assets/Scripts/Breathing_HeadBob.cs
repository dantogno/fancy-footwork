using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing_HeadBob : MonoBehaviour
{
    Vector3 startPos;
    public float amplitude = 10f;
    public float period = 5f;
    private GameObject player;
    private Vector3 lastPosition = new Vector3(0, 0, 0);
    protected void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected void Update()
    {
        if (player.transform.position==lastPosition)
        {
            float theta = Time.timeSinceLevelLoad / period;
            float distance = amplitude * Mathf.Sin(theta);
            transform.position += Vector3.up * distance;
        }
        else if(startPos!=transform.position)
        {
            startPos = transform.position;
        }
        lastPosition = player.transform.position;
    }
}
