using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class Clock : MonoBehaviour
    {
        private Managers.DayManager _dayManager;

        public GameObject HoursHand;
        public GameObject MinutesHand;
        // Start is called before the first frame update
        void Start()
        {
            if (_dayManager == null)
            {
                _dayManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<Managers.DayManager>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            float currentHour = _dayManager.CurrentGameTime;
            if (currentHour > 12)
            {
                currentHour -= 12;
            }
            float current12HourPercentage = 0.0833333f * currentHour;
            float currentMinutePercentage = currentHour % 1;

            //inverted
            HoursHand.transform.rotation = Quaternion.Euler(0, 0, -(360f * current12HourPercentage));
            MinutesHand.transform.rotation = Quaternion.Euler(0, 0, -(360f * currentMinutePercentage));
        }
    }
}
