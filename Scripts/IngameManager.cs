using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class IngameManager : MonoBehaviour
{
    public static IngameManager Instance { get; private set; }

    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private ItemGenerator _itemGenerator;

    private bool _started = false;  //ゲーム開始済みか(フェードインなどが終了した後にtrueになる)

    private void Awake()
    {
        //シングルトン
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //フレームレートを固定する
        Application.targetFrameRate = 60;

        //タイムは最初はカウントしない状態にする
        StopTimeCount();
    }

    private void Start()
    {
        //BGM開始
        AudioManager.Instance.PlayBGM(SoundNameEnum.InGameBGM);
    }

    private void Update()
    {
        //時間表示の更新
        _timeText.text = GetTimeCount().ToString("F2");

        //ゲーム開始処理(1回だけ)
        if (!SceneChanger.Instance.IsFading && !_started)
        {
            GameStart();
        }

        //キー入力でリスタートとタイトルボタンを押せるように
        //Enterでリプレイ
        if (Keyboard.current.enterKey.wasPressedThisFrame && !SceneChanger.Instance.IsFading)
        {
            SceneChanger.Instance.SceneChange("InGameScene");
            AudioManager.Instance.PlaySEOneShot(SoundNameEnum.SelectSE);
            GameData.ClearInTimes = 0;
        }
        //Escでタイトル
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !SceneChanger.Instance.IsFading)
        {
            SceneChanger.Instance.SceneChange("Title");
            AudioManager.Instance.PlaySEOneShot(SoundNameEnum.SelectSE);
            GameData.ClearInTimes = 0;
        }
    }

    //ゲームスタート時に呼ぶ関数
    public void GameStart()
    {
        _started = true;
        StartTimeCount();

        _enemyManager.GenerateEnemies();
        _itemGenerator.GenerateItems();
        
    }

    //ゲームクリア時に呼ぶ関数
    public void GameClear()
    {
        StopTimeCount();

        //演出

        //リザルトシーンを呼ぶ(クリアフラグ付き)
        SceneChanger.Instance.SceneChange("Result");
        GameData.ClearTime = GetTimeCount();
        GameData.IsClear = true;
        GameData.ClearInTimes += 1;
    }

    //ゲーム失敗時に呼ぶ関数(敵に見つかった時)
    public void GameFailure()
    {
        //時間カウント終了
        _timeCounter.SetCounting(false);

        //演出

        //リザルトシーンを呼ぶ(失敗フラグ付き)
        SceneChanger.Instance.SceneChange("Result");
        GameData.ClearTime = GetTimeCount();
        GameData.IsClear = false;
        GameData.ClearInTimes = 0;
    }

    //全部の敵を止める stopはスタン時間、flashは最後の何秒点滅するか
    public void StunAllEnemies(float stopSec, float flashSec=1f)
    {
        _enemyManager.StunAllEnemies(stopSec, flashSec);
    }

    public float GetTimeCount()
    {
        return _timeCounter.TimeSec;
    }

    public void StopTimeCount() => _timeCounter.SetCounting(false);
    public void StartTimeCount() => _timeCounter.SetCounting(true);

}
