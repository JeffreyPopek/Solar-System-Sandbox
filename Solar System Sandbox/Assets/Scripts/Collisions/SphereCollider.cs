using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : PhysicsCollider
{
    public bool isSun = false;

    public Vector3 Center => transform.position;
    public float Radius = .5f;
}