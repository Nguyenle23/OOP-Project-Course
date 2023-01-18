using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaUI : MonoBehaviour
{
    public Image Mana;

    [SerializeField]
    private float lerpSpeed = .1f;

    private float currentFill;

    private float currentValue;
    public float MaxValue { get; set; }

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MaxValue)
            {
                currentValue = MaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MaxValue; 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Mana = GetComponent<Image>();
        MaxValue = 5;
    }

    // Update is called once per frame
    void Update()
    { 
        if (currentFill != Mana.fillAmount)
        {
            Mana.fillAmount = Mathf.Lerp(Mana.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
        Mana.fillAmount = currentFill;
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
