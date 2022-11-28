using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RoguLike
{
    public class Option : MonoBehaviour
    {
        private Button SettingBtn;
        private Button ExitBtn;
        private Button CloseBtn;

        void Awake()
        {
            SettingBtn = transform.Find("Btn_Setting").GetComponent<Button>();
            ExitBtn = transform.Find("Btn_Exit").GetComponent<Button>();
            CloseBtn = transform.Find("Btn_Close").GetComponent<Button>();

            SettingBtn.onClick.AddListener(SettingBtnEvent);
            ExitBtn.onClick.AddListener(ExitBtnEvent);
            CloseBtn.onClick.AddListener(CloseBtnEvent);
        }

        private void SettingBtnEvent()
        {
            AudioManager.PlaySfx(AudioName.SFX_Btn);
            SendMessageUpwards("setting");
        }

        private void ExitBtnEvent()
        {
            canvesScript.SaveData();
            AudioManager.StopBgm(AudioName.BGM_GameScene1);
            AudioManager.PlaySfx(AudioName.SFX_Btn);
            SceneManager.LoadScene("StartScene");
        }

        private void CloseBtnEvent()
        {
            AudioManager.PlaySfx(AudioName.SFX_Btn);
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

