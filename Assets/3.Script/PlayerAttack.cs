using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject playerObject;
    private Player playerScript;
    private Animator playerAttackAnimator;
    private Rigidbody2D rigid;

    public bool isRightFace;

    private enum State { DEFAULT, ATTACK1, ATTACK2, ATTACK3 };
    private State curState;
    private bool isMoving;

    // Start is called before the first frame update
    public void Init()
    {
        // execute in Player.cs
        playerObject = this.gameObject;
        playerScript = playerObject.GetComponent<Player>();
        playerAttackAnimator = playerObject.transform.Find("SwordGraphics").GetComponent<Animator>();
        rigid = playerObject.GetComponent<Rigidbody2D>();
    }

    public bool Attack()
    {
        AnimatorStateInfo stateInfo = playerAttackAnimator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 시작 시
        // 전진 등 부가적인 움직임을 정함.
        if (stateInfo.IsName("playerSword_1hit"))
        {
            // DEFAULT => ATTACK1
            if (curState == State.DEFAULT)
            {
                curState = State.ATTACK1;

                // 해당 영역에 추가
                playerScript.FlipToRight(isRightFace);
                if (isMoving) rigid.AddForce(GetFaceVector2()*2, ForceMode2D.Impulse);
                else rigid.AddForce(GetFaceVector2(), ForceMode2D.Impulse);
                isMoving = false;
            }
        }
        else if (stateInfo.IsName("playerSword_2hit"))
        {
            if (curState == State.ATTACK1)
            {
                curState = State.ATTACK2;

                playerScript.FlipToRight(isRightFace);
                if (isMoving) rigid.AddForce(GetFaceVector2() * 2, ForceMode2D.Impulse);
                else rigid.AddForce(GetFaceVector2(), ForceMode2D.Impulse);
                isMoving = false;
            }
        }
        else if (stateInfo.IsName("playerSword_3hit"))
        {
            if (curState == State.ATTACK2)
            {
                curState = State.ATTACK3;

                playerScript.FlipToRight(isRightFace);
                if (isMoving) rigid.AddForce(GetFaceVector2() * 5, ForceMode2D.Impulse);
                else rigid.AddForce(GetFaceVector2()*3, ForceMode2D.Impulse);
                isMoving = false;
            }
        }
        else
        {
            if (curState != State.DEFAULT)
            {
                isMoving = false;
                curState = State.DEFAULT;
            }
        }

        // 애니메이션 변수에 따라
        // 공격을 어디까지 진행하는지 정함.
        if (playerAttackAnimator.GetBool("isAttack3") == true)
        {
            if (stateInfo.IsName("playerSword_3hit")
                && 0.99f <= stateInfo.normalizedTime)
            {
                AttackCancel();
                return false;
            }

            IsMoveKey();
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
            IsMoveKey();
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

            if (IsAttackKey())
            {
                playerAttackAnimator.SetBool("isAttack2", true);
            }
            IsMoveKey();
            return true;
        } 

        return false;
    }

    public bool IsAttackStarting(bool _isRightFace)
    {
        if (IsAttackKey())
        {
            AttackCancel();
            isRightFace = _isRightFace;
            playerAttackAnimator.SetBool("hitFail", false);
            playerAttackAnimator.SetBool("isAttack1", true);

            IsMoveKey();
            return true;
        }
        return false;
    }

    public void AttackFail()
    {
        playerAttackAnimator.SetBool("hitFail", true);
        isMoving = false;
        curState = State.DEFAULT;
    }

    public void AttackCancel()
    {
        playerAttackAnimator.SetBool("isAttack1", false);
        playerAttackAnimator.SetBool("isAttack2", false);
        playerAttackAnimator.SetBool("isAttack3", false);
       
        isMoving = false;
        curState = State.DEFAULT;
    }

    private bool IsAttackKey()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }
        return false;
    }

    private void IsMoveKey()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
            isRightFace = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
            isRightFace = true;
        }
    }

    private Vector2 GetFaceVector2()
    {
        if (isRightFace) return Vector2.right;
        return Vector2.left;
    }
}
