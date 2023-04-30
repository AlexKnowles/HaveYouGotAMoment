using System.Collections;
using System.Collections.Generic;
using HaveYouGotAMoment.Couriers;
using UnityEngine;

namespace HaveYouGotAMoment.Packages
{
    public class PackageStyling : MonoBehaviour
    {
        public GameObject Tape;

        private SpriteRenderer _tapeRenderer;

        private PackageData _packageData;
        // Start is called before the first frame update
        void Start()
        {
            if (_tapeRenderer == null)
            {
                _tapeRenderer = Tape.GetComponent<SpriteRenderer>();
            }


            _packageData = GetComponent<PackageData>();

            _tapeRenderer.color = _packageData.TapeColor;
        }

        public void Repaint()
        {
            _packageData = GetComponent<PackageData>();

            _tapeRenderer.color = _packageData.TapeColor;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
