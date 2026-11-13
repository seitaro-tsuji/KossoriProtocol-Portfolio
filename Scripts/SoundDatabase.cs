using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundNameEnum
{
    TitleBGM,
    InGameBGM,
    ResultBGM,
    FailureSE,
    SirenSE,
    SelectSE,
    GetItemSE,
    QuestionSE
}

//サウンドの名前とクリップ、ループ再生するかどうかの対応
[Serializable]
public class SoundNameClip
{
    public SoundNameEnum nameEnum;
    public AudioClip clip;
    public bool isOneShot;
}

//上のクラスをまとめたリストを持つ
[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Scriptable Objects/SoundDatabase")]
public class SoundDatabase : ScriptableObject
{
    [SerializeField] private List<SoundNameClip> database;

    //enumからサウンドデータを取得
    public SoundNameClip GetSoundNameClip(SoundNameEnum name)
    {
        return database.Find(x => x.nameEnum == name);
    }
}
