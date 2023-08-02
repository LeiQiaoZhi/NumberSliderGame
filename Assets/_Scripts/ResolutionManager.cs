using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions_;

    void Start()
    {
        resolutions_ = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions_.Length; i++)
        {
            string option = resolutions_[i].width + " x " + resolutions_[i].height;
            options.Add(option);

            if (resolutions_[i].width == Screen.currentResolution.width &&
                resolutions_[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void SetResolution()
    {
        int resolutionIndex = resolutionDropdown.value;
        Resolution resolution = resolutions_[resolutionIndex];
        XLogger.Log(Category.Settings,$"Resolution set to {resolution.width} x {resolution.height}.");
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
