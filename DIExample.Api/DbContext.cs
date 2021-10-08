using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DIExample.Api
{
    public class DbContext
    {
        public string Path { get; }
        public string FileName { get; }
        public string FullPath { get; }

        public DbContext()
        {
            Path = Directory.GetCurrentDirectory();
            FileName = "db.json";
            FullPath = $@"{Path}\{FileName}";
        }
    }
}
