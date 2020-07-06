using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle
{
    public bool enable;
    public Transform trans;
    public Sprite sprite;

    public Particle(Transform trans)
    {
        this.trans = trans;
    }

    public void Effusion(int size)
    {
        if (!trans) return;
        foreach (Transform tran in trans)
        {
            Vector2 arrow = Vector2.zero;
            arrow.x = 1.5f - (Random.value * 3);
            arrow.y = 3 + (int)(Random.value * 3);
            tran.GetComponent<Rigidbody2D>().AddForce(arrow, ForceMode2D.Impulse);
        }
    }
}


public class ParticleManager : MonoBehaviour
{
    private List<Particle> particles;

    private void Awake()
    {
        particles = new List<Particle>();
        foreach (Transform tran in this.transform)
        {
            particles.Add(new Particle(tran));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            particles[0].Effusion(3);
        }
    }

}
