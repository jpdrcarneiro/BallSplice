using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class itemDatabase {

    [XmlAttribute("name")]
    public string name;

    [XmlElement("Damage")]
    public string damage;

    [XmlElement("CountofBalls")]
    public string countOfBalls; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
