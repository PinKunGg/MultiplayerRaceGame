using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow360 : MonoBehaviour
{
    public Transform player;
    public float distance = 10;
    public float height = 5;
    public Vector3 lookOffset = new Vector3(0, 1, 0);
    public float cameraSpeed = 10;
    public float rotSpeed = 10;

	
	public bool gotostart;
    public Vector3 pos;
    public Quaternion rot;

    private void Start()
    {
        pos = this.transform.position;
        rot = this.transform.rotation;
    }

    void FixedUpdate()
    {
        if (player)
        {
            Vector3 lookPosition = player.position + lookOffset;
            Vector3 relativePos = lookPosition - transform.position;
            Quaternion rot = Quaternion.LookRotation(relativePos);

            transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);

            Vector3 targetPos = player.transform.position + player.transform.up * height - player.transform.forward * distance;

            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * cameraSpeed * 0.1f);
        }
		else if(gotostart){
			GoToStartPos();
		}
    }

    public void GoToStartPos()
    {
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed * 0.1f);

        this.transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime * cameraSpeed * 0.1f);

		if(transform.position == pos && transform.rotation == rot){
			gotostart = false;
		}
    }
}
