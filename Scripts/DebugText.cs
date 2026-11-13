using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = $"{IngameManager.Instance.GetTimeCount()}";
    }
}
