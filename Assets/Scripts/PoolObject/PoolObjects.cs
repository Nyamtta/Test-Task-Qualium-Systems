using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{

    [SerializeField] private List<PoolObject> Pool = default;

	#region Singleton
	
    public static PoolObjects Instans;

    private void Awake()
    {
        if(Instans == null)
            Instans = this;
        else
            Destroy(this);
    }

	#endregion

    public GameObject GetFromPool(string id)
    {
        foreach(var item in Pool)
        {
            if(item.Id == id)
            {
                GameObject temp = item.GetObj();
                temp.transform.parent = transform;
                return temp;
            }
        }

        Debug.LogError("Not correct Id");
        return null;
    }

    public void ReturnObjectInPool(string id, GameObject obj)
    {
        foreach(var item in Pool)
        {
            if(item.Id == id)
                item.ReturnObjectToPool(obj);
        }
    }

	[Serializable]
    public class PoolObject {

        public string Id;
        public GameObject Object;
        private Queue<GameObject> Pool = new Queue<GameObject>();   

        public void ReturnObjectToPool(GameObject obj)
        {
            obj.SetActive(false);

            Pool.Enqueue(obj);
        }

        public GameObject GetObj()
        {
            if(Pool.Count == 0)
            {
                return Instantiate(Object);
            }
            else
            {
                return Pool.Dequeue();
            }
        }

    }
}
