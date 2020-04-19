using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    public List<Resource> Resources;

    public static List<ResourceType> GetMissingResources(List<ResourceTuple> neededResources)
    {
        List<ResourceType> missingResources = new List<ResourceType>();
        if (neededResources == null)
            return missingResources;

        foreach (var neededRes in neededResources)
        {
            var r = instance.Resources.Find(x => { return x.ResourceInfo.Type == neededRes.Type; });
            if (r == null || r.ResourceInfo.Count < neededRes.Count)
                missingResources.Add(neededRes.Type);
        }

        return missingResources;
    }

    public static void ConsumeResources(List<ResourceTuple> neededResources, List<ResourceTuple> effects)
    {
        var commonList = MergeResourceLists(neededResources, effects);
        foreach (var prod in commonList) //effects won't work properly if same resource is mentioned few times....
        {
            var r = instance.Resources.Find(x => { return x.ResourceInfo.Type == prod.Type; });
            if (r == null)
            {
                Debug.LogError("Couldn't find resource! Make sure scriptable object exists and it's registered in Resources");
                return;
            }
            r.ResourceInfo.Count -= prod.Count;
            if (r.ResourceInfo.Count < 0)
            {
                instance.Resources.Find(x => {return x.ResourceInfo.Type == ResourceType.Prosperity;}).ResourceInfo.Count--;
                r.ResourceInfo.Count = 0;
            }
        }
    }

    public static void ProduceResources(List<ResourceTuple> producedResources, List<ResourceTuple> effects)
    {
        var commonList = MergeResourceLists(producedResources, effects);
        foreach (var prod in commonList) //effects won't work properly if same resource is mentioned few times....
        {
            var r = instance.Resources.Find(x => { return x.ResourceInfo.Type == prod.Type; });
            if (r == null)
            {
                Debug.LogError("Couldn't find resource! Make sure scriptable object exists and it's registered in Resources");
                return;
            }

            r.ResourceInfo.Count += Mathf.Max(prod.Count, 0); //production can't be below 0
        }
    }

    private static List<ResourceTuple> MergeResourceLists(List<ResourceTuple> firstList, List<ResourceTuple> secondList)
    {        
        var commonList = new List<ResourceTuple>();
        if (firstList != null)
        {
            foreach (var pr in firstList)
            {
                var val = commonList.Find(x => { return x.Type == pr.Type; });
                if (val == null)
                {
                    commonList.Add(new ResourceTuple(pr.Type, pr.Count));
                }
                else
                {
                    val.Count += pr.Count;
                }
            }
        }
        if (secondList != null)
        {
            foreach (var pr in secondList)
            {
                var val = commonList.Find(x => { return x.Type == pr.Type; });
                if (val == null)
                {
                    commonList.Add(new ResourceTuple(pr.Type, pr.Count));
                }
                else
                {
                    val.Count += pr.Count;
                }
            }
        }
        return commonList;
    }

    public bool HasLooseCondition()
    {
        return instance.Resources.Find(x => {return x.ResourceInfo.Type == ResourceType.Prosperity; }).ResourceInfo.Count <= 0;
    }
}
