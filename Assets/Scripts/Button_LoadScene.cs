using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_LoadScene : MonoBehaviour
{
    [Min(0)] public int SceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
