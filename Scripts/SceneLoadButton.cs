using UnityEngine;
using UnityEngine.UI;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;

    private Button _button;

    private void Start()
    {
        //if (SceneChanger.Instance.IsFading) return;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);

    }

    private void LoadScene()
    {
        if (SceneChanger.Instance.IsFading) return;
        AudioManager.Instance.StopSirenBGM();
        AudioManager.Instance.PlaySEOneShot(SoundNameEnum.SelectSE);
        SceneChanger.Instance.SceneChange(_nextSceneName);
    }
}
