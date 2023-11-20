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

    public delegate void UpdateAnimation(int currAnim);
    public static UpdateAnimation updateAnimationEvent;

    public delegate void UpdateAnimationHands();
    public static UpdateAnimationHands updateAnimationHandsEvent;

    public delegate void DoAttack();
    public static DoAttack DoAttackEvent;

    public delegate void CharacterInteract(int currObject);
    public static CharacterInteract charaterInteractEvent;

    public delegate void ArtifactStolen(bool stolen);
    public static ArtifactStolen ArtifactStolenEvent;

    public delegate void Garrage();
    public static Garrage GarrageEvent;

    public delegate void ChangeLevel(int currLevel);
    public static ChangeLevel ChangeLevelEvent;

    public delegate void CurrentMenu(GameObject uisystem);
    public static CurrentMenu updateCurrentMenu;

    public delegate void CurrentDirrections(GameObject up, GameObject down, GameObject left, GameObject right);
    public static CurrentDirrections updateCurrentDirections;

}