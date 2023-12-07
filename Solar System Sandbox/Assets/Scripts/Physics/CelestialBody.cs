using System;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Vector2 velocity;
    public float damping;
    public Vector2 acceleration;
    public Vector2 gravity;
    public float inverseMass;
    public Vector2 accumulatedForces { get; private set; }

    private GameObject sun;
    private AttractorForce sunGravity;

    private Vector2 currentPosition;

    private void Start()
    {
        sun = GameObject.FindWithTag("Sun");
        sunGravity = GetComponent<AttractorForce>();

        sunGravity.targetPos = sun.transform.position;
    }
    

    public void FixedUpdate()
    {
        DoFixedUpdate(Time.fixedDeltaTime);

        currentPosition = this.transform.position;
    }

    public void DoFixedUpdate(float dt)
    {
        if (!Universe.instance.gamePaused)
        {
            // Apply force from each attached ForceGenerator component
            System.Array.ForEach(GetComponents<ForceGenerator>(), generator => { if (generator.enabled) generator.UpdateForce(this); });

            Integrator.Integrate(this, dt);
            ClearForces();  
        }
    }

    public void ClearForces()
    {
        accumulatedForces = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        accumulatedForces += force;
    }


    public bool CheckIfMouseOnPos()
    {
        Vector2 temp = InputManager.instance.GetWorldPos();

        if (temp == currentPosition)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
