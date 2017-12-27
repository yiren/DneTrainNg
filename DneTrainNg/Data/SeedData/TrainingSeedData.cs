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


            var c1 = new Course()
            {
                CourseId = Guid.NewGuid(),
                CourseName = "健康職場講座-姿勢好.運動對.痠痛說掰掰",
                CourseStartDate = "2017/12/12",
                CourseEndDate = "2017/12/12",
                TrainHours = 2.0
            };
            var c2 = new Course()
            {
                CourseId = Guid.NewGuid(),
                CourseName = "106-1期「大型發電機測試及維護班」",
                CourseStartDate = "2017/12/04",
                CourseEndDate = "2017/12/08",
                TrainHours = 26.5
            };
            var c3 = new Course()
            {
                CourseId = Guid.NewGuid(),
                CourseName = "106-1期「軸承與潤滑班」",
                CourseStartDate = "2017/10/05",
                CourseEndDate = "2017/10/13",
                TrainHours = 28.5
            };
            var courses = new Course[]
            {
                c1,
                c2,
                c3
            };

            var SectionM = new Section()
            {
                //SectionId=1,
                SectionName = "機械組",
                SectionCode = "M"
            };
            var SectionN = new Section()
            {
                //SectionId=1,
                SectionName = "核析組",
                SectionCode = "N"
            };
            var SectionE = new Section()
            {
                //SectionId=1,
                SectionName = "電氣組",
                SectionCode = "E"
            };
            var SectionB = new Section()
            {
                //SectionId=1,
                SectionName = "PE II",
                SectionCode = "B"
            };
            var SectionA = new Section()
            {
                //SectionId=1,
                SectionName = "策劃組",
                SectionCode = "A"
            };
            var SectionJ = new Section()
            {
                //SectionId=1,
                SectionName = "儀控組",
                SectionCode = "J",

            };
            var sections = new Section[]
            {
                SectionM,
                SectionN,
                SectionE,
                SectionB,
                SectionA,
                SectionJ
            };

            var SJ1 = new Student()
            {
                //StudentId=1,
                StudentName = "李念中",
                SectionCode = "J",
                SectionName = "儀控組",
                Section = sections.FirstOrDefault(s => s.SectionCode.Equals("J"))
            };
            var SA1 = new Student()
            {
                // StudentId=2,
                StudentName = "廖經政",
                SectionCode = "A",
                SectionName = "策劃組",
                Section = sections.FirstOrDefault(s => s.SectionCode.Equals("A"))
            };
            var SN1 = new Student()
            {
                //StudentId=3,
                StudentName = "李宗翰",
                SectionCode = "N",
                SectionName = "核析組",
                Section = sections.FirstOrDefault(s => s.SectionCode.Equals("N"))
            };
            var SE1 = new Student()
            {
                //
                StudentName = "謝文龍",
                SectionCode = "E",
                SectionName = "電氣組",
                Section = sections.FirstOrDefault(s => s.SectionCode.Equals("E"))
            };
            var SB1 = new Student()
            {
                //
                StudentName = "張漢清",
                SectionCode = "B",
                SectionName = "PE_II",
                Section = sections.FirstOrDefault(s => s.SectionCode.Equals("B"))
            };
            var SM1 = new Student()
            {
                //StudentId=6,
                StudentName = "陳政瑋",
                SectionCode = "M",
                SectionName = "機械組",
                Section = sections.FirstOrDefault(s => s.SectionCode.Equals("M"))
            };
            var students = new Student[]
            {
               SJ1,
               SE1,
               SA1,
               SB1,
               SN1,
               SM1,
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

            SectionJ.Students.Add(SJ1);
            SectionE.Students.Add(SE1);
            SectionA.Students.Add(SA1);
            SectionB.Students.Add(SB1);
            SectionN.Students.Add(SN1);
            SectionM.Students.Add(SM1);

            var sc1 = new StudentCourse
            {
                CourseId=c1.CourseId,
                StudentId=SJ1.StudentId,
                
            };

            var scs = new StudentCourse[]
            {
                sc1
            };

        }
    }
}
