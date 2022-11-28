using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RoguLike
{
    [Serializable]
    public class attribute
    {
        public float Level; // 等级
        public float Point; // 技能点
        public float Exp; // 经验
        public float Heath; // 生命
        public float Defense; // 防御
        public float Damage; // 攻击
        public float CriticalHitRate; // 暴击率
        public float CriticalHitDamage; // 暴击伤害
        public float STR; // 力量
        public float AGI; // 灵巧
        public float VIT; // 耐力
        public float DEX; // 敏捷
        public float LUK; // 幸运
    }
}

