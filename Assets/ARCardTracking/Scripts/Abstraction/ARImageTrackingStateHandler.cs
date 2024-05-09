using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Game.ARCardTracking
{
    /// <summary>
    /// This class is responsible for handling the tracking state of the ARTrackedImage.
    /// </summary>
    public abstract class ARImageTrackingStateHandler
    {
        public abstract void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage);
    }
    
    public class TrackedStateHandler : ARImageTrackingStateHandler
    {
        public override void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage)
        {
            trackedObject.UpdateTrackedObject(cardData,trackedImage);
        }
    }
    
    public class LimitedStateHandler : ARImageTrackingStateHandler
    {
        public override void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage)
        {
            trackedObject.PauseTrackingObject();
        }
    }
    
    public class NoneStateHandler : ARImageTrackingStateHandler
    {
        public override void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage)
        {
            trackedObject.StopTrackingObject();
        }
    }
    
}