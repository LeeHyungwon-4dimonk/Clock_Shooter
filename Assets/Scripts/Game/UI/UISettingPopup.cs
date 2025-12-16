using UnityEngine;
using UnityEngine.UI;

public class UISettingPopup : UIBase
{
    [SerializeField] private Button _resumeButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(OnClickResume);
    }

    private void OnClickResume()
    {
        Manager.UI.CloseUI<UISettingPopup>();
    }
}
