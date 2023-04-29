using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HaveYouGotAMoment.Tenants
{
    public class TenantClick : MonoBehaviour
	{
		public UnityEvent OnClickTenant;

		private Camera _mainCamera;

		// Start is called before the first frame update
		void Start()
		{
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			}
		}

        // Update is called once per frame
        void Update()
        {
			if(OnClickTenant is null)
			{ 
				return;
			}

			if (Input.GetMouseButtonUp(0))
			{
				Vector2 mouseInWorldSpace = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mouseInWorldSpace, Vector2.zero);

				if (hit.collider != null && hit.collider.gameObject == gameObject)
				{
					OnClickTenant.Invoke();
				}
			}
		}
    }
}
