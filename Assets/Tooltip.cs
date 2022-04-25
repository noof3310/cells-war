using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private static Tooltip current;

    public void Awake()
    {
        current = this;
        Hide();
    }
    public static void Show()
    {
        current.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        current.gameObject.SetActive(false);
    }
}
