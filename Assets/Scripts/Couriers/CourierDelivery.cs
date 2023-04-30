using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Couriers
{
    public class CourierDelivery : MonoBehaviour
    {
        public GameObject SignatureClipboard;

        private CourierMovement _courierMovement;
        private CourierData _courierData;

        private SignForPackage _signForPackage;

        private bool _waiting = false;
        private float _secondsWaiting = 0f;

        // Start is called before the first frame update
        void Start()
        {
            _courierMovement = gameObject.GetComponent<CourierMovement>();
            _courierData = gameObject.GetComponent<CourierData>();

            _signForPackage = SignatureClipboard.GetComponent<SignForPackage>();
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
            _secondsWaiting = 0f;
            _waiting = true;
            _signForPackage.StartGettingSignature(gameObject);
        }

        public void EndDelivery()
        {
            _waiting = false;
            _courierMovement.StartMovingToExit();
        }
    }
}
