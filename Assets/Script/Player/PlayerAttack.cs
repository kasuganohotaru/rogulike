using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

namespace RoguLike
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator Anim;
        private AnimatorStateInfo AnimState;
        //private int AttactCount = 0;
        private PlayerState PlayerSta;
        private SwordInfo swordInfo;


        private float timer = 0;

        void Start()
        {
            swordInfo = GetComponentInChildren<SwordInfo>();
            Anim = GetComponent<Animator>();
            PlayerSta = GetComponent<PlayerState>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 0) return;
            procAtt();
            setAnim();
        }

        private void procAtt()
        {
            AnimState = Anim.GetCurrentAnimatorStateInfo(1);
            if (Input.GetMouseButtonDown(0) && PlayerSta.isGround)
            {
                PlayerSta.isAttack = true;
                Attack();
            }
            if (!AnimState.IsName("Empty") && AnimState.normalizedTime > 1f )
            {
                PlayerSta.AttactCount = 0;
                
                PlayerSta.isAttack = false;
                
            }

            if (PlayerSta.OnAttack)
            {
                if (timer < 5f)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    PlayerSta.OnAttack = false;
                    timer = 0;
                }
            }
        }

        private void setAnim()
        {
            Anim.SetBool("OnAttack", PlayerSta.OnAttack);
            Anim.SetBool("IsAttack", PlayerSta.isAttack);
            Anim.SetInteger("Attack", PlayerSta.AttactCount);
        }

        private void Attack()
        {
            if (AnimState.IsName("Empty") && PlayerSta.AttactCount == 0 && AnimState.normalizedTime > 0f )
            {
                PlayerSta.OnAttack = true;
                timer = 0;      
                PlayerSta.AttactCount = 1;
            }

            if (AnimState.IsName("R_Attack_09") && PlayerSta.AttactCount == 1 && AnimState.normalizedTime > 0.35f  )
            {
                PlayerSta.OnAttack = true;
                timer = 0;
                PlayerSta.AttactCount = 2;
            }

            if (AnimState.IsName("R_Attack_08") && PlayerSta.AttactCount == 2 && AnimState.normalizedTime > 0.35f )
            {
                PlayerSta.OnAttack = true;
                timer = 0;
                PlayerSta.AttactCount = 3;
            }

            if (AnimState.IsName("R_Attack_01") && PlayerSta.AttactCount == 3 && AnimState.normalizedTime > 0.45f)
            {
                PlayerSta.OnAttack = true;
                timer = 0;
                PlayerSta.AttactCount = 1;
            }
        }

        private void DamageEventHandel()
        {
            //swordInfo = GetComponentInChildren<SwordInfo>();
            if(swordInfo.obj!=null)
            {
                if (swordInfo.ishit) return;
                for(int i=0;i<swordInfo.obj.Count;i++)
                {
                    IDamageable damageable = swordInfo.obj[i].transform.GetComponent<IDamageable>();
                    damageable.Damage(swordInfo.damage);
                }
                swordInfo.ishit = true;
               
            }
        }

        private void clearDamageTarget()
        {
            if(swordInfo.obj != null)
            {
                swordInfo.ishit = false;
                swordInfo.obj.Clear();
            }
                
        }
    }
}

