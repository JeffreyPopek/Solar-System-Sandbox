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
                CollisionDetection.GetPenetration(spheres[i], spheres[j], out float penetration);

                if (penetration > 0.0f)
                {
                    if (!spheres[j].isSun)
                    {
                        FindObjectOfType<InputManager>().planets.Remove(spheres[j].gameObject.GetComponent<CelestialBody>());
                        Destroy(spheres[j].gameObject);
                    }

                    if (!spheres[i].isSun)
                    {
                        FindObjectOfType<InputManager>().planets.Remove(spheres[i].gameObject.GetComponent<CelestialBody>());
                        Destroy(spheres[i].gameObject);
                    }

                    return;
                }
            }
        }
    }
}
