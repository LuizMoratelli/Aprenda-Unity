using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LoadXMLFile : MonoBehaviour
{
    #region Sub-Classes and Structs
    #endregion

    #region Public Members
    public List<string> InterfaceTitulo;
    public List<string> InterfaceLoja;
    #endregion

    #region Private Members
    [SerializeField] private string arquivoXML;
    [SerializeField] private string idioma;
    #endregion

    #region Public Methods
    public void LoadXMLData()
    {
        if (PlayerPrefs.GetString("idioma") != null)
        {
            idioma = PlayerPrefs.GetString("idioma");
        }

        InterfaceTitulo.Clear();
        InterfaceLoja.Clear();

        TextAsset _xmlData = (TextAsset)Resources.Load(idioma + "/" + arquivoXML);
        XmlDocument _xmlDocument = new XmlDocument();

        _xmlDocument.LoadXml(_xmlData.text);

        foreach (XmlNode node in _xmlDocument["language"].ChildNodes)
        {
            string _nodeName = node.Attributes["name"].Value;

            foreach (XmlNode n in node["textos"].ChildNodes)
            {
                switch (_nodeName)
                {
                    case "interface_titulo":
                        InterfaceTitulo.Add(n.InnerText);
                        break;
                    case "interface_loja":
                        InterfaceLoja.Add(n.InnerText);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    #endregion

    #region Private Methods
    #region Default Unity Methods
    private void Awake()
    {
        LoadXMLData();
    }
    #endregion
    #endregion
}
