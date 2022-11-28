using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoguLike
{
    public class HpSlider : MonoBehaviour
    {
        private Slider slider;
        private Text text;

        void Start()
        {
            slider = transform.GetComponent<Slider>();
            text = transform.Find("Text").GetComponent<Text>();

            PlayerInfo.setHpBar += setBar;
        }

        private void setBar(float hp, float maxhp)
        {
            text.text = "" + hp + "/" + maxhp;
            slider.value = hp / maxhp;
        }
    }
}

