using Cinemachine;
using Fusion;
using Game15Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;



namespace PlayerNameSpace
{

    [RequireComponent(typeof(NetworkObject))]
    public class PlayerNew : NetworkBehaviour
    {

        public enum State
        {
            None,
            Ground,
            Walking,
            Jumping,
            Falling,
            MovingUpToZipLine,
            ZipLineHandleHolding,
        }

        #region Public properties
        public State PlayerState;
        #endregion

        public static event Action<PlayerNew> PlayerAction;
        
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected float _rotationSpeed;
        [SerializeField] protected float _fallingSpeed;

        //private NetworkTransform _networkTransform;
        public CinemachineVirtualCamera _tppVirtualCamera;
        // private CinemachineVirtualCamera _zipLineVirtualCamera;
        public Transform _mainCamera;
        private Rigidbody _rigidbody;

        private Vector2 _animMoveValue;
        private Vector3 _velocity;
        private bool _jumpPressed;
        private float _playerSpeed = 2f;
        private float _jumpForce = 5f;
        private bool _cameraAssignedToPlayer;
        private bool _networkEventAssigned;
        private bool _resetCachedInput;
        private Quaternion _playerRotation;
        private Transform _playerTransform;
        private float _cameraYrotationAngle;
        private Vector3 movementDirection;

        Vector3 _cameraOppositeDirection;


        #region Scripts/Components
       
        private InputControl _inputControl;
        
        

        #endregion


        public NetworkObject _networkObject { get; private set; }


        #region Monobehaviour callbacks
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

        }

        private void Start()
        {
            // Target frame rate set to 60.
            Application.targetFrameRate = 60;

            // _playerTransformYrotationAngle = _playerTransform.eulerAngles.y;

        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }



        private void Update()
        {
            if (UnityEngine.Input.GetButtonDown("Jump"))
            {
                _jumpPressed = true;
            }
           
            


        }
        #endregion

        #region Networkbehaviour callbacks

        public void BeforeUpdate()
        {
            /*if (_resetCachedInput)
            {
                _cachedInput = default;
                _resetCachedInput = false;
            }*/
            
        }

        private void LateUpdate()
        {
            // if (_mainCamera == null)
            //     return;
            // _cameraYrotationAngle = _mainCamera.transform.eulerAngles.y;
        }

        public override void Spawned()
        {
           
            Debug.Log($"{nameof(PlayerNew)} : Spawned");
        }

        public override void Render()
        {
            

            if (!_cameraAssignedToPlayer && Object.HasInputAuthority)
            {
                _tppVirtualCamera = GameObject.FindGameObjectWithTag("TPP Camera").GetComponent<CinemachineVirtualCamera>();
                _tppVirtualCamera.Follow = this.transform;
                _mainCamera = Camera.main.transform;

                Debug.Log("Camera assigned to player.........");
                _cameraAssignedToPlayer = true;
            } 
            if (!_networkEventAssigned && Object.HasInputAuthority)
            {
                Debug.Log("Network Events assigned...........");
                _networkEventAssigned = true;
            }
        }



        public override void FixedUpdateNetwork()
        {
            if (GetInput(out InputStruct input_) && Object.HasInputAuthority) 
            {

                _animMoveValue = input_.LeftJoystick;
             
                
                if (input_.LeftJoystick != Vector2.zero)
                {

                    Vector3 dir = this.transform.forward * input_.LeftJoystick.y + this.transform.right * input_.LeftJoystick.x; 
                    
                    // this.transform.position += dir * Runner.DeltaTime * 2.5f;
                    this.transform.Translate(dir * Runner.DeltaTime * 1.25f, Space.World);

                }

                this.transform.LookAt(_mainCamera);
                this.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, this.transform.eulerAngles.y - 180f,0), 
                                                        Quaternion.Euler(0, input_.CameraYrotation, 0), 
                                                        Runner.DeltaTime * 35f);
                
                
            }
            
        }
        #endregion

        public void SwitchCamera(bool value)
        {
            _tppVirtualCamera.gameObject.SetActive(!value);
            // _zipLineVirtualCamera.gameObject.SetActive(value);
        }

    }
}


