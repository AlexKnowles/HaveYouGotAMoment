using HaveYouGotAMoment.Managers;
using UnityEngine;

namespace HaveYouGotAMoment.Tenants
{
	public class TenantDialog : MonoBehaviour
	{
		public string[] InitialResponses;
		public string[] PositiveResponses;
		public string[] NegativeResponses;

		public GameObject DialogSpawnPoint;

		private DialogManager _dialogManager;
		private TenantPackageHandler _tenantPackageHandler;
		private TenantMovement _tenantMovement;
		private bool _dialogStarted = false;
		private float _timeSinceLastMessage = 0;
		private bool _initialResponseHasBeenSent = false;
		private bool _packageReviewOutcome = false;

		// Start is called before the first frame update
		void Start()
		{
			if (_dialogManager is null)
			{
				_dialogManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<DialogManager>();
			}

			if (_tenantPackageHandler is null)
			{
				_tenantPackageHandler = GetComponent<TenantPackageHandler>();
			}

			if (_tenantMovement is null)
			{
				_tenantMovement = GetComponent<TenantMovement>();
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (!_dialogStarted)
			{
				return;
			}

			_timeSinceLastMessage += Time.deltaTime;

			if (_timeSinceLastMessage > 1.2f)
			{
				_timeSinceLastMessage = 0;

				if (!_initialResponseHasBeenSent)
				{
					int randomMessage = Random.Range(0, InitialResponses.Length);

					_dialogManager.SendTenantMessage(InitialResponses[randomMessage]);

					_packageReviewOutcome = _tenantPackageHandler.ReviewGivenPackage();

					_initialResponseHasBeenSent = true;
				}
				else
				{
					if (_packageReviewOutcome)
					{
						int randomMessage = Random.Range(0, PositiveResponses.Length);
						_dialogManager.SendTenantMessage(PositiveResponses[randomMessage]);
						_tenantPackageHandler.TakeGivenPackage();
					}
					else
					{
						int randomMessage = Random.Range(0, NegativeResponses.Length);
						_dialogManager.SendTenantMessage(NegativeResponses[randomMessage]);
						_tenantPackageHandler.RejectPackage();
					}

					_tenantMovement.Go();
					_dialogManager.StartFadeOut();
					_dialogStarted = false;
				}
			}
		}

		public void BeginDialog()
		{
			_dialogManager.ShowDialog(DialogSpawnPoint, _tenantMovement.MovingDirection);
			_dialogManager.SendPlayerHello();

			_initialResponseHasBeenSent = false;
			_dialogStarted = true;
		}
	}
}
