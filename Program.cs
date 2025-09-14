using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace AOITagger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParseL5X("D:\\Projects\\GitTest\\AOI\\DI_Alm\\DI_Alm_AOI.L5X");
        }

        static void ParseL5X(string path)
        {
            string line;
            try
            {
                // Pass file path and file name to StreamReader constructor
                StreamReader sr = new StreamReader(path);

                // Read the first line of file
                line = sr.ReadLine();

                // Initialize AOI name variable
                string aoiName = null;

                // Find AOI name
                while (aoiName == null)
                {
                    aoiName = FindAOIName(line);
                    if (aoiName != null)
                    {
                        Console.WriteLine("AOI Name: " + aoiName);
                        break;
                    }
                    line = sr.ReadLine();
                }
                Console.WriteLine();

                // Find next AOI
                string nextAOI = null;

                // End tag for AOI block
                const string endParam = "</AddOnInstructionDefinition>";

                // Find next AOI name and tags
                while (line != endParam)
                {
                    if (nextAOI == null)
                    {
                        while (nextAOI == null)
                        {
                            nextAOI = FindNextAOI(line);
                            if (nextAOI != null)
                            {
                                Console.WriteLine("Contains AOI: " + nextAOI);
                                break;
                            }
                            line = sr.ReadLine();
                        }
                    }
                    else
                    {
                        while (line != endParam)
                        {
                            string nextTag = OutputTag(line);
                            if (nextTag != null)
                            {
                                Console.WriteLine(nextTag);
                                Console.WriteLine();
                            }
                            line = sr.ReadLine();
                        }
                    }
                }
                Console.WriteLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        static string FindAOIName(string lineData)
        {
            const string header = "<RSLogix5000Content";
            const string target = "TargetName=\"";
            if (lineData.StartsWith(header))
            {
                int startIdx = lineData.IndexOf(target);
                if (startIdx >= 0)
                {
                    startIdx += target.Length;
                    int endIdx = lineData.IndexOf("\"", startIdx);
                    if (endIdx > startIdx)
                    {
                        return lineData.Substring(startIdx, endIdx - startIdx);
                    }
                }
            }
            return null;
        }

        static string FindNextAOI(string lineData)
        {
            const string header = "<AddOnInstructionDefinition Name=";
            const string nameAttr = "Name=\"";
            if (lineData.StartsWith(header))
            {
                int startIdx = lineData.IndexOf(nameAttr);
                if (startIdx >= 0)
                {
                    startIdx += nameAttr.Length;
                    int endIdx = lineData.IndexOf("\"", startIdx);
                    if (endIdx > startIdx)
                    {
                        return lineData.Substring(startIdx, endIdx - startIdx);
                    }
                }
            }
            return null;
        }

        static string OutputTag(string lineData)
        {
            const string header = "<Parameter ";
            const string tagNameAttr = "Name=\"";
            const string tagDataTypeAttr = "DataType=\"";
            const string tagAccessAttr = "ExternalAccess=\"";
            string tagName = null;
            string tagDataType = null;
            string tagAccess = null;

            if (lineData.StartsWith(header))
            {
                int startIdx = lineData.IndexOf(tagNameAttr);
                if (startIdx >= 0)
                {
                    startIdx += tagNameAttr.Length;
                    int endIdx = lineData.IndexOf("\"", startIdx);
                    if (endIdx > startIdx)
                    {
                        tagName = lineData.Substring(startIdx, endIdx - startIdx);
                    }
                }
                startIdx = lineData.IndexOf(tagDataTypeAttr);
                if (startIdx >= 0)
                {
                    startIdx += tagDataTypeAttr.Length;
                    int endIdx = lineData.IndexOf("\"", startIdx);
                    if (endIdx > startIdx)
                    {
                        tagDataType = lineData.Substring(startIdx, endIdx - startIdx);
                    }
                }
                startIdx = lineData.IndexOf(tagAccessAttr);
                if (startIdx >= 0)
                {
                    startIdx += tagAccessAttr.Length;
                    int endIdx = lineData.IndexOf("\"", startIdx);
                    if (endIdx > startIdx)
                    {
                        tagAccess = lineData.Substring(startIdx, endIdx - startIdx);
                    }
                }
                return "Tag Name: " + tagName + "\nData Type: " + tagDataType + "\nAccess: " + tagAccess;
            }
            else
            {
                return null;
            }
        }

        static void ReadFile(string path)
        {
            string line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(path);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        static void WriteFile(string path)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(path);
                //Write a line of text
                sw.WriteLine("Hello World!!");
                //Write a second line of text
                sw.WriteLine("From the StreamWriter class");
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
