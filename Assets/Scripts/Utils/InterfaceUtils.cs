using System.Collections.Generic;
using UnityEngine;

public class InterfaceUtils
{
    /// <summary>
    /// ����̃C���^�t�F�[�X���A�^�b�`���ꂽ�I�u�W�F�N�g��������
    /// </summary>
    /// <typeparam name="T"> �T������C���^�t�F�[�X </typeparam>
    /// <returns> �擾�����N���X�z�� </returns>
    public static T[] FindObjectOfInterfaces<T>() where T : class
    {
        List<T> findList = new List<T>();

        // �I�u�W�F�N�g��T�����A���X�g�Ɋi�[
        foreach (var component in GameObject.FindObjectsOfType<Component>(true))
        {
            var obj = component as T;

            if (obj == null) continue;

            findList.Add(obj);
        }

        T[] findObjArray = new T[findList.Count];
        int count = 0;

        // �擾�����I�u�W�F�N�g���w�肵���C���^�t�F�[�X�^�z��Ɋi�[
        foreach (T obj in findList)
        {
            findObjArray[count] = obj;
            count++;
        }
        return findObjArray;
    }
}