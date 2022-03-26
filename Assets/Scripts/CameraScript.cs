using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Update the camera using the move tool, whatever those transform positions are for the player (target) moves the camera, does not move the y & Z axis
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
        //this.transform.position = new Vector3(target.transform.position.y);
    }
}
