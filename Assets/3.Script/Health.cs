using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GameObject playerObject;
    private Player playerScript;
    private List<SpriteRenderer> playerBodyGraphics;

    public float hp;
    private bool isDamaged;

    public void Init(GameObject pObj, Player pSct,  float _hp)
    {
        playerObject = pObj;
        playerScript = pSct;
        hp = _hp;

        playerBodyGraphics = new List<SpriteRenderer>();
        Transform body = playerObject.transform.Find("BodyGraphics");
        foreach (Transform part in body)
        {
            playerBodyGraphics.Add(part.GetComponent<SpriteRenderer>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemyAttack")
        {
            if (!isDamaged)
            {
                // 데미지
                StartCoroutine(CDamaged());

                // 군중제어
                Transform enenmyTransform = collision.transform.parent.parent;
                MonsterCommon monster = enenmyTransform.GetComponent<MonsterCommon>();
                List<SkillEffect> monsterSkills = monster.GetSkillEffects().ToList<SkillEffect>();

                foreach (SkillEffect skill in monsterSkills)
                {
                    skill.Do(this.gameObject);
                }
            }
        }
    }

    private IEnumerator CDamaged()
    {
        isDamaged = true;

        for (int i = 0; i < 5; i++)
        {
            SetColors(Color.black);
            yield return new WaitForSeconds(.1f);
            SetColors(Color.white);
            yield return new WaitForSeconds(.1f);
        }
       
        isDamaged = false;
        yield break;
    }

    private void SetColors(Color color)
    {
        foreach (SpriteRenderer spr in playerBodyGraphics)
        {
            spr.color = color;
        }
    }
}
