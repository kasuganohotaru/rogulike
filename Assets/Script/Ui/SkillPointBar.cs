using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RoguLike
{
    public class SkillPointBar : MonoBehaviour
    {
        //组件
        private Text attributeNameTxt;
        private Text attributePointTxt;
        private Button plusBtn;
        private Button subBtn;
        //参数
        private int currentPoint = 1;//当前点数
        private int remainPoint = 10;//剩余点数
        public string attName;//技能名称
        private int oriValue;//原始值
        public string AttName
        {
            set
            {
                attName = value;
                if (attributeNameTxt != null) 
                    attributeNameTxt.text = attName;
            }
        }

        public int CurrentPoint {
            get => currentPoint;
            private set {
                currentPoint = value;
                //oriValue = currentPoint;
                if (attributePointTxt != null)
                    attributePointTxt.text = "" + currentPoint;
            }
        }

        public void InitCurrentPoint(int value)
        {
            currentPoint = value;
            oriValue = currentPoint;
            if (attributePointTxt != null)
                attributePointTxt.text = "" + currentPoint;
        }

        public int RemainPoint {
            get => remainPoint;
            set
            {
                remainPoint = value;               
                if(remainPoint == 0)               
                    plusBtn.image.enabled = false;               
                if(currentPoint == oriValue)
                    subBtn.image.enabled = false;
                if(remainPoint>0)
                    plusBtn.image.enabled = true;
                if (currentPoint > oriValue)
                    subBtn.image.enabled = true;               
            }
        }

        void Awake()
        {
            InitComp();
        }

        private void InitComp()
        {
            attributeNameTxt = transform.Find("Text_SkillPointName").GetComponent<Text>();
            attributePointTxt = transform.Find("Slot/Text_Point").GetComponent<Text>();
            plusBtn = transform.Find("Buttons/PlusButton").GetComponent<Button>();
            subBtn = transform.Find("Buttons/SubButton").GetComponent<Button>();
            if (attributeNameTxt == null || attributePointTxt == null || plusBtn == null || subBtn == null)
            {
                Debug.LogError("找不到对应的组件，请检查组件名称");
                return;
            }
            AttName = attName;
            CurrentPoint = currentPoint;
            oriValue = currentPoint;//保存原始值
            plusBtn.onClick.AddListener(plusClickHandler);
            subBtn.onClick.AddListener(subClickHandler);
            subBtn.image.enabled = false;
        }

        private void plusClickHandler()
        {
            if(remainPoint>0)
            {
                CurrentPoint++;
                remainPoint--;
                if (!subBtn.image.enabled)//如果减号按钮不可见
                    subBtn.image.enabled = true;
                if (remainPoint <= 0)
                    plusBtn.image.enabled = false;
                //SendMessageUpwards("SkillPointChanged", -1);
            }            
        }

        private void subClickHandler()
        {
            if (currentPoint > oriValue)
            {
                CurrentPoint--;
                remainPoint++;
                if (!plusBtn.image.enabled)//如果减号按钮不可见
                    plusBtn.image.enabled = true;
                if (currentPoint == oriValue)
                    subBtn.image.enabled = false;
                //SendMessageUpwards("SkillPointChanged", 1);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

