using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        [SerializeField] private float _wallJumpTakeOffSpeed = 3;
        [SerializeField] private float _wallJumpTimeBeforeGainingBackControls = 1.0f;
        public ScoreKeeper _scoreKeeper;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public RespawnLogic _respawnLogic;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private bool _canDoubleJump = false;
        private bool _hasDoubleJumped = false;
        private bool _doubleJump = false;

        private bool _canWallJump = false;
        private bool _wallJump = false;

        private List<EnemyController> _defeatedEnemies = new List<EnemyController>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            _respawnLogic = GetComponent<RespawnLogic>();
            _scoreKeeper = GetComponent<ScoreKeeper>();
            _waitTimeBeforeControlsAfterWallJump = _wallJumpTimeBeforeGainingBackControls;
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                if (!_isWallJumping) 
                {
                    move.x = Input.GetAxis("Horizontal");
                }
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if(jumpState == JumpState.InFlight && Input.GetButtonDown("Jump") && _hittingWall && _canWallJump)
                {
                    jumpState = JumpState.WallJump;
                }
                else if(jumpState == JumpState.InFlight && Input.GetButtonDown("Jump") && _canDoubleJump && !_hasDoubleJumped)
                {
                    jumpState = JumpState.PrepareForDoubleJump;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    _canDoubleJump = true;
                    _canWallJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.PrepareForDoubleJump:
                    jumpState = JumpState.Jumping;
                    _doubleJump = true;
                    stopJump = false;
                    _canDoubleJump = false;
                    _hasDoubleJumped = true;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.WallJump:
                    jumpState = JumpState.Jumping;
                    _wallJump = true;
                    stopJump = false;
                    _canDoubleJump = false;
                    _hasDoubleJumped = true;
                    _isWallJumping = true;
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    _hasDoubleJumped = false;
                    _hittingWall = false;
                    _canWallJump = false;
                    _isWallJumping = false;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (_doubleJump && !IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                _doubleJump = false;
            }
            else if (_wallJump)
            {
                velocity.y = _wallJumpTakeOffSpeed * model.jumpModifier;

                if (_wallDirection > 0)
                {
                    velocity.x = -_wallJumpTakeOffSpeed * model.jumpModifier;
                    move.x = -1;
                }
                else
                {
                    velocity.x = _wallJumpTakeOffSpeed * model.jumpModifier;
                    move.x = 1;
                }
                _wallJump = false;
                _hittingWall = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", (IsGrounded || _movingPlatform));
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void SetMovingPlatform(GameObject movingPlatform)
        {
            _movingPlatform = movingPlatform;
        }

        public void SetMovingPlatform()
        {
            _movingPlatform = null;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            PrepareForDoubleJump,
            Jumping,
            WallJump,
            InFlight,
            Landed
        }

        public void AddDefeatedEnemy(EnemyController enemy)
        {
            _defeatedEnemies.Add(enemy);
        }

        public void RespawnEnemies()
        {
            foreach (EnemyController enemy in _defeatedEnemies)
            {
                var ev = Schedule<EnemyRespawn>();
                ev.enemy = enemy;
            }

            _defeatedEnemies.Clear();
        }

    }
}