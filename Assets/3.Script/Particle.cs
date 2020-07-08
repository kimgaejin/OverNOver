using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public bool use;
    public Transform trans;
    public enum SpriteType { DEFAULT, BLOOD, BONE };
    public SpriteType spriteType;

    public void Init (Transform trans, SpriteType sprType ,Sprite _spr)
    {
        this.trans = trans;
        use = false;
        spriteType = sprType;

        foreach (Transform tran in trans)
        {
            SpriteRenderer spr = tran.GetComponent<SpriteRenderer>();
            spr.sprite = _spr;
        }

        trans.gameObject.SetActive(false);
    }

    public bool Condition(SpriteType sprType)
    {
        if (spriteType == sprType && use == false) return true;
        return false;
    }

    public void Effusion(Vector3 pos)
    {
        if (!trans) return;

        trans.gameObject.SetActive(true);
        use = true;
        foreach (Transform tran in trans)
        {
            tran.position = pos;

            Vector2 arrow = Vector2.zero;
            arrow.x = 1.5f - (Random.value * 3);
            arrow.y = 3 + Random.value;
            Rigidbody2D rigid = tran.GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.zero;
            rigid.AddForce(arrow, ForceMode2D.Impulse);
        }

        StartCoroutine(ExitParticle(3.0f));
    }


    public IEnumerator ExitParticle(float time)
    {
        yield return new WaitForSeconds(time);
        trans.gameObject.SetActive(false);
        use = false;
        yield break;
    }

}
