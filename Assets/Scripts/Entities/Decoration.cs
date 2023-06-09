using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class Decoration : MonoBehaviour
    {
        private Camera _mainCamera;
        private bool _isBeingDragged = false;

        // Start is called before the first frame update
        void Start()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isBeingDragged && Input.GetMouseButtonDown(0))
            {
                Vector2 mouseInWorldSpace = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseInWorldSpace, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    _isBeingDragged = true;
                }
            }

            if (_isBeingDragged && Input.GetMouseButtonUp(0))
            {
                _isBeingDragged = false;
            }

            if (_isBeingDragged)
            {
                Vector3 mouseInWorldSpace = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mouseInWorldSpace.x, mouseInWorldSpace.y, transform.position.z);
            }

        }
    }
}
