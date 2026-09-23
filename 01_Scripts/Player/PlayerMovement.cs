using UnityEngine;

namespace _01_Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Player _agent;

        public Vector3 Velocity => _velocity;
        public bool IsJumping => _rigidbody.linearVelocity.y > 3;
        public bool IsFalling => _rigidbody.linearVelocity.y < -3;
        public bool IsRuning { get; private set; }
        public bool IsFlyAway { get; set; }
        
        private Vector3 _velocity;
        private Vector3 _cameraRotation;
        private Vector3 _cameraRotations;
        
        private int _cameraRotationSmooth;
        private float _moveSpeed;
        private int _jumpPower;


        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _moveSpeed = _agent.PlayerStatsSo.WalkSpeed;
            _jumpPower = _agent.PlayerStatsSo.JumpPower;
            _cameraRotationSmooth = _agent.PlayerStatsSo.CameraRotationSmooth;
        }

        private void OnEnable()
        {
            _agent.PlayerInputSo.OnMoveAction += OnMovement;
            _agent.PlayerInputSo.AtLootAction += OnAtLoot;
            _agent.PlayerInputSo.OnCrouchAction += OnCrouch;
            _agent.PlayerInputSo.OnJumpAction += OnJump;
            _agent.PlayerInputSo.OnRunAction += OnRun;
        }
        private void OnDisable()
        {
            _agent.PlayerInputSo.OnMoveAction -= OnMovement;
            _agent.PlayerInputSo.AtLootAction -= OnAtLoot;
            _agent.PlayerInputSo.OnCrouchAction -= OnCrouch;
            _agent.PlayerInputSo.OnJumpAction -= OnJump;
            _agent.PlayerInputSo.OnRunAction -= OnRun;
        }
        private void Update()
        {
            
            _cameraRotations += new Vector3(_cameraRotation.y, _cameraRotation.x);
            
            _cameraRotations.x = Mathf.Clamp(_cameraRotations.x, -80f, 70f);
            
            Quaternion currentCameraRotation = _agent.CinemaCamera.transform.localRotation;
            Quaternion targetCameraRotation = Quaternion.Euler(-_cameraRotations.x, _cameraRotations.y ,0);
            Quaternion smoothCameraRotation = Quaternion.Lerp(currentCameraRotation, targetCameraRotation, _cameraRotationSmooth * Time.deltaTime);

            /*
            float direction = Mathf.DeltaAngle(currentCameraRotation.eulerAngles.y, smoothCameraRotation.eulerAngles.y);

            float normalized = Mathf.InverseLerp(_agent.PlayerStatsSo.CameraRotationZMinClamp, 45f, Mathf.Abs(direction));
            float targetZ = Mathf.Sign(direction) * normalized * _agent.PlayerStatsSo.CameraRotationZMaxClamp;
            
            if (Mathf.Abs(targetZ - smoothCameraRotation.z) < 0.1f)
                targetZ = smoothCameraRotation.z;
            
            smoothCameraRotation.z = Mathf.Lerp(
                smoothCameraRotation.z,
                targetZ,
                _agent.PlayerStatsSo.CameraRotationZSmoothSpeed * Time.deltaTime
            );
            */


            _agent.CinemaCamera.transform.localRotation = smoothCameraRotation;
            

            if (!_agent.GroundChecker.GroundCheck())
            {
                
                _rigidbody.linearVelocity += Vector3.up * (Physics.gravity.y * (3 * Time.deltaTime));
                IsFlyAway = true;
            }
            else
            {
                IsFlyAway = false;
            }

            if (!_agent.StaminaChecker.CanRun)
            {
                _moveSpeed = _agent.PlayerStatsSo.WalkSpeed;
                IsRuning = false;
            }
        }
        
        private void FixedUpdate()
        {
            if(IsFlyAway) return;
                
            Quaternion cameraRotation = Quaternion.Euler(0, _agent.CinemaCamera.transform.localEulerAngles.y, 0); //회전에 따라 이동 방향이도 수정
            Vector3 moveDir = new Vector3(_velocity.x, 0, _velocity.y) * _moveSpeed;
            moveDir.y = _rigidbody.linearVelocity.y;
 
            _rigidbody.linearVelocity = cameraRotation * moveDir;
        }
        
        public void SetVelocityZero() => _rigidbody.linearVelocity = Vector3.zero;
        
        public void SetForce(Vector3 velocity) => _rigidbody.AddForce(velocity, ForceMode.Impulse);
        
        private void OnAtLoot(Vector2 obj)
        {
            _cameraRotation = obj * 0.5f;
        }

        private void OnMovement(Vector2 obj)
        {
            _velocity = obj;
        }

        private void OnJump()
        {
            if (_agent.GroundChecker.GroundCheck())
            {
                _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
            }
        }

        private void OnRun(bool obj)
        {
            if (!_agent.StaminaChecker.CanRun) return;

            if (obj)
            {
                _moveSpeed = _agent.PlayerStatsSo.RunSpeed;
                IsRuning = true;
            }
            else
            {
                _moveSpeed = _agent.PlayerStatsSo.WalkSpeed;
                IsRuning = false;
            }
            
        }
        
        private void OnCrouch(bool obj)
        {
            if (obj)
            {
                
            }
            else
            {
                
            }
        }
        
    }
}