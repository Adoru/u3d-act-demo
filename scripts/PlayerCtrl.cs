using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    private CharacterController cc;
    private Animator animator;
    public float speed = 4;
    public float runSpeed = 8;
    private float speedUpTimer = 0;

    public bool dodgeFlag = false;
    public float dodgeTime = 0.47f;
    private float dodgeTimeTimer = 0;

    private void Awake()
    {
        cc = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //以虚拟杆为优先
        if (JoyStick.h != 0||JoyStick.v!=0)
        {
            h = JoyStick.h;
            v = JoyStick.v;
        }

        if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Run") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run 0")))
        {
            speedUpTimer = 0;
        }

        if ((Mathf.Abs(h) > 0.1f||Mathf.Abs(v) > 0.1f) && !dodgeFlag)  //判断方向键是否被按下
        {
            dodgeTimeTimer = 0;
            if(speedUpTimer<1.5f)
            {// 移动时间小于1.5秒，角色为行走
                speedUpTimer += Time.deltaTime;
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Run 0"))      //只有正在播放行走动画时才能操作角色，避免边攻击边移动
                {
                    Vector3 targetDir = new Vector3(h, 0, v);
                    transform.LookAt(targetDir + transform.position);           //角色朝向目标位置
                    cc.SimpleMove(targetDir * speed);
                }
            }
            else
            {// 如果移动时间大于1.5秒，角色动作变为跑动
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run 0"))      //只有正在播放行走动画时才能操作角色，避免边攻击边移动
                {
                    Vector3 targetDir = new Vector3(h, 0, v);
                    transform.LookAt(targetDir + transform.position);           //角色朝向目标位置
                    cc.SimpleMove(targetDir * runSpeed);
                }
            }
        }
        else if(dodgeFlag)
        {
            GetComponent<Animator>().ResetTrigger("Attack1");
            GetComponent<Animator>().ResetTrigger("Attack2");
            GetComponent<Animator>().ResetTrigger("Attack3");
            GetComponent<Animator>().ResetTrigger("Attack4");
            GetComponent<Animator>().ResetTrigger("Skill");
            GetComponent<Animator>().ResetTrigger("StrongAtk");
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            dodgeTimeTimer += Time.deltaTime;
            float dodgeSpeed = 2;

            if (dodgeTimeTimer >= dodgeTime)
            {
                dodgeFlag = false;
            }

            Vector3 targetDir = GetComponent<PlayerAtkAndDamage>().targetDir;
            transform.LookAt(targetDir + transform.position);           //角色朝向目标位置
            cc.SimpleMove(targetDir * dodgeSpeed);
        }
        else
        {
            speedUpTimer = 0;
            dodgeTimeTimer = 0;
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }
}
