using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Couriers
{
    public class CourierStyling : MonoBehaviour
    {
        public GameObject CourierHat;
        public GameObject CourierBody;

        private SpriteRenderer _courierHatRenderer;
        private SpriteRenderer _courierBodyRenderer;

        private CourierData _courierData;
        // Start is called before the first frame update
        void Start()
        {
            if (_courierHatRenderer == null)
            {
                _courierHatRenderer = CourierHat.GetComponent<SpriteRenderer>();
            }

            if (_courierBodyRenderer == null)
            {
                _courierBodyRenderer = CourierBody.GetComponent<SpriteRenderer>();
            }

            _courierData = GetComponent<CourierData>();

            _courierHatRenderer.color = _courierData.SecondaryColor;
            _courierBodyRenderer.color = _courierData.Color;
        }

        public void Repaint()
        {
            _courierData = GetComponent<CourierData>();

            _courierHatRenderer.color = _courierData.SecondaryColor;
            _courierBodyRenderer.color = _courierData.Color;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
