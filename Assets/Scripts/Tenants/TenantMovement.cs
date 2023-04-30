using log4net.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Tenants
{
    public class TenantMovement : MonoBehaviour
	{
		public Vector3 FrontDoorSpawnPoint = new Vector3(11, 0, 2);
		public Vector3 OfficeDoorSpawnPoint = new Vector3(-11, 0, 2);
		public Vector3 SpeakPoint = new Vector3(0, 0, 2);
		public float Speed = 2.0f;

		private int _direction = 0;
		private bool _stopMoving = false;

		// Start is called before the first frame update
		void Start()
        {
			EnterFromFrontDoor();
			_direction = -1;
		}

        // Update is called once per frame
        void Update()
		{
			if (_stopMoving)
			{
				return;
			}

			if((_direction == 1 && transform.position.x < OfficeDoorSpawnPoint.x)
				|| (_direction == -1 && transform.position.x > FrontDoorSpawnPoint.x))
			{
				return;
			}

			Vector3 nextPosition = transform.position + (Vector3.left * _direction);
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * Speed);

			float yOffset = Mathf.Sin(Time.time * 10) * Speed * 0.0005f;
			transform.localPosition += new Vector3(0, yOffset, 0);
		}

        public void EnterFromFrontDoor()
		{
			SetPosition(FrontDoorSpawnPoint);
			_direction = 1;
			UpdateLookDirection();
		}

		public void EnterFromOffice()
		{
			SetPosition(OfficeDoorSpawnPoint);
			_direction = -1;
			UpdateLookDirection();
		}

		public void Stop()
		{
			_stopMoving = true;
		}

		private void SetPosition(Vector3 newPosition)
		{
			if (!_stopMoving)
			{
				transform.position = newPosition;
			}
		}

		private void UpdateLookDirection()
		{
			transform.localScale = new Vector3(_direction, transform.localScale.y, transform.localScale.z);
		}

		internal void Go()
		{
			_stopMoving = false;
		}
	}
}
