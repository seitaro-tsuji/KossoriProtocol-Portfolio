using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private Transform _itemGenerateCandidates;    //アイテム生成の候補地
    [SerializeField] private int _itemNum = 5;
    [SerializeField] private List<ItemControllerBase> _itemPrefabList;

    //ランダムな位置に敵を生成する
    public void GenerateItems()
    {
        //敵生成候補地のリストを作成する
        List<Transform> candidates = new List<Transform>();
        foreach (Transform c in _itemGenerateCandidates)
        {
            candidates.Add(c);
        }

        //指定した個数のインデックスを選択する
        for (int i = 0; i < _itemNum; i++)
        {
            //生成するアイテムをランダムで決定
            var itemPrefab = _itemPrefabList[Random.Range(0, _itemPrefabList.Count)];

            //「i番目」と「i番目からラストまでのどれか」を入れかえる
            int randomIndex = Random.Range(i, candidates.Count);
            var temp = candidates[i];
            candidates[i] = candidates[randomIndex];
            candidates[randomIndex] = temp;

            //i番目を選択して生成
            Vector3 position = candidates[i].position;
            Instantiate(itemPrefab, position, Quaternion.identity);
            Debug.Log("アイテム生成");
        }
    }
}
