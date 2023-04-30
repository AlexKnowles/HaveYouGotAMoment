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

        // Start is called before the first frame update
        void Start()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
                    GetComponent<DrawLineMouseDrag>().Activate();
                }
            }

            if (_startedSigning && Input.GetMouseButtonUp(0))
            {
                _startedSigning = false;
                PackagesSignedFor();
            }

            if(_goToStart)
            {
                _goToStart = moveTowards(_initialPosition);
                if (!_goToStart)
                {
                    GetComponent<DrawLineMouseDrag>().Clear();
                }
            }

            if(_goToTarget)
            {
                _goToTarget = moveTowards(TargetPosition.transform.position);
                if (!_goToTarget)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        private bool moveTowards(Vector3 position)
        {
            Vector3 direction = position - transform.position;

            // Calculate the distance to the target
            float distance = direction.magnitude;

            if (distance > 0.1)
            {
                // Normalize the direction vector
                direction.Normalize();

                // Move towards the target at a constant speed
                transform.position += direction * speed * Time.deltaTime;

                return true;
            }
            return false;
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