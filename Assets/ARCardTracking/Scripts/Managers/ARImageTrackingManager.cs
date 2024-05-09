using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Game.ARCardTracking
{
    /// <summary>
    /// This class is responsible for managing the AR Image Tracking.
    /// It listens to the ARTrackedImageManager's trackedImagesChanged event and updates the tracked object based on the tracking state.
    /// It also contains a dictionary of ARImageTrackingStateHandler objects that handle the tracking state.
    /// The ARImageTrackingStateHandler objects are responsible for handling the tracking state of the ARTrackedImage.
    /// </summary>
    public class ARImageTrackingManager : MonoBehaviour
    {
        private Dictionary<TrackingState,ARImageTrackingStateHandler> stateHandlers = new Dictionary<TrackingState, ARImageTrackingStateHandler>();
        private CardData cardData;
        
        [Header("Card Data Container")]
        [SerializeField] private CardDataContainer cardDataContainer;
        
        [Header("AR Tracked Object")]
        [SerializeField] private ARTrackedObject trackedObject;

        [Header("AR Foundation Components")]
        [SerializeField] private ARTrackedImageManager trackedImageManager;

        private void OnEnable()
        {
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        private void Start()
        {
            if (stateHandlers == null)
            {
                stateHandlers = new Dictionary<TrackingState, ARImageTrackingStateHandler>();
            }
            
            stateHandlers.Add(TrackingState.None, new NoneStateHandler());
            stateHandlers.Add(TrackingState.Tracking, new TrackedStateHandler());
            stateHandlers.Add(TrackingState.Limited, new LimitedStateHandler());
        }

        private void OnDisable()
        {
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var trackedImage in eventArgs.added)
            {
                OnTrackedImageUpdated(trackedImage);
            }

            foreach (var trackedImage in eventArgs.updated)
            {
                OnTrackedImageUpdated(trackedImage);
            }

            foreach (var trackedImage in eventArgs.removed)
            {
                OnTrackedImageRemoved(trackedImage);
            }
        }
        
        private void OnTrackedImageUpdated(ARTrackedImage trackedImage)
        {
            cardData = cardDataContainer.cardDataList.Find(x => x.cardName == trackedImage.referenceImage.name);
            if(cardData != null)
            {
                stateHandlers[trackedImage.trackingState].HandleState(cardData, trackedObject, trackedImage);
            }
            //stateHandlers[trackedImage.trackingState].HandleState(trackedObject, trackedImage, trackedImage.trackingState);
        }
        
        private void OnTrackedImageRemoved(ARTrackedImage trackedImage)
        {
            stateHandlers[TrackingState.None].HandleState(cardData, trackedObject, trackedImage);
            //stateHandlers[trackedImage.trackingState].HandleState(trackedObject, trackedImage, trackedImage.trackingState);
        }
    }
}