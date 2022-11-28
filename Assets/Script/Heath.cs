using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

namespace RoguLike
{
    public class Heath : MonoBehaviour,IDamageable
    {

        [Header("生命值")]
        [Range(1, 99999)]
        public float MaxHP;//最大生命

        private float HP;//当前生命

        public float Hp { get => HP; }

       


        public void Damage(float amount)
        {
            HP -= amount;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }

        // Start is called before the first frame update
        void Awake()
        {
            HP = MaxHP;
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

