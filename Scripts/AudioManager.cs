using System.Data;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource bgmSourceForSiren; //サイレン専用のsource
    [SerializeField] private AudioSource seSource;

    [SerializeField] private SoundDatabase soundDatabase;

    private float _mainBGMVolume;

    static public AudioManager Instance { get; private set; }

    private void Awake()
    {
        //シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //常駐
        DontDestroyOnLoad(gameObject);

        //メインBGMの音量を保持しておく
        _mainBGMVolume = bgmSource.volume;
    }

    //enumで選択してBGMを流す
    public void PlayBGM(SoundNameEnum nameEnum)
    {
        SoundNameClip dataset = soundDatabase.GetSoundNameClip(nameEnum);
        if (dataset.isOneShot)  //一回だけ流すタイプなら終了
            return;

        AudioClip clip = dataset.clip;

        PlayBGM(clip);
    }

    //enumで選択してSEを流す
    public void PlaySEOneShot(SoundNameEnum nameEnum)
    {
        SoundNameClip dataset = soundDatabase.GetSoundNameClip(nameEnum);
        if (!dataset.isOneShot)  //一回だけ流すタイプじゃないなら終了
            return;

        //?のseを流す時だけ音量１で、それ以外は0.5
        seSource.volume = nameEnum == SoundNameEnum.QuestionSE ? 1f : 0.5f;

        AudioClip clip = dataset.clip;

        PlaySEOneShot(clip);
    }

    //警報を鳴らす
    public void PlaySirenBGM()
    {
        SoundNameClip dataset = soundDatabase.GetSoundNameClip(SoundNameEnum.SirenSE);
        bgmSourceForSiren.clip = dataset.clip;
        bgmSourceForSiren.Play();

        //メインBGMの音量下げるかも
        bgmSource.volume = _mainBGMVolume / 2;
    }

    //警報を止める
    public void StopSirenBGM()
    {
        bgmSourceForSiren.Stop();

        //音量戻す
        bgmSource.volume = _mainBGMVolume;
    }

    public void PlayBGM(AudioClip bgmClip) 
    {
        //bgmを流す
        if (bgmSource == null)
            return;

        bgmSource.Stop();
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void PlaySEOneShot(AudioClip seClip)
    {
        //seを流す
        if (seSource == null)
            return;

        seSource.PlayOneShot(seClip);
    }
}
