using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Couriers
{
    public class CourierDelivery : MonoBehaviour
    {
        private CourierMovement _courierMovement;
        private CourierData _courierData;

        private bool _waiting = false;
        private float _secondsWaiting = 0f;

        // Start is called before the first frame update
        void Start()
        {
            _courierMovement = gameObject.GetComponent<CourierMovement>();
            _courierData = gameObject.GetComponent<CourierData>();
        }

        // Update is called once per frame
        void Update()
        {
            _secondsWaiting += Time.deltaTime;
            if (_waiting && _secondsWaiting > _courierData.MaxWaitTimeInSeconds)
            {
                EndDelivery();
            }
        }

        public void StartDelivery()
        {
            Debug.Log("Delivery Started");
            _secondsWaiting = 0f;
            _waiting = true;
            // TODO Open Clipboard UI
        }

        public void EndDelivery()
        {
            // TODO Close the clipboard if open
            Debug.Log("Delivery Ended");
            _waiting = false;
            _courierMovement.StartMovingToExit();
        }
    }
}
