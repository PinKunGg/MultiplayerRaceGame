using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WheelCollision : NetworkBehaviour
{
    public GameObject SmokeFx;
    public Collider[] colliderS;
    void Awake()
    {
        // Check if Colider is in another GameObject
        colliderS = transform.Find("WheelColliders").GetComponentsInChildren<Collider>();

        foreach (var item in colliderS)
        {
            if (item.gameObject != gameObject)
            {
                WheelSmokeFx cb = item.gameObject.AddComponent<WheelSmokeFx>();
                cb.Initialize(this);
            }
        }

    }
    public void OnCollisionEnter(Collision other)
    {
        Vector3 closepoint;
        if (other.collider.gameObject.CompareTag("Road"))
        {
            closepoint = other.collider.ClosestPoint(this.transform.position);
            CmdSpawnSmokeFx(closepoint, this.transform.rotation);
        }
    }

    [Command]
    void CmdSpawnSmokeFx(Vector3 closepoint,Quaternion rot){
        RpcSpawnSmokeFx(closepoint, rot);
    }

    [ClientRpc]
    void RpcSpawnSmokeFx(Vector3 closepoint,Quaternion rot){
        GameObject Fx = Instantiate(SmokeFx, closepoint, rot * Quaternion.Euler(0, 180, 0));
        Destroy(Fx, 0.5f);
    }
}
