using System.Collections.Generic;
using UnityEngine;

namespace _MyScripts {
    public class ScrollingBackground : MonoBehaviour {
        private enum Direction {
            Up,
            Down,
            Left,
            Right
        }

        [SerializeField] private float     _speed = 1;
        [SerializeField] private Direction _dir   = Direction.Right;

        private readonly List<SpriteRenderer> _sprites = new List<SpriteRenderer>();

        private float _heightCamera;
        private float _widthCamera;

        private Vector3 _positionCam;
        private Camera  _cam;

        private void Awake() {
            _cam          = Camera.main;
            _heightCamera = 2f            * _cam.orthographicSize;
            _widthCamera  = _heightCamera * _cam.aspect;
            foreach (Transform child in transform) {
                _sprites.Add(child.GetComponent<SpriteRenderer>());
            }
        }


        private void Update() {
            foreach (var item in _sprites) {
                switch (_dir) {
                    case Direction.Left:
                        if (item.transform.position.x + item.bounds.size.x / 2 < _cam.transform.position.x - _widthCamera / 2) {
                            SpriteRenderer sprite = _sprites[0];
                            foreach (var i in _sprites) {
                                if (i.transform.position.x > sprite.transform.position.x)
                                    sprite = i;
                            }

                            item.transform.position =
                                new Vector2((sprite.transform.position.x + (sprite.bounds.size.x / 2) + (item.bounds.size.x / 2)),
                                            sprite.transform.position.y);
                        }

                        break;
                    case Direction.Right:
                        if (item.transform.position.x - item.bounds.size.x / 2 > _cam.transform.position.x + _widthCamera / 2) {
                            SpriteRenderer sprite = _sprites[0];
                            foreach (var i in _sprites) {
                                if (i.transform.position.x < sprite.transform.position.x)
                                    sprite = i;
                            }

                            item.transform.position =
                                new Vector2((sprite.transform.position.x - (sprite.bounds.size.x / 2) - (item.bounds.size.x / 2)),
                                            sprite.transform.position.y);
                        }

                        break;
                    case Direction.Down:
                        if (item.transform.position.y + item.bounds.size.y / 2 < _cam.transform.position.y - _heightCamera / 2) {
                            SpriteRenderer sprite = _sprites[0];
                            foreach (var i in _sprites) {
                                if (i.transform.position.y > sprite.transform.position.y)
                                    sprite = i;
                            }

                            item.transform.position = new Vector2(sprite.transform.position.x,
                                                                  (sprite.transform.position.y + (sprite.bounds.size.y / 2) +
                                                                   (item.bounds.size.y                                 / 2)));
                        }

                        break;
                    case Direction.Up:
                        if (item.transform.position.y - item.bounds.size.y / 2 > _cam.transform.position.y + _heightCamera / 2) {
                            SpriteRenderer sprite = _sprites[0];
                            foreach (var i in _sprites) {
                                if (i.transform.position.y < sprite.transform.position.y)
                                    sprite = i;
                            }

                            item.transform.position = new Vector2(sprite.transform.position.x,
                                                                  (sprite.transform.position.y - (sprite.bounds.size.y / 2) -
                                                                   (item.bounds.size.y                                 / 2)));
                        }

                        break;
                }


                switch (_dir) {
                    case Direction.Left:
                        item.transform.Translate(new Vector2(Time.deltaTime * _speed * -1, 0));
                        break;
                    case Direction.Right:
                        item.transform.Translate(new Vector2(Time.deltaTime * _speed, 0));
                        break;
                    case Direction.Down:
                        item.transform.Translate(new Vector2(0, Time.deltaTime * _speed * -1));
                        break;
                    case Direction.Up:
                        item.transform.Translate(new Vector2(0, Time.deltaTime * _speed));
                        break;
                }
            }
        }
    }
}