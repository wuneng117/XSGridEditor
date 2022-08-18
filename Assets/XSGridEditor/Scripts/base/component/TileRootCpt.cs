using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XSSLG
{
    public class TileRootCpt : MonoBehaviour, ITileRoot
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector3 InverseTransformPoint(Vector3 Pos) => this.transform.InverseTransformPoint(Pos);
        public Vector3 TransformPoint(Vector3 pos) => this.transform.TransformPoint(pos);
        public void ClearAllTiles()=> XSUE.RemoveChildren(this.gameObject);
    }
}
