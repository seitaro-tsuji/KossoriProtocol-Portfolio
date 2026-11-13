using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    //ステージ開始からの時間(秒)
    private float _timeSec = 0f;
    public float TimeSec => _timeSec;

    private bool _counting;
    public bool Counting => _counting;

    private void Awake()
    {
        //時間カウントを最初から開始する(演出などを入れる場合はそのあとからに変更する)
        _counting = false;
    }

    void Update()
    {
        //カウント中は時間を加算
        if (_counting)
            _timeSec += Time.deltaTime;
    }

    //時間カウントの開始、停止を切り替え
    public void SetCounting(bool value)
    {
        _counting = value;
    }
}
