using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UrlManager
{
    public static void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
}
