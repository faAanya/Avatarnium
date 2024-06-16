using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }
    // [field: Header("UI SFX")]
    // [field: SerializeField] public EventReference equiptionSound { get; private set; }

    [field: Header("Blends SFX")]
    [field: SerializeField] public EventReference fireBallShot { get; private set; }
    [field: SerializeField] public EventReference moreFireBallsShot { get; private set; }
    [field: SerializeField] public EventReference airShot { get; private set; }
    [field: SerializeField] public EventReference stoneShot { get; private set; }
    [field: SerializeField] public EventReference lighteningShot { get; private set; }


    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }


    //[field: SerializeField] public EventReference ambient { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
