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

            //if (context.Sections.Any())
            //{
            //    return;
            //}

            //if (context.Students.Any())
            //{
            //    return;
            //}

            var c1 = new Course()
            {
                CourseId = Guid.NewGuid(),
                CourseName = "健康職場講座-姿勢好.運動對.痠痛說掰掰",
                CourseStartDate = "2017/12/12",
                CourseEndDate = "2017/12/12",
                CreateDate=new DateTime(2017,12,12),
                TrainHours = 2.0
            };
            var c2 = new Course()
            {
                CourseId = Guid.NewGuid(),
                CourseName = "106-1期「大型發電機測試及維護班」",
                CourseStartDate = "2017/12/04",
                CourseEndDate = "2017/12/08",
                CreateDate = new DateTime(2017, 12, 4),
                TrainHours = 26.5
            };
            var c3 = new Course()
            {
                CourseId = Guid.NewGuid(),
                CourseName = "106-1期「軸承與潤滑班」",
                CourseStartDate = "2017/10/05",
                CourseEndDate = "2017/10/13",
                CreateDate = new DateTime(2017, 10, 05),
                TrainHours = 28.5
            };
            var courses = new Course[]
            {
                c1,
                c2,
                c3
            };

            //var SectionM = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "機械組",
            //    SectionCode = "M"
            //};
            //var SectionN = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "核析組",
            //    SectionCode = "N"
            //};
            //var SectionE = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "電氣組",
            //    SectionCode = "E"
            //};
            //var SectionB = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "PE II",
            //    SectionCode = "B"
            //};
            //var SectionA = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "策劃組",
            //    SectionCode = "A"
            //};
            //var SectionJ = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "儀控組",
            //    SectionCode = "J",

            //};
            //var SectionV = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "處長室",
            //    SectionCode = "V",

            //};
            //var SectionO = new Section()
            //{
            //    //SectionId=1,
            //    SectionName = "移出",
            //    SectionCode = "O",

            //};
            //var sections = new Section[]
            //{
            //    SectionM,
            //    SectionN,
            //    SectionE,
            //    SectionB,
            //    SectionA,
            //    SectionJ,
            //    SectionV,
            //    SectionO
            //};

            //foreach (var course in courses)
            //{
            //    context.Courses.Add(course);
            //}

            //foreach (var section in sections)
            //{
            //    context.Sections.Add(section);
            //}

            //context.SaveChanges();

            //var SJ1 = new Student()
            //{
            //    //StudentId=1,
            //    StudentName = "李念中",
            //    SectionCode = "J",
            //    SectionName = "儀控組",
            //    Section=null,
            //    SectionId = SectionJ.SectionId
            //};
            //var SA1 = new Student()
            //{
            //    // StudentId=2,
            //    StudentName = "廖經政",
            //    SectionCode = "A",
            //    SectionName = "策劃組",
            //    Section = null,
            //    SectionId = SectionA.SectionId
            //};
            //var SN1 = new Student()
            //{
            //    //StudentId=3,
            //    StudentName = "李宗翰",
            //    SectionCode = "N",
            //    SectionName = "核析組",
            //    Section = null,
            //    SectionId = SectionN.SectionId
            //};
            //var SE1 = new Student()
            //{
            //    //
            //    StudentName = "謝文龍",
            //    SectionCode = "E",
            //    SectionName = "電氣組",
            //    Section = null,
            //    SectionId = SectionE.SectionId
            //};
            //var SB1 = new Student()
            //{
            //    //
            //    StudentName = "張漢清",
            //    SectionCode = "B",
            //    SectionName = "PE_II",
            //    Section = null,
            //    SectionId = SectionB.SectionId
            //};
            //var SM1 = new Student()
            //{
            //    //StudentId=6,
            //    StudentName = "陳政瑋",
            //    SectionCode = "M",
            //    SectionName = "機械組",
            //    Section = null,
            //    SectionId = SectionM.SectionId
            //};
            //var SV1 = new Student()
            //{
            //    //StudentId=6,
            //    StudentName = "李瑞蓮",
            //    SectionCode = "V",
            //    SectionName = "處長室",
            //    Section = null,
            //    SectionId = SectionV.SectionId
            //};
            //var SO1 = new Student()
            //{
            //    //StudentId=6,
            //    StudentName = "賴逢裕",
            //    SectionCode = "O",
            //    SectionName = "移出",
            //    Section = null,
            //    SectionId = SectionO.SectionId
            //};
            //var students = new Student[]
            //{
            //   SJ1,
            //   SE1,
            //   SA1,
            //   SB1,
            //   SN1,
            //   SM1,
            //   SV1,
            //   SO1
            //};



            

            //SectionJ.Students = new List<Student>{
            //    SJ1
            //};
            //SectionE.Students = new List<Student> {
            //    SE1
            //};
            //SectionA.Students = new List<Student> {
            //    SA1
            //};
            //SectionB.Students = new List<Student> {
            //    SB1
            //};
            //SectionN.Students = new List<Student> {
            //    SN1
            //};
            //SectionM.Students = new List<Student> {
            //    SM1
            //};
            //SectionV.Students = new List<Student> {
            //    SV1
            //};
            //SectionO.Students = new List<Student> {
            //    SO1
            //};
            //foreach (var student in students)
            //{
            //    context.Students.Add(student);
            //}

            //context.SaveChanges();
            //var sc1 = new StudentCourse
            //{
            //    CourseId=c1.CourseId,
            //    StudentId=SJ1.StudentId,

            //};

            //var sc2 = new StudentCourse
            //{
            //    CourseId = c1.CourseId,
            //    StudentId = SJ1.StudentId,

            //};

            //var scs = new StudentCourse[]
            //{
            //    sc1
            //};
            //var scs = new StudentCourse[]
            //{
            //    sc1,
            //    sc2,
            //};

            //context.StudentCourses.AddRange(scs);
            //context.SaveChanges();
        }
    }
}
