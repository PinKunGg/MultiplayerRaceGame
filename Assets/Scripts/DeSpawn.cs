using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeSpawn : NetworkBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("ReSpawn")){
            Invoke("ReSpawn",1);
            Debug.Log("000");
        }
    }

    void ReSpawn(){
        this.transform.position = this.GetComponent<MyPlayerController>().StartPos;
        this.transform.rotation = this.GetComponent<MyPlayerController>().StartRot;
        GetComponent<MyPlayerController>().translation = 0f;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("456");
    }
}
