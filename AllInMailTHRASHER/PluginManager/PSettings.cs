﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace AllInMailTHRASHER.PluginManager
{
    public class PSettings
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Threads { get; set; }
        public string Capture { get; set; }
        public string FileName { get; set; }
        public string FileOutput { get; set; }
        public List<Requests> requests { get; set; }
        [Obfuscation(Feature = "virtualization", Exclude = true)]
        public static PSettings GetSettings(string file, bool files = true)
        {
            dynamic json = "";
            if(files)
            json = JsonConvert.DeserializeObject(File.ReadAllText(file));
            else
            {
                json = JsonConvert.DeserializeObject(file);
            }
            PSettings p = new PSettings
            {
                Name = json.Name,
                Author = json.Author,
                Threads = json.Threads,
                Capture = json.Capture,
                FileName = json.FileName,
                FileOutput = json.FileOutput,
                requests = ConfigPasser(json)
            };
            return p;
        }
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static List<Requests> ConfigPasser(dynamic json)
        {
            List<Requests> requests = new List<Requests>();
            foreach (dynamic ob in json.Requests)
            {
                requests.Add(new Requests
                {
                    Domain = ob.Domain,
                    variables = ParseVariables(ob),
                    successKeys = ParsesuccessKeys(ob),
                    failureKeys = ParsefailureKeys(ob)
                });
            }
             return requests;
        }
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static List<Variables> ParseVariables(dynamic s)
        {
            int index = 0;
            List<Variables> list = new List<Variables>();
            try
            {
                foreach (var ob in s.Variables)
                {
                    index++;
                    list.Add(new Variables
                    {
                        no = index,
                        regexpos = ob["Position"],
                        regex = ob["Regex"]

                    });

                }
                return list;
            }
            catch
            {
                return list;
            }
        }
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static List<successKeys> ParsesuccessKeys(dynamic s)
        {
            List<successKeys> list = new List<successKeys>();
            try
            {
                foreach (var ob in s.successKeys)
                {
                    list.Add(new successKeys
                    {
                        key = ob

                    });

                }
                return list;
            }
            catch
            {
                return list;
            }
        }
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static List<failureKeys> ParsefailureKeys(dynamic s)
        {
            List<failureKeys> list = new List<failureKeys>();
            try
            {
                foreach (var ob in s.failureKeys)
                {
                    list.Add(new failureKeys
                    {
                        key = ob

                    });

                }
                return list;
            }
            catch
            {
                return list;
            }
        }
    }
    }
public class Variables
{
    public int no { get; set; }
    public string regexpos { get; set; }
    public string regex { get; set; }
}
public class Requests
{
    public string Domain { get; set; }
    public List<Variables> variables { get; set; }
    public List<successKeys> successKeys { get; set; }
    public List<failureKeys> failureKeys { get; set; }
}
public class successKeys
{
    public string key { get; set; }
}
public class failureKeys
{
    public string key { get; set; }
}
