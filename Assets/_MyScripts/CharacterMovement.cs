using UnityEngine;

namespace _MyScripts {
    public class CharacterMovement : MonoBehaviour {
        [SerializeField] private float       speed;
        private                  bool        isUp = true;
        private                  Rigidbody2D rb2d;
        private                  Rigidbody rb;

        private void Start() {
            rb2d = GetComponent<Rigidbody2D>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isUp) {
                transform.position += Vector3.up * 2;
                isUp               =  true;
            } else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp) {
                transform.position -= Vector3.up ;
                isUp               =  false;
            }

            rb.AddForce(transform.right * Time.deltaTime * speed, ForceMode.Impulse);
//            rb2d.AddForce(transform.right * Time.deltaTime * speed, ForceMode2D.Impulse);
        }
    }
}