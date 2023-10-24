
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public sealed class ObjectPool : Singleton<ObjectPool>
    {
        //***********
        //Variable
        //***********
        private Dictionary<GameObject, List<GameObject>> objectPools = new Dictionary<GameObject, List<GameObject>>();

        private List<GameObject> tempPool;
    
        //********
        //풀 생성
        //********
        public bool CreatePool(GameObject objToPool, int initialPoolSize)
        {
            if (objToPool == null)
                return false;

            if (objectPools.ContainsKey(objToPool))
            {
                return false;
            }
            else
            {
                List<GameObject> nPool = new List<GameObject>();

                for (int i = 0; i < initialPoolSize; i++)
                {
                    GameObject nObj = GameObject.Instantiate(objToPool, Vector3.zero, Quaternion.identity) as GameObject;

                    nObj.SetActive(false);

                    nObj.transform.parent = transform;

                    nPool.Add(nObj);
                }

                objectPools.Add(objToPool, nPool);

                return true;
            }
        }

        //********
        //풀 사용
        //********
        //NGI쪽 이펙트들때문에 객체가 켜지는 순간 ObjectPool전체가 UIPanel 밑으로 들어간다. 
        //그래서 무조껀 오브젝트는 false 상태로 존재하고 사용할때는 부모밑으로 가져가고 켜주고 끄고 가져온다.
        public GameObject GetObject(GameObject objToPool, Transform parent)
        {

            if (objectPools.ContainsKey(objToPool) == false)
            {
                //if (CreatePool(objToPool, 1))
                //    tempPool = objectPools[objToPool];
                //else
                //    return null; //오브젝트풀을 생성 못함!
                CreatePool(objToPool, 1);
            }

            tempPool = objectPools[objToPool];

            for (int i = 0; i < tempPool.Count; i++)
            {
                if (tempPool[i] != null)
                {
                    if (tempPool[i].activeSelf == false)
                    {
                        tempPool[i].transform.parent = parent;
                        tempPool[i].transform.localPosition = Vector3.zero;
                        tempPool[i].SetActive(true);
                        return tempPool[i];
                    }
                }
                else
                {
                    tempPool.Remove(null);
                }
            }

            GameObject nObj = GameObject.Instantiate(objToPool, Vector3.zero, Quaternion.identity) as GameObject;

            nObj.transform.parent = parent;
            nObj.transform.localPosition = Vector3.zero;
            nObj.SetActive(true);
            tempPool.Add(nObj);

            //Debug.LogError(nObj.name + " : " + nObj.transform.localScale);
            return nObj;
        }

        public T GetObject<T>(GameObject objToPool, Transform parent)
        {
            return GetObject(objToPool, parent).GetComponent<T>();
        }

        public void Restore(GameObject objToPool)
        {
            //추후에 ObjectPool을 사용한 객체들을 되돌릴때 어떤 문제가 발생할수 있으므로
            //그때 일괄처리를 하기 위해 무조껀 이 함수를 써서 SetActive(false)하도록 하자.

            objToPool.SetActive(false);
            objToPool.transform.parent = transform;  //사용완료한 객체들이 있는곳.    
            objToPool.transform.localScale = Vector3.one;
        }


        //***************
        //오브젝트 초기화
        //***************
        public void Restore_Obj(GameObject objToPool)
        {
            if (objectPools.ContainsKey(objToPool) == false)
                return;

            List<GameObject> listobj = objectPools[objToPool];

            for (int i = 0; i < listobj.Count; i++)
            {
                listobj[i].SetActive(false);
                listobj[i].transform.parent = transform;
            }
        }
    }

