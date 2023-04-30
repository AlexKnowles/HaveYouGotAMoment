using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace HaveYouGotAMoment.Couriers
{
    public class CourierMovement : MonoBehaviour
    {
        public GameObject targetPosition;
        public GameObject entranceExitPosition;

        public int speed = 5;

        private bool _moveToTarget;
        private bool _moveToExit;

        private CourierDelivery _courierDelivery;

        private Vector3 _courierInitScale;

        // Start is called before the first frame update
        void Start()
        {
            _moveToTarget = false;
            _moveToExit = false;
            gameObject.transform.position = entranceExitPosition.transform.position;
            _courierDelivery = gameObject.GetComponent<CourierDelivery>();
            _courierInitScale = gameObject.transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (_moveToTarget)
            {

                _moveToTarget = moveTowards(new Vector3(targetPosition.transform.position.x, targetPosition.transform.position.y, transform.position.z));
                gameObject.transform.localScale = _courierInitScale;
                // Hideous
                if (!_moveToTarget)
                {
                    // Arrived
                    _courierDelivery.StartDelivery();
                }
            }
            if (_moveToExit)
            {
                gameObject.transform.localScale = new Vector3(-_courierInitScale.x, _courierInitScale.y, _courierInitScale.z);
                _moveToExit = moveTowards(new Vector3(entranceExitPosition.transform.position.x, entranceExitPosition.transform.position.y, transform.position.z));
            }

        }

        private bool moveTowards(Vector3 position)
        {
            Vector3 direction = position - transform.position;

            // Calculate the distance to the target
            float distance = direction.magnitude;

            if (distance > 0.1)
            {
                // TODO Bobbing
                // Normalize the direction vector
                direction.Normalize();

                // Move towards the target at a constant speed
                transform.position += direction * speed * Time.deltaTime;

                return true;
            }
            return false;
        }

        public void StartMovingToTarget()
        {
            _moveToTarget = true;
            _moveToExit = false;
        }

        public void StartMovingToExit()
        {
            _moveToTarget = false;
            _moveToExit = true;
        }
    }
}
