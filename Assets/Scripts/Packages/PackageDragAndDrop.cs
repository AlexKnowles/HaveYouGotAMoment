using UnityEngine;

namespace HaveYouGotAMoment.Packages
{
	public class PackageDragAndDrop : MonoBehaviour
	{
		private Camera _mainCamera;
		private Rigidbody2D _rigidbody;
		private TenantManager _tenantManager;
		private bool _isBeingDragged = false;

		// Start is called before the first frame update
		void Start()
		{
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			}

			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody2D>();
			}

			if (_tenantManager == null)
			{
				_tenantManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<TenantManager>();
			}
		}

		// Update is called once per frame
		void Update()
		{

			if (!_isBeingDragged && Input.GetMouseButtonDown(0))
			{
				Vector2 mouseInWorldSpace = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(mouseInWorldSpace, Vector2.zero);

				if (hit.collider != null && hit.collider.gameObject == gameObject)
				{
					_isBeingDragged = true;
				}
			}

			if (_isBeingDragged && Input.GetMouseButtonUp(0))
			{
				_isBeingDragged = false;

				Vector2 mouseInWorldSpace = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
				_tenantManager.TryGivePackageToTenant(mouseInWorldSpace, gameObject);
			}

			if (_isBeingDragged)
			{
				Vector3 mouseInWorldSpace = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
				transform.position = new Vector3(mouseInWorldSpace.x, mouseInWorldSpace.y, transform.position.z);
			}

			_rigidbody.gravityScale = (_isBeingDragged ? 0 : 1);
		}
	}
}
