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
		public TenantMovingDirection MovingDirection { get; private set; }
		
		private int _direction = 0;
		private bool _stopMoving = false;

		// Start is called before the first frame update
		void Start()
        {
			EnterFromFrontDoor();
			MovingDirection = TenantMovingDirection.Right;
		}

        // Update is called once per frame
        void Update()
		{
			if (_stopMoving)
			{
				return;
			}

			if((MovingDirection == TenantMovingDirection.Left && transform.position.x < OfficeDoorSpawnPoint.x)
				|| (MovingDirection == TenantMovingDirection.Right && transform.position.x > FrontDoorSpawnPoint.x))
			{
				return;
			}

			int directionModifier = (MovingDirection == TenantMovingDirection.Right ? -1 : 1);
			Vector3 nextPosition = transform.position + (Vector3.left * directionModifier);
			transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * Speed);

			float yOffset = Mathf.Sin(Time.time * 10) * Speed * 0.0005f;
			transform.localPosition += new Vector3(0, yOffset, 0);
		}

        public void EnterFromFrontDoor()
		{
			MovingDirection = TenantMovingDirection.Left;
			SetPosition(FrontDoorSpawnPoint);
			UpdateLookDirection();
		}

		public void EnterFromOffice()
		{
			MovingDirection = TenantMovingDirection.Right;

			SetPosition(OfficeDoorSpawnPoint);
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
			int directionModifier = (MovingDirection == TenantMovingDirection.Right ? -1 : 1);
			transform.localScale = new Vector3(directionModifier, transform.localScale.y, transform.localScale.z);
		}

		internal void Go()
		{
			_stopMoving = false;
		}
	}
}
