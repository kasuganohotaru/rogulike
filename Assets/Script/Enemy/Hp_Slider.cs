using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoguLike
{
    public class Hp_Slider : MonoBehaviour
    {
        private Slider HpSlider;
        private Heath heath;

        void Awake()
        {
            HpSlider = GetComponent<Slider>();
            heath = GetComponentInParent<Heath>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
            HpSlider.value = heath.Hp / heath.MaxHP;
        }
    }
}

