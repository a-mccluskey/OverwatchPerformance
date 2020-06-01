using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    interface IDataSource
    {
        bool VerifySourceExists();

        List<Game> ReadExistingGamesSource();

        void SaveGamesToDataSource(List<Game> games);

    }
}
