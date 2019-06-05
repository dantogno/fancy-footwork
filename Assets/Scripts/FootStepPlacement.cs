using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepPlacement : MonoBehaviour
{
    public List<GameObject> footSteps;
    private Grid grid;

    private bool notPlaced = true;

    int startingNode = 1;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if(notPlaced)
        {
            notPlaced = false;
            int index = 0;
            foreach(GameObject f in footSteps)
            {
                f.transform.position = grid.path[startingNode+index].worldPosition;
                index++;
            }
        }
    }
}
