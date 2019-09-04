using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkAnimation : MonoBehaviour
{
    private Animator animator;
    private bool Atk2Flag;      //标识普通攻击二段是否可以进行
    private bool Atk3Flag;      //标识普通攻击三段是否可以进行
    private bool Atk4Flag;      //标识普通攻击四段是否可以进行

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        //监听按钮点击事件
        EventDelegate AttackEvent = new EventDelegate(this, "OnAttackClick");
        GameObject.Find("Attack").GetComponent<UIButton>().onClick.Add(AttackEvent);

        EventDelegate SkillEvent = new EventDelegate(this, "OnSkillClick");
        GameObject.Find("Skill").GetComponent<UIButton>().onClick.Add(SkillEvent);

        EventDelegate DodgeEvent = new EventDelegate(this, "OnDodgeClick");
        GameObject.Find("Dodge").GetComponent<UIButton>().onClick.Add(DodgeEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<PlayerAtkAnimation>().OnAttackClick();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<PlayerAtkAnimation>().OnSkillClick();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<PlayerAtkAnimation>().OnDodgeClick();
        }
    }

    public void OnAttackClick()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Run 0"))
        {
            animator.SetTrigger("StrongAtk");
        }
        else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk1") && Atk2Flag)
            {
                animator.SetTrigger("Attack2");
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk2") && Atk3Flag)
            {
                animator.SetTrigger("Attack3");
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttk3") && Atk4Flag)
            {
                animator.SetTrigger("Attack4");
            }
            else
            {
                animator.SetTrigger("Attack1");
            }
        }
        
    }

    public void OnSkillClick()
    {
        animator.SetTrigger("Skill");
    }

    public void OnDodgeClick()
    {
        animator.SetTrigger("DodgeFront");
    }

    public void StrongAtkEnd()
    {
        animator.ResetTrigger("StrongAtk");
    }

    public void SkillEnd()
    {
        animator.ResetTrigger("Skill");
    }

    public void Atk1End()
    {
        animator.ResetTrigger("Attack1");
    }

    //判断是否能够使用普通攻击二段
    public void Atk2Start()
    {
        GetComponent<Animator>().ResetTrigger("Attack1");
        GetComponent<Animator>().ResetTrigger("Skill");
        GetComponent<Animator>().ResetTrigger("StrongAtk");
        Atk2Flag = true;
    }

    public void Atk2End()
    {
        GetComponent<Animator>().ResetTrigger("Attack2");
        Atk2Flag = false;
    }

    public void Atk3Start()
    {
        GetComponent<Animator>().ResetTrigger("Attack2");
        Atk3Flag = true;
    }

    public void Atk3End()
    {
        GetComponent<Animator>().ResetTrigger("Attack3");
        Atk3Flag = false;
    }

    public void Atk4Start()
    {
        GetComponent<Animator>().ResetTrigger("Attack3");
        Atk4Flag = true;
    }

    public void Atk4End()
    {
        GetComponent<Animator>().ResetTrigger("Attack4");
        Atk4Flag = false;
    }
}
