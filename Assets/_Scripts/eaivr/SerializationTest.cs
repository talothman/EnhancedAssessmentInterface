using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

namespace eaivr
{
    public class SerializationTest : MonoBehaviour
    {
        public SortItemData[] sortItemData;
        public SelectItemData[] selectItemData;
        public SortItemData[] sortTest;
        public SelectItemData[] selectTest;

        public GameObject sortItemPrefab;

        public void SerializeSortSelect()
        {
            XmlUtility.Serialize(sortItemData, Application.dataPath + "/_Xml/sortSerial.xml");
            XmlUtility.Serialize(selectItemData, Application.dataPath + "/_Xml/selectSerial.xml");
        }

        public void DeserializeSortSelect()
        {
            sortTest = XmlUtility.Deserialize<SortItemData[]>(Application.dataPath + "/_Xml/sortSerial.xml");
            GameObject prefab = Instantiate(sortItemPrefab);
            prefab.GetComponent<SortItem3D>().InsertItemData(sortTest[0]);
        }
    }
}

