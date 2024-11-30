using TMPro;
using UnityEngine;

public class EndScene : BaseScene
{
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _playTime;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Scene3_End;
        SetUI();
    }
    public void SetUI()
    {
        _nameText.text = GameManager.Instance.PlayerName;
        _playTime.text = GameManager.Instance.PlayTime.ToString();
    }
    public override void Clear()
    {

    }
}
