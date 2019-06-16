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

    List<Node> previousPath;

    bool canPlace = true;

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
        if (grid.path != null)
            StartCoroutine(PlaceSteps());
        else
            StartCoroutine(InitialPlacement());
    }

    //if the path is null at first, keep going until not null then place steps
    IEnumerator InitialPlacement()
    {
        //ever .15 seconds check to see if null
        while (grid.path == null)
        {
            yield return new WaitForSeconds(.15f);
        }
        if (previousPath != grid.path)
        {
            previousPath = grid.path;
            startingNode = 0;
            //place steps
            StartCoroutine(PlaceSteps());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if they have not been placed
        if (!notPlaced)
        {
            if (grid.path != null)
                //check is the player has passed the last foot step object
                CheckPlayerPosition();
        }
        if (grid.path != null)
        {
            if (Vector3.Distance(player.transform.position, grid.path[grid.path.Count - 1].worldPosition) < 4)
            {
                canPlace = false;
                grid.path = null;
                Pathfinder.calculatePath = true;
                StartCoroutine(InitialPlacement());
            }
        }
    }

    void CheckPlayerPosition()
    {
        //if the player has hit the space then set the next positions
        //player.GetComponent<BoxCollider>().bounds.Contains(footSteps[footSteps.Count - 1].transform.position)
        if (Vector3.Distance(player.transform.position, footSteps[footSteps.Count - 1].transform.position)<4)
        {
            if ((startingNode + 4) < (grid.path.Count - 5))
            {
                if (canPlace)
                {
                    canPlace = false;
                    startingNode += 4;
                    notPlaced = true;
                    StartCoroutine(PlaySound());
                }
            }
        }
    }

    //play the foot step sounds
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

            f.transform.LookAt(grid.path[startingNode + (index+1)].worldPosition);
            //increase the index
            index++;
            yield return new WaitForSeconds(secondsBetweenSteps);
        }
        //set it to be placed
        notPlaced = false;
        canPlace = true;
    }

}
