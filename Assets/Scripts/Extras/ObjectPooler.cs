using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int amountForCreate;
    private List<GameObject> list;
    public GameObject listContainer { get; private set; }

    public void CreatePooler(GameObject objectForCreate)
    {
        list = new List<GameObject>();
        listContainer = new GameObject($"Pool - {objectForCreate.name}");
        for (int i = 0; i < amountForCreate; i++)
        {
            list.Add(AddInstance(objectForCreate));
        }
    }

    private GameObject AddInstance(GameObject objectForCreate)
    {
        GameObject newObject = Instantiate(objectForCreate,listContainer.transform);
        newObject.SetActive(false);
        return newObject;
    }

    public GameObject GetInstance()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
            {
                return list[i];
            }
        }
        return null;
    }

    public void DestroyPooler()
    {
        Destroy(listContainer);
        list.Clear();
    }
}
