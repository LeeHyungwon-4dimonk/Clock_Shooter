using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button _settingButton;

    [Header("Stack Gauge UI")]
    [SerializeField] private Image[] _stackGaugeImgs;
    [SerializeField] private Color[] _gaugeColors;
    [SerializeField] private Color _emptyColor = Color.black;
    [SerializeField] private TMP_Text _stackText;

    private float _curStack;
    private float _dir;
    private Tween _stackTween;

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

    #region StackUI

    private void StackUIInitialize()
    {
        for(int i = 0; i < _stackGaugeImgs.Length; i++)
        {
            _stackGaugeImgs[i].color = _emptyColor;
            _stackGaugeImgs[i].fillAmount = 1;
        }
        _stackText.text = "0";
    }

    private void StackUIUpdate(int prev, int cur)
    {
        float prevStack = _curStack;
        _curStack = Manager.Game.turnStack.Current;

        _stackTween?.Kill();

        if (prevStack == _curStack) return;

        _stackTween = DOTween.To(() => prevStack,
            x => 
            { 
                prevStack = x;
                UpdateStackColor(prevStack);
            },
            _curStack,
            0.3f
            )
            .SetEase(Ease.OutCubic);
        _stackText.text = $"{_curStack}";
    }

    private void UpdateStackColor(float stackCount)
    {
        int colorIndex = Mathf.FloorToInt(stackCount / _dir);

        _stackGaugeImgs[0].color =
            0 <= colorIndex
            ? _gaugeColors[colorIndex % _gaugeColors.Length]
            : _emptyColor;

        _stackGaugeImgs[1].color =
            1 <= colorIndex
            ? _gaugeColors[(colorIndex - 1) % _gaugeColors.Length]
            : _emptyColor;

        _stackGaugeImgs[0].fillAmount = (stackCount % _dir) / _dir;
    }

    #endregion

    private void OnClickSetting()
    {
        Manager.UI.OpenUI<UISettingPopup>(UIType.Popup);
    }
}