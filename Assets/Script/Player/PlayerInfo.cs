using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoguLike
{
    public delegate void SetHpBar(float hp , float MaxHp);

    public class PlayerInfo : MonoBehaviour
    {
        private Heath Hp;

        public static SetHpBar setHpBar;

        private float Exp;
        private float MaxExp;

        void Start()
        {
            Hp = transform.GetComponent<Heath>();
            Hp.MaxHP = canvesScript.Attribute.Heath;
            Exp = canvesScript.Attribute.Exp;
            MaxExp = 100 * canvesScript.Attribute.Level * 1.5f;
        }

        // Update is called once per frame
        void Update()
        {
            setHpBar(Hp.Hp,Hp.MaxHP);
        }
    }
}

