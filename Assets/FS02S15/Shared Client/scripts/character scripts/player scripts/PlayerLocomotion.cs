using Fusion;
using UnityEngine;

public class PlayerLocomotion : CharacterManager
{

    public enum State{
        walk,
        run,
        jump
    }

    /// <summary>
    /// Player Speed
    /// </summary>
    [SerializeField] float Speed;
    /// <summary>
    /// Player Jump Speed
    /// </summary>
    [SerializeField] public float JumpSpeed;
    /// <summary>
    /// Player Jump Height.
    /// </summary>
    [SerializeField] public float JumpHeight;

    [Space(20)]
    [SerializeField] PlayerManager _playerManager;

    
    #region Monobehaviour callbacks
    void Start()
    {
        
    }

    #endregion

    #region Fusion callbacks
    public override void Spawned()
    {
        if (!Object.HasInputAuthority && !Object.HasStateAuthority)
            return;

        // Enable PlayerAnimator component.
        GetComponent<PlayerAnimator>().enabled = true;
        
        GetComponent<PlayerUi>().enabled = true;  

        Debug.Log($"{nameof(PlayerLocomotion)} {nameof(Spawned)}");
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority && !Object.HasInputAuthority)
            return;

        Locomotion();
        Rotation(); 
        HeightDetection();
        Jump();
    }
    #endregion

    /// <summary>
    /// Locomotion of the Player
    /// </summary>
    public override void Locomotion()
    {
       
        // Direction
        Vector3 dir = this.transform.forward * _playerManager._currentDirection.y + this.transform.right * _playerManager._currentDirection.x;
        
        // Increase or decrease speed based on Y current Joystick direction.
        Speed = (_playerManager._joystickMove.y > 0.5f) ? 4 : 2;
            
        // Translate the players position.
        this.transform.Translate(dir * Runner.DeltaTime * Speed, Space.World);

        
    }

    /// <summary>
    /// Rotation of the  Player 
    /// </summary>
    public override void Rotation()
    {
        
        // Slerp rotation of the Player accordance with the camera.
        this.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, this.transform.eulerAngles.y, 0),
                                                    Quaternion.Euler(0, CameraController.Instance.RotationValue.y, 0),
                                                    Runner.DeltaTime * 45f);
    }

    /// <summary>
    /// Height detection using the raycast
    /// </summary>
    public void HeightDetection()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            Debug.DrawRay(this.transform.position, Vector3.down * 1f, Color.red);
        }
        
    }

    public void Jump()
    {
        
    }


}
