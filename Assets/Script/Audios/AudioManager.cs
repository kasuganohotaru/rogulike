using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioInfo
    {
        [Header("��Ƶ����")]
        public AudioClip clip;
        [Header("������")]
        public AudioMixerGroup MixerGroup;
        [Header("�Ƿ�ѭ������")]
        public bool isLoop;
        [Header("����")]
        [Range(0f,1f)]
        public float Volume = 1;
    }

    public AudioInfo[] audioInfos;

    [Header("ȫ�ֻ�����")]
    public AudioMixer mixer;

    private Dictionary<string, AudioSource> audioDic;

    private static AudioManager _instance;

    private float _bgmVolume = 0;
    private float _sfxVolume = 0;
    private bool _isOpenBGM = true;
    private bool _isOpenSFX = true;


    public static AudioManager Instance { get => _instance; }
    public float BgmVolume { get => _bgmVolume; }
    public float SfxVolume { get => _sfxVolume;  }
    public bool IsOpenBGM { get => _isOpenBGM; set => _isOpenBGM = value; }
    public bool IsOpenSFX { get => _isOpenSFX; set => _isOpenSFX = value; }

    public static void setBgmVolume(float value)
    {
        if (value > 0 || value < -80)
            return;
        _instance._bgmVolume = value;
        _instance.mixer.SetFloat("BgmVolume", value);
    }

    public static void setSfxVolume(float value)
    {
        if (value > 0 || value < -80)
            return;
        _instance._sfxVolume = value;
        _instance.mixer.SetFloat("SfxVolume", value);
    }

    public static void setIsOpenBGM(bool Value)
    {
        _instance._isOpenBGM = Value;
    }

    public static void setIsOpenSFX(bool value)
    {
        _instance._isOpenSFX = value;
    }

    public static void LoadOption()//����ע�������
    {
        if(PlayerPrefs.HasKey("BgmVolume")&& PlayerPrefs.HasKey("SfxVolume") && PlayerPrefs.HasKey("IsOpenBGM") && PlayerPrefs.HasKey("IsOpenSFX"))
        {
            _instance._bgmVolume = PlayerPrefs.GetFloat("BgmVolume");
            _instance._sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            _instance._isOpenBGM = PlayerPrefs.GetInt("IsOpenBGM") == 1 ? true : false;
            _instance._isOpenSFX = PlayerPrefs.GetInt("IsOpenSFX") == 1 ? true : false;
            //���̺����û�����
            _instance.mixer.SetFloat("BgmVolume", _instance._bgmVolume);
            _instance.mixer.SetFloat("SfxVolume", _instance._sfxVolume);
        }
        
    }

    public static void SaveOption()
    {
        PlayerPrefs.SetFloat("BgmVolume", _instance._bgmVolume);
        PlayerPrefs.SetFloat("SfxVolume", _instance._sfxVolume);
        PlayerPrefs.SetInt("IsOpenBGM", _instance._isOpenBGM ? 1 : 0);
        PlayerPrefs.SetInt("IsOpenSFX", _instance._isOpenSFX ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
            audioDic = new Dictionary<string, AudioSource>();
            InitManager();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void InitManager()
    {
        for(int i=0;i<audioInfos.Length;i++)
        {
            //�����¶���
            GameObject Obj = new GameObject();
            Obj.name = audioInfos[i].clip.name;
            Obj.transform.parent = transform;
            //�ڶ������������
            AudioSource source = Obj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = audioInfos[i].clip;
            source.outputAudioMixerGroup = audioInfos[i].MixerGroup;
            source.loop = audioInfos[i].isLoop;
            source.volume = audioInfos[i].Volume;
            audioDic.Add(Obj.name, source);

        }
    }

    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="Name">��������</param>

    public static void PlayBgm(string Name)
    {
        if(!_instance.audioDic.ContainsKey(Name))
        {
            Debug.LogError("�Ҳ���ָ����Ƶ");
            return;
        }
        if(_instance.IsOpenBGM)
            _instance.audioDic[Name].Play();
    }

    /// <summary>
    /// ֹͣ���ű�������
    /// </summary>
    /// <param name="Name">��������</param>

    public static void StopBgm(string Name)
    {
        if (!_instance.audioDic.ContainsKey(Name))
        {
            Debug.LogError("�Ҳ���ָ����Ƶ");
            return;
        }
        if(_instance!=null)
            _instance.audioDic[Name].Stop();
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="Name">��Ч����</param>

    public static void PlaySfx(string Name)
    {
        if (!_instance.audioDic.ContainsKey(Name))
        {
            Debug.LogError("�Ҳ���ָ����Ƶ");
            return;
        }
        if(_instance.IsOpenSFX)
            _instance.audioDic[Name].Play();
    }
}
