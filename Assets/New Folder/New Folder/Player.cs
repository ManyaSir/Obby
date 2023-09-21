using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public GameObject playerG;
    private Vector3 Check;

    void Start(){
        Check = new Vector3(283.31f, 23.776f, 349.1685f);
    }
    
    void OnCollisionStay(Collision col){
        Control.coll = col;
    }

    void OnCollisionEnter(Collision other)
    { 
        Control.jumpagree = true;
        if (other.gameObject.name == "Checkpoint")
        {
            Check = new Vector3(playerG.transform.position.x, playerG.transform.position.y, playerG.transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.name == "Obstacle")
        {
            playerG.transform.position = Check;
        }
    }
}
