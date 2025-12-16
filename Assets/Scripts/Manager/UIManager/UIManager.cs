using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Transform _screenRoot;
    private Transform _popupRoot;

    private readonly Dictionary<System.Type, UIBase> _uiCache = new();
    private readonly Stack<UIBase> _popupStack = new();

    private UIBase _currentScreen;

    private void Start()
    {
        FindCanvasAndRoots();
    }

    private void FindCanvasAndRoots()
    {
        var canvas = FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("[UIManager] Canvas not found in scene.");
            return;
        }

        _screenRoot = canvas.transform.Find("ScreenRoot");
        _popupRoot = canvas.transform.Find("PopupRoot");

        if (_screenRoot == null || _popupRoot == null)
        {
            Debug.LogError("[UIManager] ScreenRoot or PopupRoot not found.");
        }
    }

    public T OpenUI<T>(UIType type) where T : UIBase
    {
        var ui = GetOrCreateUI<T>(type);

        if(type == UIType.Screen)
        {
            _currentScreen?.Close();
            _currentScreen = ui;
        }
        else
        {
            _popupStack.Push(ui);
        }

        ui.Open();
        return ui as T;
    }

    public void CloseUI<T>() where T : UIBase
    {
        if (!_uiCache.TryGetValue(typeof(T), out var ui))
            return;

        ui.Close();

        if(ui == _currentScreen)
        {
            _currentScreen = null;
        }
        else if (_popupStack.Count > 0 && _popupStack.Peek() == ui)
        {
            _popupStack.Pop();
        }
    }

    private UIBase GetOrCreateUI<T>(UIType type) where T : UIBase
    {
        var key = typeof(T);    

        if(_uiCache.TryGetValue(key, out var ui))
            return ui;
        Debug.Log(key.Name);

        var prefab = Resources.Load<T>(key.Name);
        var parent = type == UIType.Screen ? _screenRoot : _popupRoot;

        var instance = Instantiate(prefab, parent);
        instance.Close();

        _uiCache.Add(key, instance);
        return instance;
    }
}