using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

namespace RoguLike
{

    [DefaultExecutionOrder(9999)]
    public class SwordInfo : MonoBehaviour
    {

        public int damage = 1;
        public List<GameObject> obj;
        public bool ishit = false;

        private PlayerState state;
        private Transform swordTrs;

        private void Awake()
        {
            state = GetComponentInParent<PlayerState>();
            obj = new List<GameObject>();
            swordTrs = GameObject.Find("swordmodle").transform;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if(!state.isAttack)
        //    {
        //        return;
        //    }
        //    if(!other.CompareTag("Enemy"))
        //    {
        //        return;
        //    }

        //    target.Add(other);

        //}
        private void FixedUpdate()
        {
            DetectionEnemy();
        }

        private void DetectionEnemy()
        {
            if (!state.isAttack)
            {
               return;
            }
            RaycastHit hit;
            Ray ray = new Ray(transform.position, swordTrs.forward);
            Debug.DrawRay(transform.position, swordTrs.forward * 1.2f, Color.red);
            if (Physics.Raycast(ray, out hit, 1.2f, 1 << LayerMask.NameToLayer("Enemy")))
            {
                if(!FindObjOnList(hit.transform.gameObject))
                    obj.Add(hit.transform.gameObject);
            }
        }

        private bool FindObjOnList(GameObject Object)
        {
            bool isFind = false;
            for(int i=0;i<obj.Count;i++)
            {
                isFind = Object == obj[i].gameObject ?  true :  false;
                if (isFind) break;
            }
            return isFind;
        }

    }
}

