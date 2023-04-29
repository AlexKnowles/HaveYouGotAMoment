using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class CourierManager : MonoBehaviour
    {
        public GameObject SignatureClipboard;
        public GameObject CourierTargetPosition;
        public GameObject CourierEntranceExitPosition;

        public GameObject CourierPrefab;

        private GameObject _courierContainerInWorld;

        private Dictionary<string, CourierConfiguration> courierConfigs = new Dictionary<string, CourierConfiguration>()
        {
            { "SGK", new CourierConfiguration {
                DeliveryTime = 7.8f,
                ErrorMargin = 0.5f,
                WaitTime = 30
            } },
            { "Valdivian", new CourierConfiguration {
                DeliveryTime = 11f,
                ErrorMargin = 2f,
                WaitTime = 15
            }  },
            { "Sovereign Post", new CourierConfiguration {
                DeliveryTime = 14.5f,
                ErrorMargin = 1f,
                WaitTime = 45
            }  },
        };

        private Dictionary<string, GameObject> couriers = new Dictionary<string, GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            OnBeginGame();
            OnBeginDay();
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        private void OnBeginGame()
        {
            _courierContainerInWorld = new GameObject("CourierContainer");
        }

        private void OnBeginDay()
        {
            foreach (var (courierName, courierConfig) in courierConfigs)
            {
                var courier = Instantiate(CourierPrefab, _courierContainerInWorld.transform);
                var data = courier.GetComponent<Couriers.CourierData>();
                data.CourierName = courierName;
                data.MaxWaitTimeInSeconds = courierConfig.WaitTime;
                // Add delivery names here:
                data.Deliveries = new string[0];
                var movement = courier.GetComponent<Couriers.CourierMovement>();
                movement.targetPosition = CourierTargetPosition;
                movement.entranceExitPosition = CourierEntranceExitPosition;
                courier.GetComponent<Couriers.CourierDelivery>().SignatureClipboard = SignatureClipboard;

                // Bit gnarly since we're relying on the thing being setup
                // but what can you do...
                courier.GetComponent<Scheduler>().Schedule[0].HourInDay = courierConfig.DeliveryTime + (Random.Range(-1.0f, 1.0f) * courierConfig.ErrorMargin);
                couriers.Add(courierName, courier);
            };
        }

        private void OnEndDay()
        {
            foreach (var (_, courier) in couriers)
            {
                Destroy(courier);
            }
            couriers = new Dictionary<string, GameObject>();
        }
    }

    public struct CourierConfiguration
    {
        public float DeliveryTime;
        public float ErrorMargin;
        public int WaitTime;
    }
}
