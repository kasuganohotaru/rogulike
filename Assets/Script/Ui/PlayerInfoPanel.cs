using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoguLike
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        private Button CloseBtn;
        private Text Text_Level;
        private Text Text_Exp;
        private Text Text_point;
        private Text Text_MaxHp;
        private Text Text_defense;
        private Text Text_Damage;
        private Text Text_CriticalHitRate;
        private Text Text_CriticalHitDamage;

        private canvesScript CanvesScript;

        void Awake()
        {
            init();

            changeInfo();
        }

        // Update is called once per frame
        void Update()
        {
            if(transform.gameObject.activeSelf)
                changeInfo();
        }

        private void init()
        {
            CloseBtn = transform.Find("Btn_Close").GetComponent<Button>();
            CloseBtn.onClick.AddListener(CloseBtnEvent);
            CanvesScript = GetComponentInParent<canvesScript>();
            Text_Level = transform.Find("PlayerInfoArea/Text_Level").GetComponent<Text>();
            Text_Exp = transform.Find("PlayerInfoArea/Text_Exp").GetComponent<Text>();
            Text_point = transform.Find("PlayerInfoArea/Text_point").GetComponent<Text>();
            Text_MaxHp = transform.Find("PlayerInfoArea/Text_MaxHp").GetComponent<Text>();
            Text_defense = transform.Find("PlayerInfoArea/Text_defense").GetComponent<Text>();
            Text_Damage = transform.Find("PlayerInfoArea/Text_Damage").GetComponent<Text>();
            Text_CriticalHitRate = transform.Find("PlayerInfoArea/Text_CriticalHitRate").GetComponent<Text>();
            Text_CriticalHitDamage = transform.Find("PlayerInfoArea/Text_CriticalHitDamage").GetComponent<Text>();
        }

        private void changeInfo()
        {
            
            Text_Level.text = "" + canvesScript.Attribute.Level;
            Text_Exp.text = "" + canvesScript.Attribute.Exp;
            Text_point.text = "" + canvesScript.Attribute.Point;
            Text_MaxHp.text = "" + canvesScript.Attribute.Heath;
            Text_defense.text = "" + canvesScript.Attribute.Defense;
            Text_Damage.text = "" + canvesScript.Attribute.Damage;
            Text_CriticalHitRate.text = "" + canvesScript.Attribute.CriticalHitRate+"%";
            Text_CriticalHitDamage.text = "" + canvesScript.Attribute.CriticalHitDamage+"%";
        }

        private void CloseBtnEvent()
        {
            canvesScript.SaveData();
            transform.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}

