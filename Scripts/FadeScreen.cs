using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.5f;

    //透過度
    private float Alpha
    {
        get
        {
            return GetComponent<Image>().color.a;
        }
        set
        {
            Image image = GetComponent<Image>();
            Color c = image.color;
            c.a = value;
            image.color = c;
        }
    }

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    //フェードイン
    public void FadeIn()
    {
        _image.DOFade(0f, _fadeDuration);
    }

    //フェードアウト
    public void FadeOut()
    {
        _image.DOFade(1f, _fadeDuration);
    }
}
