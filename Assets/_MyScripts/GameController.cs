using UnityEngine;
using UnityEngine.UI;

namespace _MyScripts {
    public class GameController : MonoBehaviour {
        [SerializeField] private Text           _scoreTxt;

        private int _score;
        private int _death;

        private void Awake() {
            if (_scoreTxt == null) Debug.LogError("_scoreTxt is not set", gameObject);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Respawn")) {
                other.GetComponent<BrainOrHeart>().Shit();
//                other.GetComponent<BrainOrHeart>().PlayAudio(isDead: true);
//                other.gameObject.SetActive(false);
                other.transform.position = Vector3.zero;
//                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
//                other.gameObject.SetActive(true);
                _score = 0;
                _death++;
                UpdateScore();
            }
        }

        public void UpdateScore() { _scoreTxt.text = ++_score + "/" + _death; }
    }
}