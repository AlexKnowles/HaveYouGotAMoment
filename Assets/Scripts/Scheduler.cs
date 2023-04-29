using HaveYouGotAMoment.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HaveYouGotAMoment
{
	public class Scheduler : MonoBehaviour
	{
		public ScheduledItem[] Schedule;

		private DayManager _dayManager;
		private Dictionary<int, bool[]> _scheduleItemsDoneOnDay = new();

		// Start is called before the first frame update
		void Start()
		{
			if (_dayManager == null)
			{
				_dayManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<DayManager>();
			}

		}

		// Update is called once per frame
		void Update()
		{
			if (Schedule == null || Schedule.Length == 0)
			{
				return;
			}

			if (!_scheduleItemsDoneOnDay.ContainsKey(_dayManager.CurrentGameDay))
			{
				AddDayToCompletedSchedule(_dayManager.CurrentGameDay);
			}

			for (int i = 0; i < Schedule.Length; i++)
			{
				ScheduledItem item = Schedule[i];

				if (!_scheduleItemsDoneOnDay[_dayManager.CurrentGameDay][i] && _dayManager.CurrentGameTime >= item.HourInDay)
				{
					_scheduleItemsDoneOnDay[_dayManager.CurrentGameDay][i] = true;
					item.ActionToTake.Invoke();
				}
			}
		}

		private void AddDayToCompletedSchedule(int day)
		{
			_scheduleItemsDoneOnDay.Add(day, new bool[Schedule.Length]);
		}

	}

	[Serializable]
	public struct ScheduledItem
	{
		public float HourInDay;
		public UnityEvent ActionToTake;
	}
}
