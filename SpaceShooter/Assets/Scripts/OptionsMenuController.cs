using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour {
    public Toggle Autofire;
    public Toggle MouseMove;
    public Slider MouseSensitivity;
    public CanvasGroup PreviousScreen;

	// Use this for initialization
	void Start ()
    {
        SetFromSettings();
    }

    public void Apply()
    {
        GameController.Instance.Settings.AutofireEnabled = Autofire.isOn;
        GameController.Instance.Settings.MouseMoveEnabled = MouseMove.isOn;
        GameController.Instance.Settings.MouseSenstivity = MouseSensitivity.value;
        GameController.Instance.Settings.Save();

        CloseMenu();
    }

    public void Cancel()
    {
        SetFromSettings();
        CloseMenu();
    }

    private void SetFromSettings()
    {
        Autofire.isOn = GameController.Instance.Settings.AutofireEnabled;
        MouseMove.isOn = GameController.Instance.Settings.MouseMoveEnabled;
        MouseSensitivity.value = GameController.Instance.Settings.MouseSenstivity;
    }

    private void CloseMenu()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        this.gameObject.SetActive(false);

        PreviousScreen.gameObject.SetActive(true);
        PreviousScreen.alpha = 1;
    }
}

public class GameSettings
{
    public bool AutofireEnabled
    {
        get { return GetBool("AutofireEnabled", true); }
        set { SetBool("AutofireEnabled", value); }
    }

    public bool MouseMoveEnabled
    {
        get { return GetBool("MouseMoveEnabled", true); }
        set { SetBool("MouseMoveEnabled", value); }
    }

    public float MouseSenstivity
    {
        get { return GetFloat("MouseSenstivity", 10); }
        set { SetFloat("MouseSenstivity", value); }
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }

    private bool GetBool(string name, bool defaultValue)
    {
        if (!PlayerPrefs.HasKey(name)) return defaultValue;
        return System.Convert.ToBoolean(PlayerPrefs.GetInt(name));
    }

    private void SetBool(string name, bool value)
    {
        PlayerPrefs.SetInt(name, System.Convert.ToInt32(value));
    }

    private float GetFloat(string name, float defaultValue)
    {
        if (!PlayerPrefs.HasKey(name)) return defaultValue;
        return PlayerPrefs.GetFloat(name);
    }

    private void SetFloat(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

}
