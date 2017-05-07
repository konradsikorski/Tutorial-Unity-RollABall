using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthSliderController : MonoBehaviour {
    public Slider HealthSlider;
    private Slider _mySlider;
    private RectTransform _mySliderRect;
    private RectTransform _canvasRect;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<IHealth>().HealthChanged += (sender, args) =>
        {
            var healthObject = (IHealth)sender;
            if (healthObject.Health <= 0) return;

            CreateHealtSlider().value = healthObject.Health / healthObject.InitialHealth;
        };
	}
	
	// Update is called once per frame
	void Update () {
        MoveSlider();
    }

    private void OnDestroy()
    {
        if (_mySlider != null) Destroy(_mySlider.gameObject);
    }

    private Slider CreateHealtSlider()
    {
        if (_mySlider != null) return _mySlider;

        _mySlider = Instantiate(HealthSlider, new Vector3(0, 0, 0), Quaternion.identity);
        _mySliderRect = _mySlider.GetComponent<RectTransform>();

        var canvas = GameObject.Find("GameScreen");
        _canvasRect = canvas.GetComponent<RectTransform>();
        _mySlider.transform.SetParent(canvas.transform, false);

        return _mySlider;
    }

    void MoveSlider()
    {
        if (_mySlider == null) return;

        var pos = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z +1);
        
        var viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        _mySliderRect.anchorMin = viewportPoint;
        _mySliderRect.anchorMax = viewportPoint;
    }
}
