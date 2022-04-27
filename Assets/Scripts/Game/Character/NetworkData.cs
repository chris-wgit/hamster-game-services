using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkData
{
    public static short[] NetworkTransform(Transform input)
    {
        Vector3 currentRot = input.rotation.eulerAngles;

        short[] newData = new short[] { (short)(input.position.x * 100), (short)(input.position.z * 100), (short)currentRot.y };

        return newData;
    }

    public static short[] EncryptionTransformDestinationData(Transform inputData, Vector3 destination)
    {
        Vector3 currentRot = inputData.rotation.eulerAngles;

        short[] newData = new short[] { (short)(inputData.position.x * 100), (short)(inputData.position.z * 100), (short)currentRot.y , (short)(destination.x * 10), (short)(destination.z * 10) };

        return newData;
    }

    public static LocalTransform ConvertNetworkTransform(short[] inputData)
    {
        LocalTransform newData = new LocalTransform();

        newData.position = new Vector3((float)inputData[0] / 100, 0.9f, (float)inputData[1] / 100);
        newData.rotation = Quaternion.Euler(0, inputData[2], 0);

        return newData;
    }

    public static NetworkTransformDestination DecryptionTransformDestinationData(short[] inputData)
    {
        NetworkTransformDestination newData = new NetworkTransformDestination();

        newData.position = new Vector3((float)inputData[0] / 100, 0.9f, (float)inputData[1] / 100);
        newData.rotation = Quaternion.Euler(0, inputData[2], 0);
        newData.destination = new Vector3((float)inputData[3] / 100, 0f, (float)inputData[4] / 100);

        return newData;
    }
}

public struct NetworkTransform
{
    public short[] position;
    public short rotation;
}

public struct LocalTransform
{
    public Vector3 position;
    public Quaternion rotation;
}

public struct NetworkTransformDestination
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 destination;
}
