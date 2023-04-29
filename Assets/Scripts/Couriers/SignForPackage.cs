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
                    PackagesSignedFor();
                }
            }

            if(_goToStart)
            {
                _goToStart = moveTowards(_initialPosition);
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
                package.GetComponent<Package.PackageData>().Courier = _courierGettingSignature.GetComponent<CourierData>().CourierName;
                package.GetComponent<Package.PackageData>().Tenant = delivery;
            }
            End();
        }

        public void End()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            _signing = false;
            _goToTarget = false;
            _goToStart = true;
            _courierGettingSignature.GetComponent<CourierDelivery>().EndDelivery();
            _courierGettingSignature = null;
        }
    }
}