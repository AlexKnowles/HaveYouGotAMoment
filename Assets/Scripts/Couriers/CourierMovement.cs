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

        // Start is called before the first frame update
        void Start()
        {
            _moveToTarget = false;
            _moveToExit = false;
            gameObject.transform.position = entranceExitPosition.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_moveToTarget)
            {
                moveTowards(new Vector3(targetPosition.transform.position.x, targetPosition.transform.position.y, transform.position.z));
            }
        }

        private void moveTowards(Vector3 position)
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
            }
            else
            {
                _moveToTarget = false;
            }
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
