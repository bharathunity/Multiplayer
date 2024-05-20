using Cinemachine;
using Fusion;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : CharacterManager
{

    public enum State
    {
        Normal,
        Aim,

    }

    
    /// <summary>
    /// Smooth time.
    /// </summary>
    [SerializeField] float smoothTime = 0.3f;

    /// <summary>
    /// Current direction.
    /// </summary>
    [field: SerializeField] public Vector2 _currentDirection { get; private set; }

    /// <summary>
    /// Final movement direction
    /// </summary>
    [field: SerializeField] public Vector2 _joystickMove { get; private set; }

    /// <summary>
    /// Current velocity
    /// </summary>
    Vector2 _currentVelocity = Vector2.zero;

    /// <summary>
    /// Check for camera assigned to player or not.
    /// </summary>
    private bool _cameraAssignedToPlayer;

    /// <summary>
    /// Toggle to true or false as the player state change.
    /// </summary>
    private bool _playerStateChanging;

    /// <summary>
    /// Start time and end time of the Player in game
    /// </summary>
    private float _startTime, _endTime;

    private PlayerRef _player;
    

    #region Scripts & Others
    /// <summary>
    /// New input system
    /// </summary>
    protected SharedInput _sharedInput;

    public State PlayerManagerState;
    #endregion



    private void Awake()
    {

        _sharedInput = new SharedInput();
    }


    private void OnEnable()
    {
        _sharedInput?.Enable();
        

    }

    private void OnDisable()
    {
        _sharedInput?.Disable();
        _endTime = MathF.Round(Time.time - _startTime, 2);
       
        Debug.Log($"{nameof(OnDestroy)} End time {MathF.Round(_endTime, 2)} Player manager state {PlayerManagerState}");

    }

    #region Monobehaviour callbacks

    void Start()
    {
        PlayerManagerState = State.Normal;
        PlayerUi.OnPlayerMessage?.Invoke(PlayerManagerState.ToString(), true);
        _startTime = Time.time;

    }

    


    private void Update()
    {
        if (_sharedInput == null)
            return;

        LocalInputProvider();
    }

    

    private void OnDestroy()
    {
        

    }

    #endregion

    #region Private methods
    /// <summary>
    /// Intakes the Input from the local device.
    /// </summary>
    void LocalInputProvider()
    {
        _joystickMove = _sharedInput.Player.Move.ReadValue<Vector2>();

        // _currentDirection = Vector2.SmoothDamp(_currentDirection, _finalDirection, ref _currentVelocity, smoothTime);
        /*_currentDirection = new Vector2((float)System.Math.Round(_currentDirection.x, 2), 
                                        (float)System.Math.Round(_currentDirection.y, 2));*/
        
        _currentDirection = new Vector2((float)System.Math.Round(_joystickMove.x, 2), 
                                        (float)System.Math.Round(_joystickMove.y, 2));

        // Player state change
        _sharedInput.Player.Aim.performed += ctx =>
        {
            _playerStateChanging = true;
            switch (PlayerManagerState)
            {
                case State.Normal:
                    PlayerManagerState = State.Aim;
                    break;
                case State.Aim: 
                    PlayerManagerState = State.Normal;
                    break;
            }
            

            Debug.Log($"Player state is now in {PlayerManagerState}");
        };
    

    }
    #endregion

    #region Fusion callbacks

    public override void Spawned()          
    {

    }

    public override void Render()
    {
        if (!this.Object.HasInputAuthority)
            return;

        if (!_cameraAssignedToPlayer)
        {
            // Assign Follow of cinemachine virtual camera with current player.
            CameraController.Instance.VirtualCamera.Follow = this.transform;

            _cameraAssignedToPlayer = true;

            Debug.DrawRay(transform.position, Vector3.down, Color.red);

            Debug.Log("Camera assigned to player.........");
        }

        if (_playerStateChanging)
        {
            // Update the state to Normal on F Keypress
            if (PlayerManagerState == State.Normal)
            {
                PlayerAnimator.OnSetLayerMaskWeight?.Invoke(1, 0);
                PlayerUi.OnPlayerMessage?.Invoke(PlayerManagerState.ToString(), true);
            }
            // Update the state to Aim on F Keypress
            if (PlayerManagerState == State.Aim)
            {   
                PlayerAnimator.OnSetLayerMaskWeight?.Invoke(1, 1);
                PlayerUi.OnPlayerMessage?.Invoke(PlayerManagerState.ToString(), true);    
            }
            _playerStateChanging = false;
        }

        var pingValue = MathF.Round((float)(Runner.GetPlayerRtt(_player) * 100), 0);
        PlayerUi.OnPlayerPing?.Invoke(pingValue.ToString(), true);

    }

    public override void FixedUpdateNetwork()
    {

        
    }
    #endregion

    

    

}
