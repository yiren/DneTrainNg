using DneTrainNg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DneTrainNg.Data.SeedData
{
    public static class TrainingSeedData
    {
        public static void Initialize(TrainingDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Courses.Any())
            {
                return;
            }

            var courses = new Course[]
            {
                new Course()
                {
                    CourseId=Guid.NewGuid(),
                    CourseName="健康職場講座-姿勢好.運動對.痠痛說掰掰",
                    CourseStartDate="2017/12/12",
                    CourseEndDate="2017/12/12",
                    TrainHours=2.0
                },
                new Course()
                {
                    CourseId=Guid.NewGuid(),
                    CourseName="106-1期「大型發電機測試及維護班」",
                    CourseStartDate="2017/12/04",
                    CourseEndDate="2017/12/08",
                    TrainHours=26.5
                },
                new Course()
                {
                    CourseId=Guid.NewGuid(),
                    CourseName="106-1期「大型發電機測試及維護班」",
                    CourseStartDate="2017/12/04",
                    CourseEndDate="2017/12/08",
                    TrainHours=27.0
                }
            };

            var sections = new Section[]
            {
                new Section()
                {
                    //SectionId=1,
                    SectionName="機械組",
                    SectionCode="M"
                },
                new Section()
                {
                   // SectionId=2,
                    SectionName="電氣組",
                    SectionCode="E"
                },
                new Section()
                {
                    //SectionId=3,
                    SectionName="儀控組",
                    SectionCode="J"
                },
                new Section()
                {
                    //SectionId=4,
                    SectionName="PE II",
                    SectionCode="B"
                },
                new Section()
                {
                    //SectionId=5,
                    SectionName="策劃組",
                    SectionCode="A"
                }
            };

            var students = new Student[]
            {
                new Student()
                {
                    //StudentId=1,
                    StudentName="李念中",
                    SectionCode="J",
                    SectionName="儀控組",
                    Section=sections.FirstOrDefault(s=>s.SectionCode.Equals("J"))
                },
                new Student()
                {
                   // StudentId=2,
                    StudentName="廖經政",
                    SectionCode="A",
                    SectionName="策劃組",
                    Section=sections.FirstOrDefault(s=>s.SectionCode.Equals("A"))
                },
                new Student()
                {
                    //StudentId=3,
                    StudentName="李宗翰",
                    SectionCode="N",
                    SectionName="核析組",
                    Section=sections.FirstOrDefault(s=>s.SectionCode.Equals("N"))
                },
                new Student()
                {
                   //
                    StudentName="謝文龍",
                    SectionCode="E",
                    SectionName="電氣組",
                    Section=sections.FirstOrDefault(s=>s.SectionCode.Equals("E"))
                },
                new Student()
                {
                    //
                    StudentName="張漢清",
                    SectionCode="B",
                    SectionName="PE_II",
                    Section=sections.FirstOrDefault(s=>s.SectionCode.Equals("B"))
                },
                new Student()
                {
                    //StudentId=6,
                    StudentName="陳政瑋",
                    SectionCode="M",
                    SectionName="機械組",
                    Section=sections.FirstOrDefault(s=>s.SectionCode.Equals("M"))
                },
            };


            foreach (var course in courses)
            {
                context.Courses.Add(course);
            }

            foreach (var section in sections)
            {
                context.Sections.Add(section);
            }

            foreach (var student in students)
            {
                context.Students.Add(student);
            }

            context.SaveChanges();

        }
    }
}
