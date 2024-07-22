using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _UIdocumnet;
    private Button _button;

    int i = 0;
    private List<Button> _menuButtons = new List<Button>();
    void Awake()
    {
        _UIdocumnet = GetComponent<UIDocument>();
        _button = _UIdocumnet.rootVisualElement.Q("NewGame") as Button;
        _button.RegisterCallback<ClickEvent>(OnNewGameClickEvent);

        _menuButtons = _UIdocumnet.rootVisualElement.Query<Button>().ToList();

        for (i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnNewGameClickEvent);
        }

    }

    void OnButtonClickSound(ClickEvent clickEvent)
    {

    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnNewGameClickEvent);
    }

    private void OnNewGameClickEvent(ClickEvent clickEvent)
    {
        // AudioManager.Instance.PlayOneShot(FMODEvents.Instance.buttonClick, this.transform.position);
        Debug.Log("You clicked me");
        //Time.timeScale = 1;
    }
}
