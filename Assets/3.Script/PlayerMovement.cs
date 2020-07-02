using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject playerObject;
    private Rigidbody2D rigidbody2d;
    private Animator playerBodyAnimator;
    private PlayerGroundCollider playerGroundCollider;

    private float moveSpeed = 2.0f;
    private float jumpPower = 4.0f;

    private UnityEngine.Quaternion leftFace;
    private UnityEngine.Quaternion rightFace;

    private void Start()
    {
        playerObject = this.gameObject;
        rigidbody2d = playerObject.GetComponent<Rigidbody2D>();
        playerGroundCollider = playerObject.transform.Find("GroundCollider").GetComponent<PlayerGroundCollider>();
        playerBodyAnimator = playerObject.transform.Find("BodyGraphics").GetComponent<Animator>();

        playerGroundCollider.SetPlayerMovement(this.GetComponent<PlayerMovement>());

        leftFace.eulerAngles = new Vector3(0, -180, 0);
        rightFace.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        PlusJump();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // attacked 효과를 실험해보기 위한 임시 스크립트
        if (collision.tag == "enemyAttack")
        {
            Transform enenmyTransform = collision.transform.parent.parent;
            MonsterCommon monster = enenmyTransform.GetComponent<MonsterCommon>();
            List <SkillEffect> monsterSkills = monster.GetSkillEffects().ToList<SkillEffect>();
            foreach (SkillEffect skill in monsterSkills)
            {
                skill.Do(this.gameObject);
            }
        }
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.S) && Jump())
        {

        }

        Vector3 arrow = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            arrow += Vector3.left;
            playerObject.transform.rotation = leftFace;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            arrow += Vector3.right;
            playerObject.transform.rotation = rightFace;
        }
        else
        {
            NoMove();
            return;
        }
        arrow = arrow.normalized * moveSpeed * Time.deltaTime;
        playerObject.transform.Translate(arrow, Space.World);
        playerBodyAnimator.SetBool("isRunning", true);
    }

    private void NoMove()
    {
        playerBodyAnimator.SetBool("isRunning", false);
    }

    public bool Jump()
    {
        bool isGround = rigidbody2d.velocity.y <= 0.01f && playerGroundCollider.GetIsGround() == true;
        if (isGround == false) return false;

        rigidbody2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        return true;
    }

    private void PlusJump()
    {
        if (playerGroundCollider.GetIsJumpGround() && rigidbody2d.velocity.y < 0.01f)
        {
            rigidbody2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

}
