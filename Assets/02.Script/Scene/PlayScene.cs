using UnityEngine;

public class PlayScene : BaseScene
{
    float _startTime;
    float _elapsedTime;

    protected override void Init()
    {
        base.Init();

        EventManager.Instance.GameOverEvent -= SetPlayTime;
        EventManager.Instance.GameOverEvent += SetPlayTime;
        SceneType = Define.Scene.Scene2_Play;
        _startTime = Time.time;
    }

    void Update()
    {
        _elapsedTime = Time.time - _startTime;
    }

    public void SetPlayTime()
    {
        GameManager.Instance.PlayTime = _elapsedTime;
    }
    public float GetElapsedTime()
    {
        return _elapsedTime;
    }

    public override void Clear()
    {

    }
}
