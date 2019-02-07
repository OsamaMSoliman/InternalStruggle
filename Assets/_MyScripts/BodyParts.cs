using UnityEngine;

namespace _MyScripts {
    public enum BodyPartsType {
        Vertical,
        Horizontal
    }

    public class BodyParts : MonoBehaviour {
        public BodyPartsType Type;

        [SerializeField] private bool _isControlledByAi;

        [SerializeField] private float _speed = 10f;

        private void Update() {
            var pos = transform.position;

            if (Type == BodyPartsType.Vertical) {
                var vertical = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
                pos.y = Mathf.Clamp(_isControlledByAi ? BrainOrHeart.Brain.transform.position.y : pos.y + vertical, -7, 7);
            } else {
                var horizontal = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
                pos.x = Mathf.Clamp(_isControlledByAi ? BrainOrHeart.Brain.transform.position.x : pos.x + horizontal, -12, 12);
            }

            transform.position = pos;
        }
    }
}