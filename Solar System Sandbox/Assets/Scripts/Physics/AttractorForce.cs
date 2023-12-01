using UnityEngine;

public class AttractorForce : ForceGenerator
{
    public Vector3 targetPos;
    public float power;

    public override void UpdateForce(CelestialBody obj)
    {
        Vector2 distance = this.transform.position - targetPos;
        
        Vector2 force = (power / distance.sqrMagnitude) * -distance.normalized;
        
        obj.AddForce(force);
    }
}
