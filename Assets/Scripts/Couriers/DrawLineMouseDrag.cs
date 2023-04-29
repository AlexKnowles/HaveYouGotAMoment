using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Couriers
{
    public class DrawLineMouseDrag : MonoBehaviour
    {
        private Camera _mainCamera;

        public LineRenderer Line;
        public BoxCollider2D Surface;
        public float lineWidth = 0.04f;
        public float minimumVertexDistance = 0.1f;

        private bool isLineStarted;

        private bool _canDraw = false;

        void Start()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            // set the color of the line
            Line.startColor = Color.red;
            Line.endColor = Color.red;

            // set width of the renderer
            Line.startWidth = lineWidth;
            Line.endWidth = lineWidth;

            isLineStarted = false;
            Line.positionCount = 0;
        }

        void Update()
        {
            if (_canDraw && Input.GetMouseButtonDown(0))
            {
                Line.positionCount = 0;
                Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);

                Line.positionCount = 2;
                // TODO Clamp inside box
                Line.SetPosition(0, mousePos);
                Line.SetPosition(1, mousePos);
                isLineStarted = true;
            }

            if (_canDraw && Input.GetMouseButton(0) && isLineStarted)
            {
                Vector3 currentPos = GetWorldCoordinate(Input.mousePosition);
                float distance = Vector3.Distance(currentPos, Line.GetPosition(Line.positionCount - 1));
                if (distance > minimumVertexDistance)
                {
                    UpdateLine();
                }
            }

            if (_canDraw && Input.GetMouseButtonUp(0))
            {
                isLineStarted = false;
            }
        }

        public void Activate()
        {
            _canDraw = true;
        }

        public void Deactivate()
        {
            _canDraw = false;
        }

        public void Clear()
        {
            Line.SetPositions(new Vector3[0]);
            Line.positionCount = 0;
        }

        private void UpdateLine()
        {
            Line.positionCount++;
            var clampedPosition = Input.mousePosition;
            Vector2 mouseInWorldSpace = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseInWorldSpace, Vector2.zero);

            if (hit.collider == null || hit.collider.gameObject != gameObject)
            {
                Line.SetPosition(Line.positionCount - 1, Line.GetPosition(Line.positionCount - 2));

            } else
            {
                Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(clampedPosition));

            }
        }

        private Vector3 GetWorldCoordinate(Vector3 mousePosition)
        {
            Vector3 mousePos = new Vector3(mousePosition.x, mousePosition.y, 1);
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}
