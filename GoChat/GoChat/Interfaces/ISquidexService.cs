using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoChat.Interfaces
{
    public interface ISquidexService
    {
        Task<string> AddAssetFile(string speech, string filename);
    }
}
   