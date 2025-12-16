using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : UIBase
{
    [SerializeField] private Button _settingButton;

    [SerializeField] private Image _stackGauge;

    private float _stackCount = 0;

    private void Awake()
    {
        _settingButton.onClick.AddListener(OnClickSetting);
        InitializeStackUI();
    }

    private void OnEnable()
    {
        Manager.Game.turnStack.OnChanged += StackUIUpdate;
    }

    #region StackGauge

    private void InitializeStackUI()
    {
        _stackGauge.fillAmount = _stackCount;
    }

    private void StackUIUpdate(int prev, int cur)
    {
        _stackCount = Manager.Game.turnStack.Current;
        _stackGauge.fillAmount = _stackCount / Manager.Game.Direction;
    }

    #endregion

    private void OnClickSetting()
    {
        Manager.UI.OpenUI<UISettingPopup>(UIType.Popup);
    }
}
