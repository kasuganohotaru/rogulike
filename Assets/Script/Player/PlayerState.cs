using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoguLike
{
    public class PlayerState : MonoBehaviour
    {
        public bool isGround = true;
        public bool isDash = false;
        public bool isJump = false;
        public bool isAttack = false;
        public bool OnAttack = false;
        public int AttactCount = 0;
    }
}

