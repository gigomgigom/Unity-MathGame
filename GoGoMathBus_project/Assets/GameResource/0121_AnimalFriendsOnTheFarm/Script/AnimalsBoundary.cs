using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsBoundary : MonoBehaviour
{
    [SerializeField] Game_AnimalFriendsOnTheFarm animalFriendsOnTheFarm;
    [SerializeField] RectTransform fenceRT;
    public RectTransform FenceRT => fenceRT;
    [SerializeField] RectTransform dragRT;
    public RectTransform DragRT => dragRT;
    [SerializeField] float updateInterval = 0.2f;

    List<Transform> childTransforms;
    int lastChildCount;
    float lastUpdateTime;

    void Start()
    {
        childTransforms = new List<Transform>();
        UpdateChildCount();
        SortChildrenByYPosition();
    }

    void Update()
    {
        // Update the Hierachy order at the specified interval
        if (transform.childCount > 0 && Time.time - lastUpdateTime >= updateInterval)
        {
            if (lastChildCount != transform.childCount) UpdateChildCount();
            SortChildrenByYPosition();
            lastUpdateTime = Time.time;
        }
    }

    void SortChildrenByYPosition()
    {
        // Sort the child transforms by Y position
        childTransforms.Sort((a, b) => b.position.y.CompareTo(a.position.y));

        // Set the Hierachy order based on the sorted child transforms
        for (int i = 0; i < childTransforms.Count; i++)
        {
            childTransforms[i].SetSiblingIndex(i);
        }
    }
    
    public void UpdateChildCount()
    {
        childTransforms.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms.Add(transform.GetChild(i));
        }
        lastChildCount = childTransforms.Count;
    }

    public void CheckFenceChildCount()
    {
        animalFriendsOnTheFarm.CheckFenceChildCount(fenceRT.childCount);
    }

}