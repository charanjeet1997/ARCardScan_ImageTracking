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
        public virtual void Initialize() { }
        public abstract void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage);
    }
    
    public class TrackedStateHandler : ARImageTrackingStateHandler
    {
        public override void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage)
        {
            Debug.Log("Tracked State Handler");
            trackedObject.UpdateTrackedObject(cardData,trackedImage);
        }
    }
    
    public class LimitedStateHandler : ARImageTrackingStateHandler
    {
        float maxTime = 5f;
        float currentTime = 0f;
        
        public override void Initialize()
        {
            currentTime = 0f;
        }
        public override void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage)
        {
            Debug.Log("Limited State Handler");
            if (currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                if (cardData.cardName == trackedImage.referenceImage.name)
                {
                    trackedObject.PauseTrackingObject();
                }
                else
                {
                    trackedObject.StopTrackingObject();
                }
                currentTime = 0f;
            }
        }
    }
    
    public class NoneStateHandler : ARImageTrackingStateHandler
    {
        public override void HandleState(CardData cardData, ARTrackedObject trackedObject, ARTrackedImage trackedImage)
        {
            Debug.Log("None State Handler");
            trackedObject.StopTrackingObject();
        }
    }
    
}