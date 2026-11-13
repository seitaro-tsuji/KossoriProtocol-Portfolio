using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }

    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private float _fadeDurationSec=0.5f;

    private bool _isFading;
    public bool IsFading => _isFading;

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

        _isFading = false;
    }

    public void SceneChange(string nextSceneName)
    {
        //フェードイン、アウト用のImage
        Image fadeScreenImage = _fadeScreen.GetComponent<Image>();
        Sequence sequence = DOTween.Sequence();

        //最初にフェード中にしておく
        _isFading = true;

        sequence
            .Append(fadeScreenImage.DOFade(1f, _fadeDurationSec))    //フェードアウト
            .AppendCallback(() =>
            {
                SceneManager.LoadScene(nextSceneName);              //シーン遷移
            })
            .AppendInterval(0.1f)                               //0.1s待つ
            .Append(fadeScreenImage.DOFade(0f, _fadeDurationSec))  //フェードイン
            .AppendCallback(() => _isFading = false);                   //フェード終了
    }
}
