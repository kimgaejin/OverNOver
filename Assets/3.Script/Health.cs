using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GameObject targetObject;
    private CharacterCommon characterCommon;
    private List<SpriteRenderer> bodySprites;
    private List<string> interactTag;

    public float hp;
    private bool isDamaged;

    public void Init(GameObject pObj,  float _hp, string graphicsName, string [] interactType)
    {
        targetObject = pObj;
        characterCommon = targetObject.GetComponent<CharacterCommon>();
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
                Transform characterTransform = collision.transform;
                CharacterCommon chara = characterTransform.GetComponent<CharacterCommon>();

                while (chara == null && characterTransform.parent != null)
                {
                    characterTransform = characterTransform.parent;
                    chara = characterTransform.GetComponent<CharacterCommon>();
                }
                if (chara == null) return;

                List<SkillEffect> skills = chara.GetSkillEffects();
                foreach (SkillEffect skill in skills)
                {
                    skill.Do(this.gameObject);
                }
            }
        }
    }

    public void Damaged(float damage, bool invincibilityType)
    {
        // Damage로 인해 hp가 낮아지는 경우 호출하는 함수
        // Damage의 SkillEffect.Do() 에서 호출하며
        // 무적이 존재하는 공격의 경우 5틱 (1초)간 무적하고 아닐경우 1틱 (0.2초)만 무적 상태

        hp -= damage;
        if (invincibilityType )
            StartCoroutine(CDamaged(5));
        else
            StartCoroutine(CDamaged(1));

        characterCommon.Interrupt("damaged");
        if (hp <= 0) characterCommon.Dead();
    }

    private IEnumerator CDamaged(int tick)
    {
        isDamaged = true;

        for (int i = 0; i < tick; i++)
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
