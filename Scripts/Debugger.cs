using UnityEngine;
using UnityEngine.InputSystem;

public class Debugger : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("アウェイク");
    }
    private void Update()
    {
        //fキー押したとき
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            AudioManager.Instance.PlaySirenBGM();
        }

        //gキー押したとき
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            AudioManager.Instance.StopSirenBGM();
        }
    }

 
}
