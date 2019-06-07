using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepPlacement : MonoBehaviour
{
    //footstep game objects
    public List<GameObject> footSteps;
    //the grid in the scene
    private Grid grid;

    //has it previously been placed
    private bool notPlaced = true;

    //the time between each step placement
    public float secondsBetweenSteps = .1f;

    //player object
    public GameObject player=null;

    public AudioSource audioSource;

    //node to start at
    int startingNode = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        //get grid
        grid = GetComponent<Grid>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaceSteps());
    }

    // Update is called once per frame
    void Update()
    {
        //if they have not been placed
        if (!notPlaced)
        {
            //check is the player has passed the last foot step object
            CheckPlayerPosition();
        }
    }

    void CheckPlayerPosition()
    {
        //if the player has hit the space then set the next positions
        if(player.GetComponent<BoxCollider>().bounds.Contains(footSteps[footSteps.Count - 1].transform.position))
        {
            if ((startingNode + 4) < (grid.path.Count - 5))
            {
                startingNode += 4;
                notPlaced = true;
                StartCoroutine(PlaySound());
            }
        }
    }

    IEnumerator PlaySound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1);
        StartCoroutine(PlaceSteps());
    }

    IEnumerator PlaceSteps()
    {
        //Place steps
        //count of how many have been placed starting at 0
        int index = 0;
        //place each game object to the correct spot
        foreach (GameObject f in footSteps)
        {
            if (startingNode + index < (grid.path.Count - 1))
                //get the world position of the grid node
                f.transform.position = grid.path[startingNode + index].worldPosition;
            //increase the index
            index++;
            yield return new WaitForSeconds(secondsBetweenSteps);
        }
        //set it to be placed
        notPlaced = false;
    }

}
