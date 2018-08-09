using UnityEngine;

namespace _MyScripts {
    public class Brain : MonoBehaviour {
        public static Brain Self;
        [SerializeField] private float          _speed = 10f;
        [SerializeField] private GameController _gameController;
        [SerializeField] private AudioClip      _hurtClip;
        [SerializeField] private AudioClip      _deathClip;

        private Rigidbody2D _rb2D;
        private float       _angle;
        private AudioSource _audioSource;


        private void Awake() {
            Self = this;
            _rb2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            if (_gameController == null) Debug.LogError("GameController is not set", gameObject);
            if (_speed          == 0) Debug.LogError("_speed is ZERO",               gameObject);
        }

        private void OnEnable() { _rb2D.AddForce(Vector2.one * Random.value * _speed, ForceMode2D.Impulse); }

        private void Update() { transform.Rotate(Vector3.forward, _angle); }

        private void OnCollisionEnter2D(Collision2D other) {
            if (!other.collider.CompareTag("Player")) return;
            PlayAudio();
            _gameController.UpdateScore();
            var velocity = Vector2.zero;
            switch (other.transform.parent.GetComponent<BodyParts>().Type) {
                case BodyPartsType.Horizontal:
                    var xPosRelative = (transform.position.x - other.transform.position.x) / other.collider.bounds.size.x;
                    velocity.x = xPosRelative;
                    velocity.y = -other.contacts[0].point.normalized.y;
                    break;
                case BodyPartsType.Vertical:
                    var yPosRelative = (transform.position.y - other.transform.position.y) / other.collider.bounds.size.y;
                    velocity.y = yPosRelative;
                    velocity.x = -other.contacts[0].point.normalized.x;
                    break;
                default:
                    Debug.LogError("wazzat?", gameObject);
                    break;
            }

//            print(velocity);
//            Debug.DrawRay(transform.position, velocity * _speed, Color.cyan,  10f);
//            Debug.DrawRay(transform.position, velocity,          Color.green, 10f);
            _rb2D.AddForce(velocity * _speed, ForceMode2D.Impulse);
            _angle = velocity.magnitude;
        }

        public void PlayAudio(bool isDead=false) {
            _audioSource.clip = isDead ? _deathClip : _hurtClip;
            _audioSource.Play();
        }
    }
}