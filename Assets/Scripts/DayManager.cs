using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Managers
{
    public class DayManager : MonoBehaviour
    {
        public int GameHoursInRealSeconds = 30;
		public int PlayerShiftStartGameHour = 7;
        public int PlayerShiftEndGameHour = 18;
        public int GameDayLengthInHours = 24;

        public float CurrentGameTime = 0;
        public int CurrentGameDay = 0;


		// Start is called before the first frame update
		void Start()
        {
            StartNewDay();

		}

        // Update is called once per frame
        void Update()
        {
            CurrentGameTime += (Time.deltaTime / GameHoursInRealSeconds);

            if(CurrentGameTime >= PlayerShiftEndGameHour)
            {
                EndDay();
			}
        }

        private void StartNewDay()
		{
			CurrentGameDay++;
			CurrentGameTime = PlayerShiftStartGameHour;
		}

        private void EndDay()
        {
            // Do stuff

			StartNewDay();
		}
    }
}