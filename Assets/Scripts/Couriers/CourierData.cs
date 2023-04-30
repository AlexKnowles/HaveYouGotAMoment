using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Couriers
{
    public class CourierData : MonoBehaviour
    {
        public string CourierName = "Generic";
        public Color Color = new Color(0.5f, 0.5f, 1f);
        public Color SecondaryColor = new Color(1f, 0.5f, 0.5f);
        public int MaxWaitTimeInSeconds = 30;

        public string[] Deliveries = new string[0];
    }
}
