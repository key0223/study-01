using UnityEngine;
using UnityEngine.UI;
public class UI_PlayerName : MonoBehaviour
{
    [SerializeField] InputField _nameInput;
    [SerializeField] Button _confirmButton;

    string _name;

    void Awake()
    {
        _name = _nameInput.GetComponent<InputField>().text;
        _nameInput.onValueChanged.AddListener(delegate { InputText(); });

        _confirmButton.onClick.AddListener(() => OnConfirmButtonClicked());
    }

    public void InputText()
    {
        _name = _nameInput != null ? _nameInput.text : "";
        _confirmButton.interactable = _name.Length > 0 && _name.Length > 0;
    }

    void OnConfirmButtonClicked()
    {
        string uniqueId = _name;
        GameManager.Instance.PlayerName = _name;
        LoadScene();
    }

    void LoadScene()
    {
        SceneManagerEx.Instance.LoadScene(Define.Scene.Scene2_Play);
    }
}
