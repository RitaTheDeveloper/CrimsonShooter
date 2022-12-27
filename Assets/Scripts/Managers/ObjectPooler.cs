using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;

    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            yellow_bullet,
            red_bullet,
            green_bullet,
            spiderSmall,
            spiderBig,
            zombie,
            zombieSpawner
        }

        public ObjectType Type;
        public GameObject Prefab;
        //public GameObject PrefabModel;
        public int StartCount;
    }

    [SerializeField]
    private List<ObjectInfo> objectsInfo;

    private Dictionary<ObjectInfo.ObjectType, Pool> pools;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitPool();
    }

    private void InitPool()
    {
        pools = new Dictionary<ObjectInfo.ObjectType, Pool>();

        // пустой объект из которого будут создаваться объекты-контейнеры
        var emptyGO = new GameObject();
        
        // для каждого пула создаем конрейнер с именем типа пула, и добавляем его в dictionary
        foreach (var obj in objectsInfo)
        {
            var container = Instantiate(emptyGO, transform, false);
            container.name = obj.Type.ToString();
            pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var go = InstantiateObject(obj.Type, container.transform);
                pools[obj.Type].Objects.Enqueue(go); // добавляем в очередь объекта в пуле
            }
        }

        Destroy(emptyGO); // удаляем пустой объект
    }

    // создаем объект
    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        var go = Instantiate(objectsInfo.Find(x => x.Type == type).Prefab, parent);
        go.SetActive(false);
        return go;
    }



    // получаеем нужный нам объект, если он есть в пуле, если нет - создаем новый
    public GameObject GetObject(ObjectInfo.ObjectType type, Vector3 position, Quaternion rotation)
    {
        var obj = pools[type].Objects.Count > 0 ?
            pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        obj.SetActive(true);

        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }
    
}
