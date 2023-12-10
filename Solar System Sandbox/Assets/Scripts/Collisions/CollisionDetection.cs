using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;

public static class CollisionDetection
{
    public static void GetNormalAndPenetration(Sphere s1, Sphere s2, out Vector3 normal, out float penetration)
    {
        Vector2 midline = s1.position - s2.position;
        float size = midline.magnitude;

        normal = midline * (1.0f / size);
        penetration = s1.Radius + s2.Radius - size;
    }

    public static void ApplyCollisionResolution(Sphere s1, Sphere s2)
    {
        GetNormalAndPenetration(s1, s2, out Vector3 normal, out float penetration);

        if (penetration <= 0.0f) { return; }
        if (s1.invMass <= 0.0f && s2.invMass <= 0.0f) { return; }

        float d1pct = s1.invMass / (s1.invMass + s2.invMass);
        float d2pct = s2.invMass / (s2.invMass + s1.invMass);

        Vector3 deltaS1 = normal * d1pct * penetration;
        Vector3 deltaS2 = -normal * d2pct * penetration;

        s1.position += deltaS1;
        s2.position += deltaS2;

        float mu = 1.0f;
        float sepVel = Vector3.Dot(s1.velocity - s2.velocity, normal);
        float deltaVel = -sepVel - mu * sepVel;

        s1.velocity += deltaVel * d1pct * normal;
        s2.velocity -= deltaVel * d2pct * normal;
    }

    public static void GetPenetration(Sphere s1, Sphere s2, out float penetration)
    {
        Vector2 midline = s1.position - s2.position;
        float size = midline.magnitude;

        penetration = s1.Radius + s2.Radius - size;
    }
}
