using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GameObject targetObject;
    private List<SpriteRenderer> bodySprites;
    private List<string> interactTag;

    public float hp;
    private bool isDamaged;

    public void Init(GameObject pObj,  float _hp, string graphicsName, string [] interactType)
    {
        targetObject = pObj;
        hp = _hp;

        bodySprites = new List<SpriteRenderer>();
        Transform body = targetObject.transform.Find(graphicsName);
        foreach (Transform part in body)
        {
            bodySprites.Add(part.GetComponent<SpriteRenderer>());
        }

        interactTag = new List<string>();
        foreach (string type in interactType)
        {
            interactTag.Add(type);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactTag.Contains(collision.tag))
        {
            if (!isDamaged)
            {
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

    public void Damaged(float damage)
    {
        // Damage로 인해 hp가 낮아지는 경우 호출하는 함수
        // Damage의 SkillEffect.Do() 에서 호출하며
        // 무적시간 자체는 데미지를 입었을 경우에만 삽입한다.
        hp -= damage;
        StartCoroutine(CDamaged());
    }

    public void DamagedWithoutInvincibility(float damage)
    {
        // 무적시간이 존재하지 않는 데미지
        hp -= damage;
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
        foreach (SpriteRenderer spr in bodySprites)
        {
            spr.color = color;
        }
    }
}
