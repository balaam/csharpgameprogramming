using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.PathFinding;

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
    }
}
