// ----------------------------------------------------------------------------
// <copyright file="CustomTypes.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2011 Exit Games GmbH
// </copyright>
// <summary>
//   
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------
using ExitGames.Client.Photon;
using UnityEngine;


public static class CustomTypes
{

    public static void Register()
    {
        PhotonPeer.RegisterType(typeof(Vector2), (byte)'W', SerializeVector2, DeserializeVector2);
        PhotonPeer.RegisterType(typeof(Vector3), (byte)'V', SerializeVector3, DeserializeVector3);
        PhotonPeer.RegisterType(typeof(Transform), (byte)'T', SerializeTransform, DeserializeTransform);
        PhotonPeer.RegisterType(typeof(Quaternion), (byte)'Q', SerializeQuaternion, DeserializeQuaternion);
    }

    #region Custom De/Serializer Methods

    public static readonly byte[] memTransform = new byte[2 * 3 * 4];

    private static short SerializeTransform(StreamBuffer outStream, object customObject)
    {
        Transform t = customObject as Transform;
        short count = 2 * 3 * 4;
        lock (memTransform)
        {
            int index = 0;
            byte[] bytes = memTransform;
            Protocol.Serialize(t.position.x, bytes, ref index);
            Protocol.Serialize(t.position.y, bytes, ref index);
            Protocol.Serialize(t.position.z, bytes, ref index);
            Protocol.Serialize(t.eulerAngles.x, bytes, ref index);
            Protocol.Serialize(t.eulerAngles.y, bytes, ref index);
            Protocol.Serialize(t.eulerAngles.z, bytes, ref index);
            outStream.Write(bytes, 0, count);
        }
        return count;
    }

    private static object DeserializeTransform(StreamBuffer inStream, short length)
    {
        GameObject go = new GameObject();
        Transform transform = go.transform;
        lock (memTransform)
        {
            inStream.Read(memTransform, 0, 2 * 3 * 4);
            int index = 0;
            Vector3 position = new Vector3();
            Protocol.Deserialize(out position.x, memTransform, ref index);
            Protocol.Deserialize(out position.y, memTransform, ref index);
            Protocol.Deserialize(out position.z, memTransform, ref index);
            transform.position = position;
            Vector3 eulerAngles = new Vector3();
            Protocol.Deserialize(out eulerAngles.x, memTransform, ref index);
            Protocol.Deserialize(out eulerAngles.y, memTransform, ref index);
            Protocol.Deserialize(out eulerAngles.z, memTransform, ref index);
            transform.eulerAngles = eulerAngles;
        }
        return transform;
    }

    public static readonly byte[] memVector3 = new byte[3 * 4];

    private static short SerializeVector3(StreamBuffer outStream, object customobject)
    {
        Vector3 vo = (Vector3)customobject;

        int index = 0;
        lock (memVector3)
        {
            byte[] bytes = memVector3;
            Protocol.Serialize(vo.x, bytes, ref index);
            Protocol.Serialize(vo.y, bytes, ref index);
            Protocol.Serialize(vo.z, bytes, ref index);
            outStream.Write(bytes, 0, 3 * 4);
        }

        return 3 * 4;
    }

    private static object DeserializeVector3(StreamBuffer inStream, short length)
    {
        Vector3 vo = new Vector3();
        lock (memVector3)
        {
            inStream.Read(memVector3, 0, 3 * 4);
            int index = 0;

            Protocol.Deserialize(out vo.x, memVector3, ref index);
            Protocol.Deserialize(out vo.y, memVector3, ref index);
            Protocol.Deserialize(out vo.z, memVector3, ref index);
        }

        return vo;
    }


    public static readonly byte[] memVector2 = new byte[2 * 4];

    private static short SerializeVector2(StreamBuffer outStream, object customobject)
    {
        Vector2 vo = (Vector2)customobject;
        lock (memVector2)
        {
            byte[] bytes = memVector2;
            int index = 0;
            Protocol.Serialize(vo.x, bytes, ref index);
            Protocol.Serialize(vo.y, bytes, ref index);
            outStream.Write(bytes, 0, 2 * 4);
        }

        return 2 * 4;
    }

    private static object DeserializeVector2(StreamBuffer inStream, short length)
    {
        Vector2 vo = new Vector2();
        lock (memVector2)
        {
            inStream.Read(memVector2, 0, 2 * 4);
            int index = 0;
            Protocol.Deserialize(out vo.x, memVector2, ref index);
            Protocol.Deserialize(out vo.y, memVector2, ref index);
        }

        return vo;
    }


    public static readonly byte[] memQuarternion = new byte[4 * 4];

    private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
    {
        Quaternion o = (Quaternion)customobject;

        lock (memQuarternion)
        {
            byte[] bytes = memQuarternion;
            int index = 0;
            Protocol.Serialize(o.w, bytes, ref index);
            Protocol.Serialize(o.x, bytes, ref index);
            Protocol.Serialize(o.y, bytes, ref index);
            Protocol.Serialize(o.z, bytes, ref index);
            outStream.Write(bytes, 0, 4 * 4);
        }

        return 4 * 4;
    }

    private static object DeserializeQuaternion(StreamBuffer inStream, short length)
    {
        Quaternion o = new Quaternion();

        lock (memQuarternion)
        {
            inStream.Read(memQuarternion, 0, 4 * 4);
            int index = 0;
            Protocol.Deserialize(out o.w, memQuarternion, ref index);
            Protocol.Deserialize(out o.x, memQuarternion, ref index);
            Protocol.Deserialize(out o.y, memQuarternion, ref index);
            Protocol.Deserialize(out o.z, memQuarternion, ref index);
        }

        return o;
    }

    #endregion
}