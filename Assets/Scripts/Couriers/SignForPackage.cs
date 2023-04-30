using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using UnityEngine;

namespace HaveYouGotAMoment.Couriers
{
    public class SignForPackage : MonoBehaviour
    {
        private Camera _mainCamera;

        public GameObject PackagePrefab;
        public GameObject TargetPosition;

        public int speed = 10;

        private bool _signing = false;
        private bool _startedSigning = false;
        private Vector3 _initialPosition;

        private bool _goToStart = false;
        private bool _goToTarget = false;

        private GameObject _courierGettingSignature;

        private DrawLineMouseDrag _dragLineMouseDrag;

        // Start is called before the first frame update
        void Start()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            if (_dragLineMouseDrag == null)
            {
                _dragLineMouseDrag = GetComponent<DrawLineMouseDrag>();
            }
            _initialPosition = transform.position;
            GetComponent<BoxCollider2D>().enabled = false;
        }


        // Update is called once per frame
        void Update()
        {
            if (_signing && Input.GetMouseButtonDown(0))
            {
                Vector2 mouseInWorldSpace = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mouseInWorldSpace, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    _startedSigning = true;
                    _dragLineMouseDrag.Activate();
                }
            }

            if (_startedSigning && Input.GetMouseButtonUp(0))
            {
                _startedSigning = false;
                PackagesSignedFor();
            }

            if(_goToStart)
            {
                var (keepMoving, positionDifference) = moveTowards(_initialPosition);
                _goToStart = keepMoving;
                if (!_goToStart)
                {
                    _dragLineMouseDrag.Clear();
                }
                else
                {
                    _dragLineMouseDrag.MovePoints(positionDifference);
                }
            }

            if(_goToTarget)
            {
                var (keepMoving, _) = moveTowards(TargetPosition.transform.position);
                _goToTarget = keepMoving;
                if (!_goToTarget)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        private (bool, Vector3) moveTowards(Vector3 position)
        {
            Vector3 direction = position - transform.position;

            // Calculate the distance to the target
            float distance = direction.magnitude;

            if (distance > 0.1)
            {
                // Normalize the direction vector
                direction.Normalize();
                Vector3 moveThisMuch = direction * speed * Time.deltaTime;

                // Move towards the target at a constant speed
                transform.position += moveThisMuch;

                return (true, moveThisMuch);
            }
            return (false, new Vector3());
        }

        public void StartGettingSignature(GameObject courier)
        {
            _signing = true;
            _goToTarget = true;
            _goToStart = false;
            _courierGettingSignature = courier;
        }

        public void PackagesSignedFor()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            var deliveries = _courierGettingSignature.GetComponent<CourierData>().Deliveries;
            foreach (var delivery in deliveries)
            {
                var package = Instantiate(PackagePrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                package.GetComponent<Packages.PackageData>().Courier = _courierGettingSignature.GetComponent<CourierData>().CourierName;
                package.GetComponent<Packages.PackageData>().Tenant = delivery;
                package.transform.localScale = new Vector3(Random.Range(0.5f, 2.0f), Random.Range(0.5f, 2.0f), 1);
            }
            _courierGettingSignature.GetComponent<CourierDelivery>().EndDelivery();
        }

        public void End()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<DrawLineMouseDrag>().Deactivate();
            _signing = false;
            _goToTarget = false;
            _goToStart = true;
            _courierGettingSignature = null;
        }
    }
}