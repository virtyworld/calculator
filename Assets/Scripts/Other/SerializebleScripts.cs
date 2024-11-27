using System.Collections.Generic;
using UnityEngine;

public class SerializebleScripts : MonoBehaviour
{
}
[System.Serializable]
public class JsonData
{
    public List<ResultData> data;
}

[System.Serializable]
public class ResultData
{
    public string result;
}