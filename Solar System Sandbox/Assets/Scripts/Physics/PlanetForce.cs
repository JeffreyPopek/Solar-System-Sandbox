using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetForce : ForceGenerator
{
    public float power = 10.0f;

    public override void UpdateForce(CelestialBody obj)
    {
        List<CelestialBody> allPlanets = FindObjectOfType<InputManager>().planets;

        foreach (CelestialBody other in allPlanets)
        {
            if (other != obj)
            {
                float sqrDist = (other.transform.position - this.transform.position).sqrMagnitude;
                Vector3 dir = (other.transform.position - this.transform.position).normalized;
                Vector3 force = dir * power / obj.inverseMass / other.inverseMass / sqrDist;

                obj.AddForce(force);
            }
        }
    }
}
