using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DneTrainNg.Models;
using Microsoft.EntityFrameworkCore;

namespace DneTrainNg.Data.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly TrainingDbContext db;

        public SectionRepository(TrainingDbContext context)
        {
            db = context;
        }
        public Section CreateSection(Section section)
        {
            Section s = new Section {
                SectionCode=section.SectionCode,
                SectionName=section.SectionName
            };

            db.Sections.Add(s);
            db.SaveChanges();
            return db.Sections.Find(s.SectionId);
        }

        public void DeleteSection(int sectionId)
        {
            var record = db.Sections.Find(sectionId);
            db.Sections.Remove(record);
            var sectionStudents=db.Students.Where(s => s.SectionId.Equals(sectionId));
            foreach (var student in sectionStudents)
            {
                student.Section = null;
                student.SectionCode = "ZZ";
                // 99代表不隸屬任何一組
                student.SectionId = 99;
                student.SectionName = "未隸屬任何組別";
            }
            db.SaveChanges();
        }

        public IEnumerable<Section> GetSections()
        {
            var data =
               from sect in db.Sections

               select new Section
               {
                   SectionId = sect.SectionId,
                   SectionCode = sect.SectionCode,
                   SectionName = sect.SectionName,
                   Students = db.Students.Where(s => s.SectionId.Equals(sect.SectionId)).ToList()
               };

            return data.ToList();
        }

        public Section UpdateSection(int sectionId, Section section)
        {

            db.Entry(section).State = EntityState.Modified;
            db.SaveChanges();
            return db.Sections.Find(sectionId);
        }
    }
}
