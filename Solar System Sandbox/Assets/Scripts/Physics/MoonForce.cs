using UnityEngine;

public class MoonForce : ForceGenerator
{
    public GameObject primary;
    public Vector3 targetPos;
    public float power;

    public override void UpdateForce(CelestialBody obj)
    {
        if (primary != null)
            targetPos = primary.transform.position;
        else
            targetPos = Vector3.zero;

        Vector2 distance = this.transform.position - targetPos;

        Vector2 force = (power / distance.magnitude) * -distance.normalized;

        obj.AddForce(force);
    }
}
