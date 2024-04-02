using codecampapi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace codecampapi.Services
{
    public class CodeCampService
    {
        private readonly ILogger<CodeCampService> _logger;
        private static List<CodeCamperModel> items = new List<CodeCamperModel>
        {
            new CodeCamperModel {  CamperId = 1, CamperName = "Better Name 1", Track = CodeCampTrack.FrontEnd },
            new CodeCamperModel {  CamperId = 1, CamperName = "Better Name 2", Track = CodeCampTrack.Backend },
            new CodeCamperModel {  CamperId = 1, CamperName = "Better Name 3", Track = CodeCampTrack.Mobile },
        };

        public CodeCampService(ILogger<CodeCampService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<CodeCamperModel> GetAll()
        {
            return items;
        }

        public CodeCamperModel GetById(int id)
        {
            return items.Find(i => i.CamperId == id);
        }

        public void Add(CodeCamperModel newItem)
        {
            items.Add(newItem);
        }

        public bool Update(int id, CodeCamperModel updatedItem)
        {
            var existingItem = items.Find(i => i.CamperId == id);

            if (existingItem == null)
            {
                return false;
            }

            existingItem.CamperName = updatedItem.CamperName;
            return true;
        }

        public bool Delete(int id)
        {
            var itemToRemove = items.Find(i => i.CamperId == id);

            if (itemToRemove == null)
            {
                return false;
            }

            items.Remove(itemToRemove);
            return true;
        }
    }
}
