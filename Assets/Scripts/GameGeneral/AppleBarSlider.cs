using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleBarSlider : MonoBehaviour
{
    public static AppleBarSlider instance;

    public int currentSliderValue = 10;
    public Coroutine _decreaseSlider;

    private Slider _appleSlider;    
    private int _maxSliderValue = 20;
    private WaitForSeconds _decreaseTick = new WaitForSeconds(2.0f);

    private void Awake()
    {
        instance = this;

        _appleSlider = GetComponent<Slider>();
        _appleSlider.value = currentSliderValue;
        _appleSlider.maxValue = _maxSliderValue;
    }

    public void DecreaseAppleSlider()
    {
        if (_decreaseSlider != null)
            StopCoroutine(_decreaseSlider);

        _decreaseSlider = StartCoroutine(DecreaseSlider(_appleSlider));
    }

    public IEnumerator DecreaseSlider(Slider slider)
    {
        yield return new WaitForSeconds(2.0f);

        while (slider.value >= 0)
        {
            slider.value -= 1;
            
            yield return _decreaseTick;

            if (slider.value == 0)
                break;
        }
        _decreaseSlider = null;

    }



}
