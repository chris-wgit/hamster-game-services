using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesertFoxTeam
{
    public class UIBillboard : MonoBehaviour
    {
        private Transform camTrans;

        private Transform trans;
        // Start is called before the first frame update
        private void Awake()
        {
            camTrans = Camera.main.transform;
            trans = transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(trans.position + camTrans.rotation * Vector3.forward, camTrans.rotation * Vector3.up);
        }
    }
}

