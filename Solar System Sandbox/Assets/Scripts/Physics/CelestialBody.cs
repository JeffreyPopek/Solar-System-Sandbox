using System;
using Unity.VisualScripting;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public int planetIndex;

    public bool isStatic = false;
    public Vector2 velocity;
    public float damping;
    public Vector2 acceleration;
    public float inverseMass;

    public Vector2 accumulatedForces { get; private set; }

    private Vector2 currentPosition;

    private void Awake()
    {
        // find direction based on position
        Vector2 dir = new Vector2(-this.transform.position.y, this.transform.position.x).normalized;

        // find magnitude based on distance from sun
        float dist = this.transform.position.magnitude;
        float invMass = FindObjectOfType<InputManager>().planets[0].inverseMass;
        float gravConst = 10.0f;
        float mag = (float)Math.Sqrt((gravConst / invMass) / dist);

        // set starting velocity by multiplying the two
        velocity = dir * mag;
    }

    public void FixedUpdate()
    {
        DoFixedUpdate(Time.fixedDeltaTime);

        currentPosition = this.transform.position;

        CheckIfOutOfBounds();
    }

    public void DoFixedUpdate(float dt)
    {
        if (!Universe.instance.gamePaused && !isStatic)
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

    public void CheckIfOutOfBounds()
    {
        if (transform.position.magnitude >= 200.0f)
        {
            FindObjectOfType<InputManager>().planets.Remove(this);
            Destroy(gameObject);
        }
    }

    public void DestroyObject()
    {
        Destroy(this.GameObject());
    }
}
