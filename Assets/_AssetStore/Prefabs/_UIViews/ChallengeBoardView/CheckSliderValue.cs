using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject checkImg;
    [SerializeField]
    private float clearValue;
    // Start is called before the first frame update

    public void CheckValue()
    {
        if (slider.value >= clearValue && !checkImg.activeSelf)
        {
            checkImg.SetActive(true);
        }
    }    
}
