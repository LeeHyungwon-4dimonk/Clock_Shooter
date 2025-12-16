using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : UIBase
{
    [SerializeField] private Button _settingButton;

    private void Awake()
    {
        _settingButton.onClick.AddListener(OnClickSetting);
    }

    private void OnClickSetting()
    {
        Manager.UI.OpenUI<UISettingPopup>(UIType.Popup);
    }
}
