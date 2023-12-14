using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private int _multiplier;
    [SerializeField]
    private TextMeshProUGUI _multiplierText;

    private void Start()
    {
        _multiplierText.text = "X " + _multiplier.ToString();
    }

    public int Multiplier
    {
        get
        {
            return _multiplier;
        }
    }
}
