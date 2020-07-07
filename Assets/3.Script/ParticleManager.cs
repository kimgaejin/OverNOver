using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private List<Particle> particles;

    public Sprite SPRITE_BLOOD;
    public Sprite SPRITE_BONE;

    private void Awake()
    {
        particles = new List<Particle>();
        foreach (Transform tran in this.transform)
        {
            Particle particle = tran.gameObject.AddComponent<Particle>();
            particle.Init(tran, Particle.SpriteType.BONE, SPRITE_BONE);
            particles.Add(particle);
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Particle particle in particles)
            {
                if (particle.Condition(Particle.SpriteType.BONE))
                {
                    particle.Effusion(Vector3.zero);
                    break;
                }
            }
        }
    }

}
