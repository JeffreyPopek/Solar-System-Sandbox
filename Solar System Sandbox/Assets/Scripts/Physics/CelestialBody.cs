using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Vector2 velocity;
    public float damping;
    public Vector2 acceleration;
    public Vector2 gravity;
    public float inverseMass;
    public Vector2 accumulatedForces { get; private set; }
    
    
    public void FixedUpdate()
    {
        DoFixedUpdate(Time.fixedDeltaTime);
    }

    public void DoFixedUpdate(float dt)
    {
        // Apply force from each attached ForceGenerator component
        System.Array.ForEach(GetComponents<ForceGenerator>(), generator => { if (generator.enabled) generator.UpdateForce(this); });

        Integrator.Integrate(this, dt);
        ClearForces();
    }

    public void ClearForces()
    {
        accumulatedForces = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        accumulatedForces += force;
    }
}
