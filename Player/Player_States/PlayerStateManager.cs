using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [HideInInspector] public PlayerBaseState currentState;
    [HideInInspector] public PlayerMoveState MoveState = new PlayerMoveState();
    [HideInInspector] public PlayerDeathState DeathState = new PlayerDeathState();
    [HideInInspector] public PlayerReloadState ReloadState = new PlayerReloadState();

    public Animator animator { get; private set; }

    [field: SerializeField]
    public GameObject Projectile { get; private set; }

    [field: SerializeField]
    public PlayerControls ActivePlayerControls { get; private set; }

    public PlayerStats Stats { get; private set; }

    [field: SerializeField]
    public GameObject[] FiringPoints { get; private set; }

    [field: SerializeField]
    public int PlayerSide { get; private set; }

    public bool CanFire { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        MoveState = gameObject.AddComponent<PlayerMoveState>();
        DeathState = gameObject.AddComponent<PlayerDeathState>();
        ReloadState = gameObject.AddComponent<PlayerReloadState>();

        Stats = GetComponent<PlayerStats>();

        CanFire = true;

        animator = GetComponentInChildren<Animator>();
        currentState = MoveState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuUI.Instance.SetVisible(true);
            Time.timeScale = 0;
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void Death(int side)
    {
        if(currentState == DeathState) { return; }
        ReloadState.StopAllCoroutines();
        if(side != -1) { InGameUI.Instance.IncrementScore(side); }
        SwitchState(DeathState);
    }

    public void CanFireSwitch(bool b)
    {
        CanFire = b;
    }
}
