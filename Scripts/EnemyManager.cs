using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyGenerateCandidates;    //敵生成の候補地
    [SerializeField] private int _enemyNum = 5;
    [SerializeField] private Enemy1 _enemyPrefab;

    List<Enemy1> enemyList = new List<Enemy1>();

    Sequence _sequence;

    //ランダムな位置に敵を生成する
    public void GenerateEnemies()
    {
        //敵生成候補地のリストを作成する
        List<Transform> candidates = new List<Transform>();
        foreach (Transform c in _enemyGenerateCandidates)
        {
            candidates.Add(c);
        }

        //指定した個数のインデックスを選択する
        for(int i = 0; i < _enemyNum; i++)
        {
            //「i番目」と「i番目からラストまでのどれか」を入れかえる
            int randomIndex = Random.Range(i, candidates.Count);
            var temp = candidates[i];
            candidates[i] = candidates[randomIndex];
            candidates[randomIndex] = temp;

            //i番目を選択して生成
            Vector3 position = candidates[i].position;
            var enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, transform);
            enemyList.Add(enemy);
        }
    }

    //flashが残り秒数
    public void StunAllEnemies(float stopSec, float flashSec)
    {
        if (flashSec > stopSec)
            flashSec = 0.5f * stopSec;

        if(_sequence != null && _sequence.IsActive())
        {
            _sequence.Kill();
        }

        _sequence = DOTween.Sequence();

        _sequence
            .AppendCallback(() => StopAllEnemies())   //全部の敵を止める
            .AppendInterval(stopSec - flashSec)     //点滅開始まで待つ
            .AppendCallback(() => StartFlashAllEnemies()) //点滅開始
            .AppendInterval(flashSec)               //点滅時間待つ
            .AppendCallback(() => StartAllEnemies());   //全部の敵の停止を解除
    }

    private void StopAllEnemies()
    {
        foreach(var enemy in enemyList)
        {
            enemy.ChangeState(EnemyState.Stun);
        }
    }

    private void StartAllEnemies()
    {
        //敵を動かして点滅も終了
        foreach (var enemy in enemyList)
        {
            enemy.ChangeState(EnemyState.Patrol);
        }
    }

    private void StartFlashAllEnemies()
    {
        foreach(var enemy in enemyList)
        {
            enemy.BeginBodyBlinking();
        }

    }
}
