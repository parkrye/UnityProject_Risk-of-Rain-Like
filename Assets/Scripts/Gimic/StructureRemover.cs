using System.Collections.Generic;
using UnityEngine;

public class StructureRemover : MonoBehaviour
{
    [SerializeField] GameObject[] structureRoots;
    [SerializeField] List<GameObject> structures;

    public void StartRemove()
    {
        if(structureRoots.Length > 0)
        {
            for (int i = 0; i < structureRoots.Length; i++)
            {
                Transform[] structureGroup = structureRoots[i].GetComponentsInChildren<Transform>();
                for(int j = 1; j < structureGroup.Length; j++)
                {
                    structures.Add(structureGroup[j].gameObject);
                }
            }    
        }

        if(structures.Count > 0)
        {
            int num;
            for (int i = 0; i < structures.Count; i++)
            {
                num = Random.Range(0, 3);
                if (num == 0)
                {
                    structures[i].SetActive(false);
                }
            }
        }
    }
}
