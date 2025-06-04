using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SnapshotManager : MonoBehaviour
{
    private string screenshotName = "AR_Snapshot.png";

    public void TakeSnapshot()
    {
        string path = Path.Combine(Application.persistentDataPath, screenshotName);
        ScreenCapture.CaptureScreenshot(screenshotName);
        Debug.Log($"📸 Screenshot saved to: {path}");
    }
}
