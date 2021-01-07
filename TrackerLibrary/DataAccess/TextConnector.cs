using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";

        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load the text file and convert the text file to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the max Id
            int currentId = 1;

            if (prizes.Count > 0) 
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
                //currentId = prizes[prizes.Count-1].Id + 1; // A simpler way to perform the same task, but not quite as fool-proof.
            }

            model.Id = currentId;

            // Add the new record with the new Id (max + 1)
            prizes.Add(model);

            // Convert the prizes to List<string>
            // Save the List<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}
