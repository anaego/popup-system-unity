using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO add interface?
public class PopupQueue 
{
    private static PopupQueue instance;
    
    private Queue<PopupData> popupQueue;
    private PopupPool popupPool;

    private PopupSettingsScriptableObject popupSettingsSO;
    private Transform popupParent;

    private PopupQueue()
    { 
    }

    public static PopupQueue GetInstance(PopupSettingsScriptableObject settings, Transform popupParent)
    {
        if (instance == null)
        {
            instance = new PopupQueue();
            instance.InitializeQueue(settings, popupParent);
        }
        return instance;
    }

    public void AddToQueue(PopupData popupData)
    {
        popupQueue.Enqueue(popupData);
        // TODO TEMP!!!
        ExecuteQueue();
    }

    public void ExecuteQueue()
    {
        var popupData = popupQueue.Dequeue();
        var popupController = popupPool.GetObjectFromPool();
        popupController.SetupViewWithData(popupData);
    }

    private void InitializeQueue(PopupSettingsScriptableObject settings, Transform popupParent)
    {
        popupSettingsSO = settings;
        this.popupParent = popupParent;
        popupQueue = new Queue<PopupData>();
        popupPool = new PopupPool(
            CreatePopupController, 
            GetPopupController, 
            ReleasePopupController, 
            settings.MaxPopupsSimultaneously);
    }

    private PopupController CreatePopupController()
    {
        var popupView = UnityEngine.Object.Instantiate(popupSettingsSO.PopupViewPrefab, popupParent);
        var popupController = new PopupController(popupView);
        popupController.IsShown = false;
        return popupController;
    }

    private void GetPopupController(PopupController popupController)
    {
        popupController.SetLastVisually();
    }

    private void ReleasePopupController(PopupController popupController)
    {
        popupController.IsShown = false;
    }
}
