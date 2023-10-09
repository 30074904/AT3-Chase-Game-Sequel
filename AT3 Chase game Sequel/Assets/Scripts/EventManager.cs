using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("there can only be one EventManager");

            gameObject.SetActive(false);
        }
    }

    public delegate void ChangeCarCount(int carCount);
    public static ChangeCarCount changeCarCountEvent;

    public delegate void UpdateColor(Color color, int direction);
    public static UpdateColor updateButtonColorEvent;


}