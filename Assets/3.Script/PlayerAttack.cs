using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameObject playerObject;
    Animator playerAttackAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        playerObject = this.gameObject;
        playerAttackAnimator = playerObject.transform.Find("SwordGraphics").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.D))
        {
            playerAttackAnimator.SetBool("isAttack", true);
        }
        else
        {
            playerAttackAnimator.SetBool("isAttack", false);
        }
        */
    }

    public bool Attack()
    {
        AnimatorStateInfo stateInfo = playerAttackAnimator.GetCurrentAnimatorStateInfo(0);

        if (playerAttackAnimator.GetBool("isAttack3") == true)
        {
            if (stateInfo.IsName("playerSword_3hit")
                && 0.99f <= stateInfo.normalizedTime)
            {
                AttackCancel();
                return false;
            }
            return true;
        }
        else if (playerAttackAnimator.GetBool("isAttack2") == true)
        {
            if (stateInfo.IsName("playerSword_2hit")
                && 0.99f <= stateInfo.normalizedTime)
            {
                AttackCancel();
                return false;
            }

            if (IsAttackKey())
            {
                playerAttackAnimator.SetBool("isAttack3", true);
            }
            return true;
        }
        else if (playerAttackAnimator.GetBool("isAttack1") == true)
        {
            if (stateInfo.IsName("playerSword_1hit")
                && 0.99f <= stateInfo.normalizedTime)
            {
                AttackCancel();
                return false;
            }
            else
            {
                Debug.LogError(stateInfo.normalizedTime);
            }

            if (IsAttackKey())
            {
                playerAttackAnimator.SetBool("isAttack2", true);
            }
            return true;
        } 

        return false;
    }

    public bool IsAttackStarting()
    {
        if (IsAttackKey())
        {
            playerAttackAnimator.SetBool("isAttack1", true);
            return true;
        }
        return false;
    }

    public void AttackCancel()
    {
        playerAttackAnimator.SetBool("isAttack1", false);
        playerAttackAnimator.SetBool("isAttack2", false);
        playerAttackAnimator.SetBool("isAttack3", false);
    }

    private bool IsAttackKey()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }
        return false;
    }
}
