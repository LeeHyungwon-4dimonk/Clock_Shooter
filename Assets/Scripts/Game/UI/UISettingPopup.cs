using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISettingPopup : UIBase
{
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()
    {
        Manager.UI.CloseUI<UISettingPopup>();
    }
}
