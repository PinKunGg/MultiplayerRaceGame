using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlipReSpawn : NetworkBehaviour
{
    public MyPlayerController PlayerCon;
    public Rigidbody rb;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Road")){
            Invoke("ReSpawn",1);
            Debug.Log("111");
        }
    }

    void ReSpawn(){
        this.transform.parent.position = PlayerCon.StartPos;
        this.transform.parent.rotation = PlayerCon.StartRot;
        PlayerCon.translation = 0f;
        rb.velocity = Vector3.zero;
        Debug.Log("789");
    }
}
