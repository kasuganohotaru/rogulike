using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoguLike
{
    public class Enemy : MonoBehaviour
    {

        private Heath Hp;

        void Start()
        {
            Hp = transform.GetComponent<Heath>();
        }


        void Update()
        {
            if(!Hp.IsAlive())
            {
                Destroy(gameObject);
            }
        }


    }
}

