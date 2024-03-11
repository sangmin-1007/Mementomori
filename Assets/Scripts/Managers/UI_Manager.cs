using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{   
    Dictionary<string, GameObject> UI_List = new Dictionary<string, GameObject>();

    [HideInInspector] public string sceneName;


    /// <summary>
    /// UI를 보여줍니다 받아올 UI가 없으면 생성 후 보여줍니다
    /// </summary>
    public T ShowUI<T>(Transform parent = null) where T : Component
    {
        if (UI_List.ContainsKey(typeof(T).Name) && UI_List[typeof(T).Name] != null)
        {
            UI_List[typeof(T).Name].SetActive(true);
            return UI_List[typeof(T).Name].GetComponent<T>();
        }
        else
            return CreateUI<T>(parent);
    }

    public T CreateUI<T>(Transform parent = null)
    {
        try
        {
            if (IsUIExit<T>())
                UI_List.Remove(typeof(T).Name);

            GameObject go = Instantiate(Resources.Load<GameObject>(GetPath<T>()), parent);
          
            T temp = go.GetComponent<T>();
            AddUI<T>(go);

            return temp;

        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return default;
    }

    public void AddUI<T>(GameObject go)
    {
        if(UI_List.ContainsKey(typeof(T).Name) == false)
            UI_List.Add(typeof(T).Name, go);
    }

    public bool IsUIExit<T>()
    {
        if (UI_List.ContainsKey(typeof(T).Name))
            return true;
        else
            return false;
    }

    private string GetPath<T>()
    {
        string className = typeof(T).Name;
        return "Prefabs/UI/" + className;
    }

    /// <summary>
    /// LoadingUI를 보여줍니다 (가고싶은 씬 이름)
    /// </summary>
    public T ShowLoadingUI<T>(string loadSceneName)
    {
        sceneName = loadSceneName;

        if (UI_List.ContainsKey(typeof(T).Name) && UI_List[typeof(T).Name] != null)
        {
            UI_List[typeof(T).Name].SetActive(true);
            return UI_List[typeof(T).Name].GetComponent<T>();
        }
        else
            return CreateUI<T>();
    }

    public void RemoveUI<T>()
    {
        string className = typeof(T).Name;

        if(UI_List.ContainsKey(className))
        {
            UI_List.Remove(className);
        }
    }

    public void DestroyUI<T>()
    {
        string classname = typeof(T).Name;
        if (UI_List.ContainsKey(classname))
        {
            if (UI_List[classname].gameObject != null)
                Destroy(UI_List[classname]);

            UI_List.Remove(classname);
        }
    }

    public bool IsActive<T>()
    {
        if(IsUIExit<T>() && UI_List[typeof(T).Name] == null)
        {
            RemoveUI<T>();
            return false;
        }

        if(IsUIExit<T>() && UI_List[typeof(T).Name].activeSelf)
        {
            return UI_List[typeof(T).Name].GetComponent<UI_Base<T>>().IsEnabled;
        }
        else
            return false;
    }
}
