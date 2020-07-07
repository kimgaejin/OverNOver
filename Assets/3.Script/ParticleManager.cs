using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager instance;
    private List<Particle> particles;


    public static ParticleManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ParticleManager>();

                if (obj != null)
                {
                    instance = obj;
                }

                else
                {
                    var newSingleton = new GameObject("Particles").AddComponent<ParticleManager>();
                    instance = newSingleton;
                }

            }
            return instance;
        }
    }

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
    

    public void Show(Particle.SpriteType type, Vector3 pos)
    {
        foreach (Particle particle in particles)
        {
            if (particle.Condition(type))
            {
                particle.Effusion(pos);
                break;
            }
        }
    }
}