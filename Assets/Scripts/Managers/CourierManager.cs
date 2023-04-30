using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public int MaxPackagesPerTenantPerDay = 3;

        private TenantManager _tenantManager;

        private Dictionary<string, CourierConfiguration> courierConfigs = new Dictionary<string, CourierConfiguration>()
        {
            { "SGK", new CourierConfiguration {
                DeliveryTime = 7.8f,
                ErrorMargin = 0.5f,
                WaitTime = 30,
                Primary = new Color(0.5f, 0.5f, 1f),
                Secondary = new Color(1f, 0.5f, 0.5f),
            } },
            { "Valdivian", new CourierConfiguration {
                DeliveryTime = 11f,
                ErrorMargin = 2f,
                WaitTime = 15,
                Primary = new Color(1f, 0.5f, 1f),
                Secondary = new Color(0.5f, 0.5f, 0.5f),
            }  },
            { "Sovereign Post", new CourierConfiguration {
                DeliveryTime = 14.5f,
                ErrorMargin = 1f,
                WaitTime = 45,
                Primary = new Color(1f, 0.5f, 0.5f),
                Secondary = new Color(0.5f, 1f, 0.5f),
            }  },
        };

        private Dictionary<string, GameObject> couriers = new Dictionary<string, GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            if (_tenantManager == null)
            {
                _tenantManager = GetComponent<TenantManager>();
            }
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
            var names = _tenantManager.GetTenantNames();

            List<(string, int)> namesAndPackageCounts = names.Select(x => (x, Random.Range(0, MaxPackagesPerTenantPerDay + 1))).ToList();

            foreach (var (courierName, courierConfig) in courierConfigs)
            {
                var deliveries = new List<string>();

                for(int i = 0; i < namesAndPackageCounts.Count; i++)
                {
                    if (Random.Range(0, 3) > 0 && namesAndPackageCounts[i].Item2 > 0)
                    {
                        deliveries = deliveries.Append(namesAndPackageCounts[i].Item1).ToList();
                        namesAndPackageCounts[i] = (namesAndPackageCounts[i].Item1,namesAndPackageCounts[i].Item2 - 1);
                    }
                }

                if (deliveries.Count > 0)
                {
                    var courier = Instantiate(CourierPrefab, _courierContainerInWorld.transform);
                    courier.name = "Courier" + courierName;
                    var data = courier.GetComponent<Couriers.CourierData>();
                    var styling = courier.GetComponent<Couriers.CourierStyling>();
                    data.CourierName = courierName;
                    data.MaxWaitTimeInSeconds = courierConfig.WaitTime;
                    data.Color = courierConfig.Primary;
                    data.SecondaryColor = courierConfig.Secondary;
                    // Add delivery names here:
                    data.Deliveries = deliveries.ToArray();

                    var movement = courier.GetComponent<Couriers.CourierMovement>();
                    movement.targetPosition = CourierTargetPosition;
                    movement.entranceExitPosition = CourierEntranceExitPosition;
                    courier.GetComponent<Couriers.CourierDelivery>().SignatureClipboard = SignatureClipboard;

                    // Bit gnarly since we're relying on the thing being setup
                    // but what can you do...
                    courier.GetComponent<Scheduler>().Schedule[0].HourInDay = courierConfig.DeliveryTime + (Random.Range(-1.0f, 1.0f) * courierConfig.ErrorMargin);
                    couriers.Add(courierName, courier);
                }
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
        public Color Primary;
        public Color Secondary;
    }
}
