using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoguLike
{
    public class SettingPanel : MonoBehaviour
    {
        private Slider BgmSlider;
        private Slider SfxSlider;
        private Toggle BgmToggle;
        private Toggle SfxToggle;
        private Button BackBtn;

        void Awake()
        {
            BgmSlider = transform.Find("Panel/BGM_Slider").GetComponent<Slider>();
            SfxSlider = transform.Find("Panel/SFX_Slider").GetComponent<Slider>();
            BgmToggle = transform.Find("Panel/BGM_Toggle").GetComponent<Toggle>();
            SfxToggle = transform.Find("Panel/SFX_Toggle").GetComponent<Toggle>();
            BackBtn = transform.Find("Panel/Back_Btn").GetComponent<Button>();    
        }

        private void Start()
        {
            BgmSlider.onValueChanged.AddListener(SliderBgmChanged);
            SfxSlider.onValueChanged.AddListener(SliderSfxChanged);

            BgmToggle.onValueChanged.AddListener(ToggleBgmChanged);
            SfxToggle.onValueChanged.AddListener(ToggleSfxChanged);

            BackBtn.onClick.AddListener(Btn_Back);

            BgmSlider.value = AudioManager.Instance.BgmVolume;
            SfxSlider.value = AudioManager.Instance.SfxVolume;
            BgmToggle.isOn = AudioManager.Instance.IsOpenBGM;
            SfxToggle.isOn = AudioManager.Instance.IsOpenSFX;
        }


        public void SliderBgmChanged(float Value)
        {
            Value = BgmSlider.value;
            AudioManager.setBgmVolume(Value);
        }

        public void SliderSfxChanged(float Value)
        {
            Value = SfxSlider.value;
            AudioManager.setSfxVolume(Value);
            AudioManager.PlaySfx(AudioName.SFX_Btn);
        }

        public void ToggleBgmChanged(bool value)
        {
            value = BgmToggle.isOn;
            AudioManager.setIsOpenBGM(value);
            if (value)
                AudioManager.PlayBgm(AudioName.BGM_GameScene1);
            else
                AudioManager.StopBgm(AudioName.BGM_GameScene1);
        }

        public void ToggleSfxChanged(bool value)
        {
            value = SfxToggle.isOn;
            AudioManager.setIsOpenSFX(value);
        }

        public void Btn_Back()
        {
            AudioManager.PlaySfx(AudioName.SFX_Btn);
            AudioManager.SaveOption();
            transform.gameObject.SetActive(false);
        }
    }
}

