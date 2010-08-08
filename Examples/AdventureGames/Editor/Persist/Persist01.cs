using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.PathFinding;
using System.Xml;

namespace Editor.Persist
{
    /// <summary>
    /// Save and load 0.1 versions of the game
    /// </summary>
    class Persist01
    {
        #region Saving
        internal static void Save(string path, Scene scene, TextureManager textureManager)
        {
            // Create a big string as the xml data 
            StringBuilder saveData = new StringBuilder();
            saveData.AppendLine("<gamedata>");
            {
                // Save the texture data (at some point associate with scene / when used)
                saveData.AppendLine("\t<texturedata>");
                {
                    foreach (var textureEntry in textureManager)
                    {
                        saveData.AppendFormat("\t\t<entry key=\"{0}\" value=\"{1}\" />\n",
                            textureEntry.Key, textureEntry.Value.Path);
                    }
                }
                saveData.AppendLine("\t</texturedata>");
                // Save the scene
                saveData.AppendLine("\t<scenedata>");
                {
                    // there's only one scene
                    SaveScene(scene, 2, saveData);
                }
                saveData.AppendLine("\t</scenedata>");
            }
            saveData.AppendLine("</gamedata>");
            System.IO.File.WriteAllText(path, saveData.ToString());
        }

        private static void SaveScene(Scene scene, int indentLevel, StringBuilder xmlData)
        {
            xmlData.AppendLine(new string('\t', indentLevel) + "<scene>");
            {
                indentLevel++;
                xmlData.AppendLine(new string('\t', indentLevel) + "<navmesh>");
                {
                    indentLevel++;
                    SaveNavMeshPolygons(scene.NavMesh, indentLevel, xmlData);
                    SaveNavMeshLinks(scene.NavMesh, indentLevel, xmlData);
                    indentLevel--;
                }
                xmlData.AppendLine(new string('\t', indentLevel) + "</navmesh>");
                indentLevel--;

                indentLevel++;
                xmlData.AppendLine(new string('\t', indentLevel) + "<layers>");
                {
                    indentLevel++;
                    foreach (Layer layer in scene.Layers)
                    {
                        xmlData.AppendFormat(new string('\t', indentLevel) +
                            "<layer name=\"{0}\" textureid=\"{1}\" />",
                            layer.Name, layer.TextureId);

                    }
                    indentLevel--;
                }
                xmlData.AppendLine(new string('\t', indentLevel) + "</layers>");
                indentLevel--;
            }
            xmlData.AppendLine(new string('\t', indentLevel) + "</scene>");
        }


        private static int FindPolygonIndex(NavMesh navMesh, ConvexPolygon convexPolygon)
        {
            return navMesh.PolygonList.FindIndex(convexPolygon.Equals);
        }

        private static void SaveNavMeshLinks(NavMesh navMesh, int indentLevel, StringBuilder xmlData)
        {
            xmlData.AppendLine(new string('\t', indentLevel) + "<links>");
            {
                indentLevel++;
                foreach (var link in navMesh.Links)
                {
                    xmlData.AppendLine(new string('\t', indentLevel) + "<link>");
                    {
                        indentLevel++;

                        xmlData.AppendFormat(new string('\t', indentLevel) +
                            "<start polygon=\"{0}\" edgestart=\"{1}\" edgeend=\"{2}\"/>\n",
                            FindPolygonIndex(navMesh, link.StartPoly),
                            link.StartEdgeIndex.Start,
                            link.StartEdgeIndex.End);

                        xmlData.AppendFormat(new string('\t', indentLevel) +
                            "<end polygon=\"{0}\" edgestart=\"{1}\" edgeend=\"{2}\"/>\n",
                            FindPolygonIndex(navMesh, link.EndPoly),
                            link.EndEdgeIndex.Start,
                            link.EndEdgeIndex.End);

                        indentLevel--;
                    }
                    xmlData.AppendLine(new string('\t', indentLevel) + "</link>");
                }
                indentLevel--;
            }
            xmlData.AppendLine(new string('\t', indentLevel) + "</links>");
        }

        private static void SaveNavMeshPolygons(NavMesh navMesh, int indentLevel, StringBuilder xmlData)
        {
            xmlData.AppendLine(new string('\t', indentLevel) + "<polygons>");
            {
                indentLevel++;
                foreach (ConvexPolygon polygon in navMesh.PolygonList)
                {
                    xmlData.AppendLine(new string('\t', indentLevel) + "<polygon>");
                    {
                        indentLevel++;
                        foreach (Point point in polygon.Vertices)
                        {
                            xmlData.AppendFormat(new string('\t', indentLevel) +
                                "<point x=\"{0}\" y =\"{1}\"/>",
                                point.X, point.Y);
                        }
                        indentLevel--;
                    }
                    xmlData.AppendLine(new string('\t', indentLevel) + "</polygon>");
                }
                indentLevel--;
            }
            xmlData.AppendLine(new string('\t', indentLevel) + "</polygons>");
        }
        #endregion 
    
        #region Loading
        internal static void Open(string path, Scene scene, TextureManager textureManager, Layers layers)
        {
            XmlTextReader xmlReader = new System.Xml.XmlTextReader(path);
            xmlReader.MoveToContent();  // Jumps into top level header
            while (xmlReader.Read())
            {
                if ("texturedata" == xmlReader.Name)
                {
                    LoadTextureData(xmlReader, textureManager);
                }
                else if("scenedata" == xmlReader.Name)
                {
                    LoadSceneData(xmlReader, scene, textureManager);
                }
            }
            layers.RefreshLayerContent(scene.Layers);
        }

        private static void LoadSceneData(XmlTextReader xmlReader, Scene scene, TextureManager textureManager)
        {
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("scene" == xmlReader.Name)
                {
                    LoadSingleScene(xmlReader, scene, textureManager);
                }
                else if ("sceendata" == xmlReader.Name)
                {
                    // Closing tag so jump out.
                    return;
                }
            }
        }

        private static void LoadSingleScene(XmlTextReader xmlReader, Scene scene, TextureManager textureManager)
        {
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("navmesh" == xmlReader.Name)
                {
                    LoadNavMesh(xmlReader, scene);
                }
                else if ("layers" == xmlReader.Name)
                {
                    LoadLayers(xmlReader, scene, textureManager);
                }
                else if ("scene" == xmlReader.Name)
                {
                    return;
                }
            }
        }

        private static void LoadLayers(XmlTextReader xmlReader, Scene scene, TextureManager textureManager)
        {
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("layer" == xmlReader.Name)
                {
                    string name = xmlReader.GetAttribute("name");
                    string textureId = xmlReader.GetAttribute("textureid");
                    Layer layer = new Layer(name);
                    layer.SetImage(textureManager.Get(textureId));
                    scene.AddLayer(layer);
                }
                else if ("layers" == xmlReader.Name)
                {
                    return;
                }
            }
        }

        private static void LoadNavMesh(XmlTextReader xmlReader, Scene scene)
        {
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("polygons" == xmlReader.Name)
                {
                    LoadPolygons(xmlReader, scene.NavMesh);
                }
                else if ("links" == xmlReader.Name)
                {
                    LoadLinks(xmlReader, scene.NavMesh);
                }
                else if ("navmesh" == xmlReader.Name)
                {
                    return;
                }
            }
        }

        private static void LoadLinks(XmlTextReader xmlReader, NavMesh navMesh)
        {
            
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("link" == xmlReader.Name)
                {
                    LoadSingleLink(xmlReader, navMesh);
                }
                else if ("links" == xmlReader.Name)
                {
                    return;
                }
            }
        }



        /// <summary>
        /// This function will look up the polygon index right away. 
        /// This could potentially cause problems if the XML has the polygon defs later in the file.
        /// The values should really be cached and it should be done after.
        /// (or thrown in a closure but the C# syntax is a little too messy for that)
        /// </summary>
        /// <param name="xmlReader"></param>
        /// <param name="navMesh"></param>
        private static void LoadSingleLink(XmlTextReader xmlReader, NavMesh navMesh)
        {
            int startPolygonIndex   = -1;
            int startEdgeStart      = -1;
            int startEdgeEnd        = -1;

            int endPolygonIndex = -1;
            int endEdgeStart    = -1;
            int endEdgeEnd      = -1;

            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("start" == xmlReader.Name)
                {
                    startPolygonIndex = int.Parse(xmlReader.GetAttribute("polygon"));
                    startEdgeStart = int.Parse(xmlReader.GetAttribute("edgestart"));
                    startEdgeEnd = int.Parse(xmlReader.GetAttribute("edgeend"));   
                }
                else if ("end" == xmlReader.Name)
                {
                    endPolygonIndex = int.Parse(xmlReader.GetAttribute("polygon"));
                    endEdgeStart = int.Parse(xmlReader.GetAttribute("edgestart"));
                    endEdgeEnd = int.Parse(xmlReader.GetAttribute("edgeend"));
                }
                else if ("link" == xmlReader.Name)
                {
                    PolygonLink polygonLink = new PolygonLink(
                        navMesh.PolygonList[startPolygonIndex],
                        new IndexedEdge(startEdgeStart, startEdgeEnd),
                        navMesh.PolygonList[endPolygonIndex],
                        new IndexedEdge(endEdgeStart, endEdgeEnd));
                    navMesh.AddLink(polygonLink);
                    return;
                }
            }
        }

        private static void LoadPolygons(XmlTextReader xmlReader, NavMesh navMesh)
        {
              xmlReader.MoveToContent();
              while (xmlReader.Read())
              {
                  if ("polygon" == xmlReader.Name)
                  {
                      LoadSinglePolygon(xmlReader, navMesh);
                  }
                  else if ("polygons" == xmlReader.Name)
                  {
                      return;
                  }
              }
        }

        private static void LoadSinglePolygon(XmlTextReader xmlReader, NavMesh navMesh)
        {
            ConvexPolygon polygon = new ConvexPolygon();
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if ("point" == xmlReader.Name)
                {
                    float x = float.Parse(xmlReader.GetAttribute("x"));
                    float y = float.Parse(xmlReader.GetAttribute("y"));
                    polygon.Vertices.Add(new Point(x, y));
                }
                else if("polygon" == xmlReader.Name)
                {
                    polygon.GenerateEdges();
                    navMesh.AddPolygon(polygon);
                    return;
                }
            }
        }

        private static void LoadTextureData(XmlTextReader xmlReader, TextureManager textureManager)
        {
            // Jump into the texture data node
            xmlReader.MoveToContent();
            while (xmlReader.Read())
            {
                if (xmlReader.Name == "entry")
                {
                    string key = xmlReader.GetAttribute("key");
                    string value = xmlReader.GetAttribute("value");
                    if (textureManager.Exists(key))
                    {
                        System.Windows.Forms.MessageBox.Show("Warning a texture is trying to be loaded twice. This shouldn't ever happen.");
                    }
                    else
                    {
                        textureManager.LoadTexture(key, value);
                    }
                }
                else if (xmlReader.Name == "texturedata")
                {
                    // closing tag return.
                    return;
                }

            }
        }
        #endregion
    }
}
