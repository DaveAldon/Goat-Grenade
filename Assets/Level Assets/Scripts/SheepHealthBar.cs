using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SheepHealthBar : MonoBehaviour
{
    public int health = 0;
    public RectTransform healthBar;

    public void LateUpdate()
    {
        healthBar.sizeDelta = new Vector2(GetComponent<BaseSheepAttribute>().health, healthBar.sizeDelta.y);
    }
}
