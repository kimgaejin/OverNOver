using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager instance;
    private List<Particle> particles;

    public GameObject particlePrefab;
    public Sprite SPRITE_BLOOD;
    public Sprite SPRITE_BONE;

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
                    var newSingleton = new GameObject("ParticleManager").AddComponent<ParticleManager>();
                    instance = newSingleton;
                }

            }
            return instance;
        }
    }


    private void Awake()
    {
        particles = new List<Particle>();
        NewParticle(Particle.SpriteType.BONE, SPRITE_BONE);
        NewParticle(Particle.SpriteType.BONE, SPRITE_BONE);
        NewParticle(Particle.SpriteType.BLOOD, SPRITE_BLOOD);
        NewParticle(Particle.SpriteType.BLOOD, SPRITE_BLOOD);
    }

    private Particle NewParticle(Particle.SpriteType type, Sprite sprite)
    {
        GameObject ins = Instantiate(particlePrefab, transform);
        Particle particle = ins.AddComponent<Particle>();
        particle.Init(ins.transform, type, sprite);
        particles.Add(particle);
        return particle;
    }
    

    public void Show(Particle.SpriteType type, Vector3 pos)
    {
        bool process = false;
        foreach (Particle particle in particles)
        {
            if (particle.Condition(type))
            {
                particle.Effusion(pos);
                process = true;
                break;
            }
        }

        if (process == false)
        {
            NewParticle(Particle.SpriteType.BONE, SPRITE_BONE).Effusion(pos);
        }
    }
}