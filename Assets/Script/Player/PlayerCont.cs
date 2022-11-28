using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoguLike
{
    public class PlayerCont : MonoBehaviour
    {
        private Rigidbody PlayerRb;
        private Animator Anim;
        private Transform CheckPos;
        private Vector3 TargetDirection;
        private PlayerState State;
        private GameObject Sword;
        private Transform mapCamTrs;

        [Range(6,10)]
        public float OriginalPlayerSpeed;
        public float JumpHight = 5f;


        private float h;
        private float v;
        private bool j;
        private bool d;
        private float Speed;
        private float JumpColdTime = 1f;
        private float JumpTimer = 0;
        private float DashColdTime = 0.7f;
        public float DashTimer = 0;

        void Start()
        {
            PlayerRb = GetComponent<Rigidbody>();
            State = GetComponent<PlayerState>();
            CheckPos = transform.Find("CheckPos").transform;
            Anim = transform.GetComponent<Animator>();
            Speed = OriginalPlayerSpeed;
            mapCamTrs = transform.Find("MapCam").transform;
            Sword = transform.GetComponentInChildren<SwordInfo>().transform.gameObject;
        }

        
        void Update()
        {
            ProcInput();
            SetAnim();
            SetMiniMap();
            if (!State.isAttack)
                Sword.SetActive(false);
            else
                Sword.SetActive(true);

        }

        private void LateUpdate()
        {
            Jump();
            Dash();
        }

        private void FixedUpdate()
        {
            Movement();
            DetectionGround();
        }

        private void SetMiniMap()
        {
            Quaternion quaternion = new Quaternion();
            quaternion.eulerAngles = new Vector3(90, 0, 0);
            mapCamTrs.rotation = quaternion;
        }

        private void ProcInput()
        {
            
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
                
            j = Input.GetKeyDown(KeyCode.Space);
            d = Input.GetKeyDown(KeyCode.LeftShift);
            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    Speed = OriginalPlayerSpeed * 2f;
            //}
            
        }

        private void Dash()
        {
            if ( !State.isDash )
            {
                if (d && State.isGround && DashTimer <= 0)
                {
                    State.isAttack = false;
                    State.AttactCount = 0;
                    State.isDash = true;
                    //Speed = 12;
                    //if (h < 0.1 && v < 0.1 || h > -0.1 && v > -0.1)
                    //PlayerRb.velocity = transform.forward * Speed;
                    DashTimer = DashColdTime;
                    Anim.SetTrigger("Dash");
                }
                else if(DashTimer > 0)
                {
                    DashTimer -= Time.deltaTime;
                }
            }
           
        }

        private void DashAnimHandel()
        {
            State.isDash = false;
        }

        private void Jump()
        {
            
            if (!State.isJump)
            {
                if (j && State.isGround)
                {
                    State.isJump = true;
                    State.isAttack = false;
                    State.AttactCount = 0;
                    float value = Mathf.Sqrt(2 * -Physics2D.gravity.y * JumpHight * PlayerRb.mass);
                    PlayerRb.velocity += new Vector3(PlayerRb.velocity.x, value, PlayerRb.velocity.z);
                }
            }
            else
            {
                if (JumpTimer < 0)
                {
                    JumpTimer = JumpColdTime;
                    State.isJump = false;
                }
                else
                {
                    JumpTimer -= Time.deltaTime;
                    
                }
            }
            

        }

        private void SetAnim()
        {
            Anim.SetBool("isGround", State.isGround);
            Anim.SetFloat("SpeedY", PlayerRb.velocity.y);
            Anim.SetBool("IsAttack", State.isAttack);
            
        }

        private void DetectionGround()
        {
            RaycastHit hit;
            Ray ray = new Ray(CheckPos.position, Vector3.down);
            Debug.DrawRay(CheckPos.position, Vector3.down * 0.5f ,Color.red);
            if (Physics.Raycast(ray, out hit, 0.5f, 1<<LayerMask.NameToLayer("Scene")))
            {
                State.isGround = true;
            }
            else
            {
                State.isGround = false;
            }
            
        }

        private void Movement()
        {
            
            if (h !=0 || v != 0)
            {
                TargetDirection = new Vector3(h , 0 , v);
                float y = Camera.main.transform.rotation.eulerAngles.y;
                TargetDirection = Quaternion.Euler(0, y, 0) * TargetDirection;
                Rotating(TargetDirection);
                Anim.SetFloat("Speed", Speed);
                if (!State.isAttack)
                {
                    if (!State.isDash)
                        PlayerRb.velocity = new Vector3(TargetDirection.x * Speed, PlayerRb.velocity.y, TargetDirection.z * Speed);
                }
                    //PlayerRb.MovePosition(PlayerRb.position + TargetDirection * Speed * Time.deltaTime);  
            }
            else
            {
                Speed = OriginalPlayerSpeed;
                Anim.SetFloat("Speed", 0);
            }
            
        }



        private void Rotating(Vector3 targetDirection)
        {

            // Create a rotation based on this new vector assuming that up is the global y axis.
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            // Create a rotation that is an increment closer to the target rotation from the player's rotation.
            Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 20f * Time.deltaTime);
            //Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            // Change the players rotation to this new rotation.
            GetComponent<Rigidbody>().MoveRotation(newRotation);
            //transform.rotation = newRotation;
        }
    }
}

