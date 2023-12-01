using UnityEngine;

public class Integrator
{
    public static void Integrate(CelestialBody obj, float dt)
    {
        obj.transform.position += new Vector3(obj.velocity.x * dt, obj.velocity.y * dt);

        obj.acceleration = obj.accumulatedForces * obj.inverseMass;
        obj.acceleration += obj.gravity;

        obj.velocity += obj.acceleration * dt;
        obj.velocity *= Mathf.Pow(obj.damping, dt);
    }
}
