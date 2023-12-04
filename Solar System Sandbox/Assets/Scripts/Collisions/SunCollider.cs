using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : PhysicsCollider
{
    public Vector3 Center => transform.position;
    public float Radius = 5.0f;
}