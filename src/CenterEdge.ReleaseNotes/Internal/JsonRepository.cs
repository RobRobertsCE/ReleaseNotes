using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CenterEdge.ReleaseNotes.Internal
{
    abstract class JsonRepository<T>
    {
        protected internal abstract string DataFile { get; set; }

        protected void Save(List<T> data)
        {
            var json = JsonConvert.SerializeObject(data);

            File.WriteAllText(DataFile, json);
        }

        protected List<T> Load()
        {
            if (File.Exists(DataFile))
            {
                var json = File.ReadAllText(DataFile);

                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            else
            {
                return null;
            }
        }
    }
}
