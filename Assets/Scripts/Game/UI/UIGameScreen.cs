using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : UIBase
{
    [SerializeField] private Button _settingButton;

    [SerializeField] private Image[] _stackGaugeImgs;

    [SerializeField] private Color[] _gaugeColors;
    [SerializeField] private Color _emptyColor = Color.black;

    private float _stackCount = 0;
    private float _dir;

    private void Awake()
    {
        _settingButton.onClick.AddListener(OnClickSetting);
        _dir = Manager.Game.Direction;
        StackUIInitialize();
    }

    private void OnEnable()
    {
        Manager.Game.turnStack.OnChanged += StackUIUpdate;
    }

    #region StackGauge

    private void StackUIInitialize()
    {
        for(int i = 0; i < _stackGaugeImgs.Length; i++)
        {
            _stackGaugeImgs[i].color = _emptyColor;
            _stackGaugeImgs[i].fillAmount = 1;
        }
    }

    private void StackUIUpdate(int prev, int cur)
    {
        _stackCount = Manager.Game.turnStack.Current;

        int colorIndex = Mathf.FloorToInt(_stackCount / _dir);

        _stackGaugeImgs[0].color = 0 <= colorIndex? _gaugeColors[colorIndex % _gaugeColors.Length] : _emptyColor;
        _stackGaugeImgs[1].color = 1 <= colorIndex ? _gaugeColors[(colorIndex - 1) % _gaugeColors.Length] : _emptyColor;
        _stackGaugeImgs[0].fillAmount = ((float)_stackCount % _dir) / _dir;
    }

    #endregion

    private void OnClickSetting()
    {
        Manager.UI.OpenUI<UISettingPopup>(UIType.Popup);
    }
}
