using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Reflection;
using System.IO;

namespace BombSquad.StaticClasses
{
    public static class Configuration
    {
        private static readonly string ConfigFile = "Configuration.XML";
        private static XmlSchemaSet schemaSet = new XmlSchemaSet();

        /// <summary>
        /// Initializes the <see cref="Configuration"/> class.
        /// </summary>
        static Configuration()
        {            
            Assembly a = Assembly.GetExecutingAssembly();
            using (Stream stream = a.GetManifestResourceStream("BombSquad.Schemas.BombSquadConfiguration.xsd"))
            {
                using (XmlReader xmlReader = XmlReader.Create(stream)) { schemaSet.Add("", xmlReader); }
            }

            if (!System.IO.File.Exists(ConfigFile))
                WriteConfigFile();
        }

        private static void WriteConfigFile()
        {
            XmlDocument configDoc = new XmlDocument();
            XmlElement root = configDoc.DocumentElement;

            XmlElement userPreferenceElement = configDoc.CreateElement(string.Empty, "Configuration", string.Empty);
            XmlElement subElement = configDoc.CreateElement(string.Empty, "MaxAttempts", string.Empty);
            XmlText elementText = configDoc.CreateTextNode(mMaxAttempts.ToString());
            subElement.AppendChild(elementText);
            userPreferenceElement.AppendChild(subElement);

            subElement = configDoc.CreateElement(string.Empty, "DefaultStartTime", string.Empty);
            elementText = configDoc.CreateTextNode(mDefaultStartTime.TotalSeconds.ToString());
            subElement.AppendChild(elementText);
            userPreferenceElement.AppendChild(subElement);

            subElement = configDoc.CreateElement(string.Empty, "CodeLength", string.Empty);
            elementText = configDoc.CreateTextNode(mCodeLength.ToString());
            subElement.AppendChild(elementText);
            userPreferenceElement.AppendChild(subElement);

            configDoc.AppendChild(userPreferenceElement);
            configDoc.Save(ConfigFile);
        }

        private static int mMaxAttempts = 8;
        public static int MaxAttempts
        {
            get 
            {
                if(System.IO.File.Exists(ConfigFile))
                {
                    string xmlContent = File.ReadAllText(ConfigFile);
                    if (xmlContent != string.Empty)
                    {
                        bool error = false;
                        XDocument configSetting = XDocument.Parse(xmlContent);
                        configSetting.Validate(schemaSet, (o, e) => { error = true; });
                        if (!error)
                        {
                            XmlDocument configDoc = new XmlDocument();
                            configDoc.LoadXml(xmlContent);
                            XmlNodeList elementList = configDoc.GetElementsByTagName("MaxAttempts");
                            if (elementList.Count == 1)
                            {
                                XmlNode firstNode = elementList.Item(0);
                                string innerText = firstNode.InnerText;
                                int.TryParse(innerText, out mMaxAttempts);
                            }
                        }
                    }
                }
                return mMaxAttempts; 
            }
            set { mMaxAttempts = value; WriteConfigFile(); }
        }

        private static TimeSpan mDefaultStartTime = new TimeSpan(0, 1, 0);
        public static TimeSpan DefaultStartTime
        {
            get 
            {
                if (System.IO.File.Exists(ConfigFile))
                {
                    string xmlContent = File.ReadAllText(ConfigFile);
                    if (xmlContent != string.Empty)
                    {
                        bool error = false;
                        XDocument configSetting = XDocument.Parse(xmlContent);
                        configSetting.Validate(schemaSet, (o, e) => { error = true; });
                        if (!error)
                        {
                            XmlDocument configDoc = new XmlDocument();
                            configDoc.LoadXml(xmlContent);
                            XmlNodeList elementList = configDoc.GetElementsByTagName("DefaultStartTime");
                            if (elementList.Count == 1)
                            {
                                XmlNode firstNode = elementList.Item(0);
                                string innerText = firstNode.InnerText;
                                int numSeconds = 10;
                                int.TryParse(innerText, out numSeconds);
                                mDefaultStartTime = TimeSpan.FromSeconds(numSeconds);
                            }
                        }
                    }
                }
                return mDefaultStartTime; 
            }
            set { mDefaultStartTime = value; WriteConfigFile(); }
        }

        private static int mCodeLength = 4;
        public static int CodeLength
        {
            get 
            {
                if (System.IO.File.Exists(ConfigFile))
                {
                    string xmlContent = File.ReadAllText(ConfigFile);
                    if (xmlContent != string.Empty)
                    {
                        bool error = false;
                        XDocument configSetting = XDocument.Parse(xmlContent);
                        configSetting.Validate(schemaSet, (o, e) => { error = true; });
                        if (!error)
                        {
                            XmlDocument configDoc = new XmlDocument();
                            configDoc.LoadXml(xmlContent);
                            XmlNodeList elementList = configDoc.GetElementsByTagName("CodeLength");
                            if (elementList.Count == 1)
                            {
                                XmlNode firstNode = elementList.Item(0);
                                string innerText = firstNode.InnerText;
                                int.TryParse(innerText, out mCodeLength);
                            }
                        }
                    }
                }
                return mCodeLength; 
            }
            set { mCodeLength = value; WriteConfigFile(); }
        }
    }
}