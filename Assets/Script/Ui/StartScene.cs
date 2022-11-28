using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RoguLike
{
    public class StartScene : MonoBehaviour
    {
        private Button btn_Start;
        private Button btn_Setting;
        private Button Btn_Exit;

        private GameObject settingPanel;

        void Start()
        {
            btn_Start = transform.Find("Panel/Btn_Start").GetComponent<Button>();
            btn_Setting = transform.Find("Panel/Btn_Setting").GetComponent<Button>();
            Btn_Exit = transform.Find("Panel/Btn_Exit").GetComponent<Button>();

            settingPanel = transform.Find("Panel/settingPanel").gameObject;
            settingPanel.SetActive(false);

            btn_Start.onClick.AddListener(StartGame);
            btn_Setting.onClick.AddListener(Setting);
            Btn_Exit.onClick.AddListener(Exit);

            AudioManager.LoadOption();
            AudioManager.PlayBgm(AudioName.BGM_NoGameScene);
        }


        private void StartGame()
        {
            AudioManager.PlaySfx(AudioName.SFX_Btn);
            AudioManager.StopBgm(AudioName.BGM_NoGameScene);
            SceneManager.LoadScene("GameScene");
        }

        private void Setting()
        {
            AudioManager.PlaySfx(AudioName.SFX_Btn);
            settingPanel.SetActive(true);
        }

        private void Exit()
        {
            AudioManager.StopBgm(AudioName.BGM_NoGameScene);
            #if UNITY_EDITOR//在编辑器模式退出
                UnityEditor.EditorApplication.isPlaying = false;
            #else//发布后退出
                 Application.Quit();
            #endif
        }


    }
}

