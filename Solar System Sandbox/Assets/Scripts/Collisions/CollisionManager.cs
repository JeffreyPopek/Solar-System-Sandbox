using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void FixedUpdate()
    {
        Sphere[] spheres = FindObjectsOfType<Sphere>();

        for (int i = 0; i < spheres.Length; i++)
        {
            for (int j = i + 1; j < spheres.Length; j++)
            {
                CollisionDetection.ApplyCollisionResolution(spheres[i], spheres[j]);
            }
        }

        Sun[] suns = FindObjectsOfType<Sun>();

        for (int i = 0; i < suns.Length; i++)
        {
            for (int j = 0; j < spheres.Length; j++)
            {
                CollisionDetection.GetPenetration(suns[i], spheres[j], out float penetration);

                if (penetration > 0.0f)
                    Destroy(spheres[j].gameObject);
            }
        }
    }
}
