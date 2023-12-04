using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollider : MonoBehaviour
{
    public float invMass
    {
        get
        {
            CelestialBody particle;
            if (TryGetComponent(out particle))
            {
                return particle.inverseMass;
            }
            return 0;
        }
        set
        {
            CelestialBody particle;
            if (TryGetComponent(out particle))
            {
                particle.inverseMass = value;
            }
        }
    }

    public Vector3 velocity
    {
        get
        {
            CelestialBody particle;
            if (TryGetComponent(out particle))
            {
                return particle.velocity;
            }
            return Vector3.zero;
        }
        set
        {
            CelestialBody particle;
            if (TryGetComponent(out particle))
            {
                particle.velocity = value;
            }
        }
    }

    public Vector3 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            CelestialBody particle;
            if (TryGetComponent(out particle))
            {
                particle.transform.position = value;
            }
        }
    }
}
