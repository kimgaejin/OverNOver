using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private List<Particle> particles;

    private void Awake()
    {
        particles = new List<Particle>();
        foreach (Transform tran in this.transform)
        {
            Particle particle = tran.gameObject.AddComponent<Particle>();
            particle.Init(tran);
            particles.Add(particle);
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Particle particle in particles)
            {
                if (particle.use == false)
                {
                    particle.Effusion(Vector3.zero);
                    break;
                }
            }
        }
    }

}
