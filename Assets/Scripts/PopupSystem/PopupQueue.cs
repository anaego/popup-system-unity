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

    private bool CanShowNewPopup => popupPool.ActivePopups < popupSettingsSO.MaxPopupsSimultaneously;
    private bool IsQueueEmpty => popupQueue.Count == 0;

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
        if (CanShowNewPopup && IsQueueEmpty)
        {
            Execute(popupData);
            return;
        }
        popupQueue.Enqueue(popupData);
    }

    public void ExecuteQueue()
    {
        if (!CanShowNewPopup || IsQueueEmpty)
        {
            return;
        }
        var popupData = popupQueue.Dequeue();
        Execute(popupData);
    }

    private void Execute(PopupData popupData)
    {
        var popupController = popupPool.GetObjectFromPool();
        popupController.SetupViewWithData(popupData, OnObjectDone);
    }

    private void OnObjectDone(PopupController popupController)
    {
        popupPool.ReleaseObjectToPool(popupController);
        ExecuteQueue();
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
        var popupController = new PopupController(popupView, popupSettingsSO);
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
