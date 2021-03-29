using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSmokeFx : MonoBehaviour
{
    WheelCollision _listener;
    public void Initialize(WheelCollision l)
    {
        _listener = l;
    }
    private void OnCollisionEnter(Collision other) {
        _listener.OnCollisionEnter(other);
    }
}
