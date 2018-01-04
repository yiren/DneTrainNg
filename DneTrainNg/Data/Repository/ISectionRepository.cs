using DneTrainNg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.Repository
{
    public interface ISectionRepository
    {
        Section CreateSection(Section section);
        Section UpdateSection(int sectionId, Section section);
        void DeleteSection(int sectionId);
        IEnumerable<Section> GetSections();
    }
}
