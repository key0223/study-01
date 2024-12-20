using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerEx : SingletonMonobehaviour<SceneManagerEx>
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        GameManager.Instance.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
