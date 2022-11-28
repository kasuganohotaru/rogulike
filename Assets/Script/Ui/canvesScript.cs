using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helper;
using System.IO;

namespace RoguLike
{
    public class canvesScript : MonoBehaviour
    {
        private GameObject OptionPanle;
        private GameObject PlayerInfoPanel;
        private GameObject settingPanel;

        public static attribute Attribute;

        private void Awake()
        {
            LoadData();

            OptionPanle = transform.Find("OptionPanel").gameObject;
            PlayerInfoPanel = transform.Find("PlayerInfoPanel").gameObject;
            settingPanel = transform.Find("settingPanel").gameObject;
            OptionPanle.SetActive(false);
            PlayerInfoPanel.SetActive(false);

           // SettingPanel.SetActive(true);
        }

        private void Start()
        {
            AudioManager.LoadOption();
            AudioManager.PlayBgm(AudioName.BGM_GameScene1);
            
        }

        public void setting()
        {
            settingPanel.SetActive(true);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (OptionPanle.activeSelf == false)
                    OptionPanle.SetActive(true);

            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (PlayerInfoPanel.activeSelf == false)
                    PlayerInfoPanel.SetActive(true);
            }

            
        }

        private void LoadData()
        {
            string srcFile = Application.dataPath + "/Resources/Data/attribute.json";
            string desFile = Application.persistentDataPath + "/attribute.json";
            if (File.Exists(srcFile))
            {
                if (!File.Exists(desFile))
                {
                    File.Copy(srcFile, desFile);
                }
                string str = FileHelper.ReadFileToJson("/", "attribute.json", FileHelper.FILESRC.PersistentData);
                Attribute = JsonUtility.FromJson<attribute>(str);
            }
            else
            {
                Debug.LogError("用户数据文件不存在");
                return;
            }
        }

        public static void SaveData()
        {
            string srcFile = Application.dataPath + "/Resources/Data/attribute.json";
            string desFile = Application.persistentDataPath + "/attribute.json";
            if (File.Exists(srcFile))
            {
                if (!File.Exists(desFile))
                {
                    File.Copy(srcFile, desFile);
                }
                
                FileHelper.SaveObjToJsonFile("/", "attribute.json",Attribute);
                
            }
        }
    }

}

