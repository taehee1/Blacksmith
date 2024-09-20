using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private Color textColor;
    private TextMeshProUGUI damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        textColor = damageText.color;
    }

    public void Setup(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
    }

    public void ObjectDestroy()
    {
        Destroy(gameObject);
    }
}